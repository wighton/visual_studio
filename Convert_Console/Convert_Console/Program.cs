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
            GigyaCreateAccount();
            return;

            // File bulk import file, read in to system and parse via Csv Reader.
            try
            {
                string readFromFileLocation = @"C:\Users\stuart.wighton\Downloads\Rollout_2.csv";
                StreamReader streamReader = new StreamReader(readFromFileLocation);
                var parsedImportFile = new CsvReader(streamReader);

                // Intiate bulkUpload object and populate with parsed file data.
                Importer bulkUpload = new Importer();
                int count = 1;
                while (parsedImportFile.Read())
                {
                    bulkUpload.accounts.Add(new Account
                    {
                        UID = "UID" + count,
                        email = parsedImportFile.GetField<string>(5),
                        profile = new Profile
                        {
                            firstName = parsedImportFile.GetField<string>(3),
                            lastName = parsedImportFile.GetField<string>(4)
                        }

                    });
                    bulkUpload.settings.totalRecords = count;
                    count++;

                } // End of parsed file read.

                // Save bulkUpload object to file
                //SavetoFile(bulkUpload);
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

        }

        private static void SavetoFile(Importer bulkUpload)
        {
            //Converts to JSON from the bulkUpload object and outputs to json file.
            string saveToFileLocation = @"C:\Users\stuart.wighton\Downloads\Nordic_Test2.json";
            File.WriteAllText(saveToFileLocation, JsonConvert.SerializeObject(bulkUpload, Formatting.Indented));
        }

        private static void GigyaCreateAccount()
        {
            string apiKey = ConfigurationManager.AppSettings["apiKey"];
            string secretKey = ConfigurationManager.AppSettings["secretKey"];

            #region GetToken 
            // Get token from Gigya to allow us to create accounts.
            string regToken = "";
            GSRequest requestGetToken = new GSRequest(apiKey, secretKey, "accounts.initRegistration", true);
            GSResponse responseToGetToken = requestGetToken.Send();
            GSObject tokenObject = new GSObject();
            tokenObject = responseToGetToken.GetData();
            regToken = tokenObject.GetString("regToken");
            //DebugToConsole(regToken);
            #endregion

            #region Register Account
            // Setup hardcoded account using regToken
            GSObject gigyaProfile = new GSObject();
            gigyaProfile.Put("firstName", "Created by");
            gigyaProfile.Put("lastName", "MS Visual Studio");
            GSObject gigyaData = new GSObject();
            gigyaData.Put("terms", "true");
            GSObject gigyaNewUser = new GSObject();
            gigyaNewUser.Put("siteUID", "TestUID201610050930");
            gigyaNewUser.Put("email", "lmiSSOtestFromVS@mailinator.com");
            gigyaNewUser.Put("password", "Test11!!");
            gigyaNewUser.Put("regToken", regToken);
            gigyaNewUser.Put("finalizeRegistration", "true");
            gigyaNewUser.Put("profile", gigyaProfile);
            gigyaNewUser.Put("data", gigyaData);
            // Setup call to Gigya and send request 
            GSRequest gigyaNewUserRequest = new GSRequest(apiKey, secretKey, "accounts.register", gigyaNewUser, true);
            GSResponse gigyaNewUserResponse = gigyaNewUserRequest.Send();

            // Check what happened
            if (gigyaNewUserResponse.GetErrorCode() == 0)
            {
                // SUCCESS! response status = OK  
                Console.WriteLine("Success in setStatus operation.");
            }
            else
            {
                // Error
                Console.WriteLine("Got error on setStatus: {0}", gigyaNewUserResponse.GetLog());
            }
            DebugToConsole(gigyaNewUserResponse.ToString());
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
            Console.WriteLine(messageToShow);
            Console.ReadLine();
        }
        #endregion
    }
}
