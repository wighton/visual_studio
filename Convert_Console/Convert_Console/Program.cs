using System;
using System.IO;
using Newtonsoft.Json;
using CsvHelper;
using Gigya.Socialize.SDK;
using System.Configuration;

namespace Convert_Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            // File bulk import file, read in to system and parse via Csv Reader.
            try
            {
                Console.WriteLine("Searching for file to load.\n");
                string readFromFileLocation = @"C:\Users\stuart.wighton\Downloads\Rollout_2.csv";
                StreamReader streamReader = new StreamReader(readFromFileLocation);
                var parsedImportFile = new CsvReader(streamReader);

                // Intiate bulkUpload object and populate with parsed file data.
                Importer bulkUpload = new Importer();
                int count = 0;
                while (parsedImportFile.Read())
                {
                    Console.WriteLine("Building profile: {0}.\n", count);
                    bulkUpload.accounts.Add(new Account
                    {
                        siteUID = "FromWhileLoopToGSObject" + count,
                        email = parsedImportFile.GetField<string>(5),
                        profile = new Profile
                        {
                            firstName = parsedImportFile.GetField<string>(3),
                            lastName = parsedImportFile.GetField<string>(4)
                        },
                        data = new Data()
                    });
                    Console.WriteLine("Profile {0} completed.\n", count);
                    bulkUpload.settings.totalRecords = count;

                    if (count > 1)
                    {
                        Console.WriteLine("End of loop.\n");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Preparing profile for creation.\n");
                        string siteUID = bulkUpload.accounts[count].siteUID;
                        string email = bulkUpload.accounts[count].email;
                        GigyaCreateAccount(siteUID, email);
                    };
                    count++;
                } // End of parsed file read.

                // Save bulkUpload object to file
                Console.WriteLine("Saving file.\n");
                SavetoFile(bulkUpload);

            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }

            catch (Exception e)
            {
                Console.WriteLine("Unknown problem stopped json file being created.");
                Console.WriteLine("");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            Console.WriteLine("Job Completed.\n\n");
            Console.ReadLine();
        }

        private static void SavetoFile(Importer bulkUpload)
        {
            //Converts to JSON from the bulkUpload object and outputs to json file.
            string saveToFileLocation = @"C:\Users\stuart.wighton\Downloads\Upload_Test.json";
            File.WriteAllText(saveToFileLocation, JsonConvert.SerializeObject(bulkUpload, Formatting.Indented));
        }

        private static void GigyaCreateAccount(String siteUID, String email)
        {
            string apiKey = ConfigurationManager.AppSettings["apiKey"];
            string secretKey = ConfigurationManager.AppSettings["secretKey"];

            #region GetToken 
            // Get token from Gigya to allow us to create accounts.
            string regToken = "";
            GSRequest requestGetToken = new GSRequest(apiKey, secretKey, "accounts.initRegistration", true);
            Console.WriteLine("Getting token...\n");
            GSResponse responseToGetToken = requestGetToken.Send();
            // Check what happened
            if (responseToGetToken.GetErrorCode() == 0)
            {
                // SUCCESS! response status = OK  
                Console.WriteLine("Success in responseToGetToken operation.\n");
            }
            else
            {
                // Error
                Console.WriteLine("Got error on responseToGetToken: {0}\n", responseToGetToken.GetLog());
            }

            GSObject tokenObject = new GSObject();
            tokenObject = responseToGetToken.GetData();
            regToken = tokenObject.GetString("regToken");
            //DebugToConsole(regToken);
            #endregion

            #region Register Account
            // Setup hardcoded account using regToken
            Console.WriteLine("Setting profile details...\n");
            GSObject gigyaProfile = new GSObject();
            gigyaProfile.Put("firstName", "Created by");
            gigyaProfile.Put("lastName", "MS Visual Studio");
            GSObject gigyaData = new GSObject();
            gigyaData.Put("portal.clubPortal", "1");
            gigyaData.Put("terms", "true");
            GSObject gigyaNewUser = new GSObject();
            gigyaNewUser.Put("siteUID", siteUID);
            gigyaNewUser.Put("email", email);
            gigyaNewUser.Put("password", "Test11!!");
            gigyaNewUser.Put("regToken", regToken);
            gigyaNewUser.Put("finalizeRegistration", "true");
            //gigyaNewUser.Put("lastLogin", "");
            //gigyaNewUser.Put("lastLoginTimestamp", "");
            gigyaNewUser.Put("profile", gigyaProfile);
            gigyaNewUser.Put("data", gigyaData);
            // Setup call to Gigya and send request 
            Console.WriteLine("Request new account to be created...\n");
            GSRequest gigyaNewUserRequest = new GSRequest(apiKey, secretKey, "accounts.register", gigyaNewUser, true);
            GSResponse gigyaNewUserResponse = gigyaNewUserRequest.Send();

            // Check what happened
            if (gigyaNewUserResponse.GetErrorCode() == 0)
            {
                // SUCCESS! response status = OK  
                Console.WriteLine("Success in gigyaNewUserResponse operation.\n");
            }
            else if (gigyaNewUserResponse.GetErrorCode() == 400009)
            {
                // Error! response status = Email Address Already Exists
                Console.WriteLine("Email address already exists.\n");
            }
            else if (gigyaNewUserResponse.GetErrorCode() == 403002 ||
                     gigyaNewUserResponse.GetErrorCode() == 403025)
            {
                // Error! response status = Token has expired or is invalid
                Console.WriteLine("Token has expired or is invalid, try again.\n");
            }
            else
            {
                // Error
                Console.WriteLine("Got error on gigyaNewUserResponse: {0}\n", gigyaNewUserResponse.GetLog());
            }
            //DebugToConsole(gigyaNewUserResponse.ToString());
            //Console.ReadLine();
            #endregion
        }

        private static void GigyaGetAccountInfo(string apiKey, string secretKey)
        {
            // Get the user's Gigya account info
            string method = "accounts.getAccountInfo";
            GSRequest request = new GSRequest(apiKey, secretKey, method, true);
            request.SetParam("uid", "20160823BC001380");
            GSResponse response = request.Send();

            //Debug to display content on console
            Console.WriteLine(response.GetResponseText());
            Console.ReadLine();
        }

        #region Debug to Console 
        private static void DebugToConsole(String messageToShow)
        {
            //Debug to display content on console
            Console.WriteLine(messageToShow + "\n");
            Console.ReadLine();
        }
        #endregion
    }
}
