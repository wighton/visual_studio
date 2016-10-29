using System.Configuration;

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

            this.importFormat = "gigya-­raas-­import";
            this.apiKey = ConfigurationManager.AppSettings["apiKey"];
            this.finalizeRegistration = true;
            this.skipVerification = true;
        }
    }
}
