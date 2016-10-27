using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using CsvHelper;
using System.Diagnostics;

namespace Convert_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Importer contentObject = ReadContentFromFile();
            SaveJsonObjectFile(contentObject);
        }

        private static Importer ReadContentFromFile()
        {
            // Build importer object to match scheme
            Importer importer = new Importer();
            importer.settings = new Settings();
            importer.accounts = new List<Account>();

            // Read the file and populate object
            var readContentFilePath = @"C:\Users\stuart.wighton\Downloads\Rollout_2.csv";
            StreamReader streamReader = new StreamReader(readContentFilePath);
            var csvReader = new CsvReader(streamReader);
            int count = 0;
            while (csvReader.Read())
            {
                importer.accounts.Add(new Account
                {
                    UID = "UID " + count,
                    email = csvReader.GetField<string>(5),
                    profile = new Profile
                    {
                        firstName = csvReader.GetField<string>(3),
                        lastName = csvReader.GetField<string>(4)
                    }
                });
                count++;
            }
            return importer;
        }

        private static void SaveJsonObjectFile(Importer contentObject)
        {
            //Converts to JSON from the streaming object and outputs to file.
            var json = JsonConvert.SerializeObject(contentObject, Formatting.Indented);
            var destinationPath = @"C:\Users\stuart.wighton\Downloads\Nordic_Test2.json";
            File.WriteAllText(destinationPath, json);

            //Debug to display content on console
            Console.WriteLine(json);
            Console.ReadLine();
        }
    }
}
