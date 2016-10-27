using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Convert_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Account account = new Account();

            //account.UID = "StringUIDSting";
            //account.HashedPassword = "StringhashedPasswordString";
            //account.PwHashAlgorithm = "StringpwHashAlgorithmString";
            //account.Email = "test@someithng.com";
            //account.Profile.FirstName = "testFirstName";
            //account.Profile.LastName = "testLastName";

            //Importer importer = new Importer();
            //importer.accounts.UID = "";
            //importer.accounts.hashedPassword = "StringhashedPasswordString";
            //importer.accounts.pwHashAlgorithm = "StringpwHashAlgorithmString";
            //importer.accounts.email = "test@someithng.com";
            //importer.accounts.Profile.firstName = "testFirstName";
            //importer.accounts.Profile.lastName = "testLastName";

            Importer importer = new Importer
            {
                settings = new Settings { },
                accounts = new List<Account>()
                {
                    new Account {
                        UID = "UID 1",
                        hashedPassword = "StringhashedPasswordString",
                        pwHashAlgorithm = "StringpwHashAlgorithmString",
                        email = "test@someithng.com",
                        profile = new Profile
                        {
                            firstName = "testFirstName",
                            lastName = "testLastName"
                        }
                    },
                    new Account {
                        UID = "UID 2",
                        hashedPassword = "StringhashedPasswordString",
                        pwHashAlgorithm = "StringpwHashAlgorithmString",
                        email = "test@someithng.com",
                        profile = new Profile
                        {
                            firstName = "testFirstName",
                            lastName = "testLastName"
                        }
                    }
                }


                //account2 = new Account
                //{
                //    UID = "UID 2",
                //    hashedPassword = "2 StringhashedPasswordString",
                //    pwHashAlgorithm = "2 StringpwHashAlgorithmString",
                //    email = "2_test@someithng.com",
                //    profile = new Profile
                //    {
                //        firstName = "2 testFirstName",
                //        lastName = "2 testLastName"
                //    }
                //}
            };


            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Importer));
            ser.WriteObject(stream1, importer);

            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            Console.Write("JSON form of Person object: ");
            Console.WriteLine(sr.ReadToEnd());

            //Generates JSON from the LINQ query
            var json = JsonConvert.SerializeObject(importer, Formatting.Indented);
            var destinationPath = @"C:\Users\stuart.wighton\Downloads\Nordic_Test2.json";

            //Write the file to the destination path    
            File.WriteAllText(destinationPath, json);


            Console.ReadLine();

        }
    }
}
