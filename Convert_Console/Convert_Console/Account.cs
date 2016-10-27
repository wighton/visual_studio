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
            hashedPassword = "5110AF5AE2CB887B2028B9F28A791DDB91EACE2B077ADB0CED4D2964598681C7CCE34DBF2D2234FEF34BBFA7DFA79F2943CE707D7816F4CA286FDAC63E1B18D9";
            pwHashAlgorithm = "sha512";
        }
    }
}
