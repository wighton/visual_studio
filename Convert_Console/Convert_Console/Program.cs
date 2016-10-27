using System;
using System.IO;
using Newtonsoft.Json;
using CsvHelper;

namespace Convert_Console
{
    class Program
    {
        static void Main(string[] args)
        {

            // File bulk import file, read in to system and parse via Csv Reader.
            var readContentFilePath = @"C:\Users\stuart.wighton\Downloads\Rollout_2.csv";
            try
            {
                StreamReader streamReader = new StreamReader(readContentFilePath);
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

                //Converts to JSON from the bulkUpload object and outputs to json file.
                var json = JsonConvert.SerializeObject(bulkUpload, Formatting.Indented);
                var destinationPath = @"C:\Users\stuart.wighton\Downloads\Nordic_Test2.json";
                File.WriteAllText(destinationPath, json);

                //Debug to display content on console
                Console.WriteLine(json);
                Console.ReadLine();

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
    }
}
