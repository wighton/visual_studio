using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Console
{
    public class Settings
    {
        public string importFormat { get; set; }
        public string apiKey { get; set; }
        public bool finalizeRegistration { get; set; }
        public bool skipVerification { get; set; }
        public int totalRecords { get; set; }

        public Settings()
        {
            importFormat = "gigya-­raas-­import";
            apiKey = "TBC";
            finalizeRegistration = true;
            skipVerification = true;
        }
    }
}
