using System.Collections.Generic;

namespace Convert_Console
{
    public class Importer
    {
        public Settings settings { get; set; }
        public List<Account> accounts { get; set; }

        public Importer()
        {
            this.settings = new Settings();
            this.accounts = new List<Account>();

        }
    }
}
