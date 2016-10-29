using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Console
{
    public class HashSettings
    {
        public string algorithm { get; set; }

        public HashSettings()
        {
            this.algorithm = "sha512";
        }
    }
}
