using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace Convert_Console
{
    public sealed class MyClassMap : CsvClassMap<Importer>
    {
        public MyClassMap()
        {
            Map(m => m.settings.importFormat).Ignore();
            Map(m => m.settings.apiKey).Ignore();
            Map(m => m.settings.finalizeRegistration).Ignore();
            Map(m => m.settings.skipVerification).Ignore();
            Map(m => m.settings.totalRecords).Ignore();

            //Map(m => m.accounts.UID).Ignore();
            //Map(m => m.accounts.hashedPassword).Ignore();
            //Map(m => m.accounts.pwHashAlgorithm).Ignore();
            //Map(m => m.accounts.email).Index(6);

            //Map(m => m.accounts.profile.firstName).Index(4);
            //Map(m => m.accounts.profile.lastName).Index(5);

        }
    }

}
