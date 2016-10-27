using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Common;
using Newtonsoft.Json;
using System.IO;


namespace Convert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            // Setting up source local, sheet name and output local
            var pathToExcel = @"C:\Egnyte\Shared\ONETEAM GLOBAL FILES\LMI\IT\Projects\1.Projects in progress\SSO\7.Project Phases\3. DataMigration\Brand_Machine\Nordic\Rollout 2.xlsx";
            var sheetName = "INPUT SHEET";
            var destinationPath = @"C:\Users\stuart.wighton\Downloads\Nordic_Test.json";
            // Setup connection string using Access 2010 thing...
            var connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES""", pathToExcel);

            //Creating and opening a data connection to the Excel sheet 
            using (var dbConnection = new OleDbConnection(connectionString))
            {
                dbConnection.Open();

                var dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = String.Format(@"SELECT * FROM [{0}$]", sheetName);

                using (var dbReader = dbCommand.ExecuteReader())
                {

                    //LINQ query - when executed will create anonymous objects for each row
                    var query =
                        from DbDataRecord column in dbReader

                        select new
                        {
                            email = column[5],
                            firstName = column[3],
                            lastName = column[4],

                        };

                    //Generates JSON from the LINQ query
                    var json = JsonConvert.SerializeObject(query);

                    //Write the file to the destination path    
                    File.WriteAllText(destinationPath, json);

                    Application.Exit();
                }
            }
        }
    }
}

