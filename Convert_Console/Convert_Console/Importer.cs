using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Console
{
    public class Importer
    {
        public Settings settings { get; set; }
        public List<Account> accounts { get; set; }

        public Importer()
        {
            settings = new Settings();
            accounts = new List<Account>();

        }
    }
}
