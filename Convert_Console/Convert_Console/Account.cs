using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Console
{
    public class Account
    {
        public string UID { get; set; }
        public string hashedPassword { get; set; }
        public string pwHashAlgorithm { get; set; }
        public string email { get; set; }
        public Profile profile { get; set; }

        public Account()
        {
            profile = new Profile();
        }
    }
}
