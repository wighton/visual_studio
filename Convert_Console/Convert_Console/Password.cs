using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Console
{
    public class Password
    {
        public string hash { get; set; }
        public HashSettings hashSettings { get; set; }

        public Password()
        {
            this.hash = "5110AF5AE2CB887B2028B9F28A791DDB91EACE2B077ADB0CED4D2964598681C7CCE34DBF2D2234FEF34BBFA7DFA79F2943CE707D7816F4CA286FDAC63E1B18D9";
            this.hashSettings = new HashSettings();

        }

    }
}
