namespace Convert_Console
{
    public class Account
    {
        public string siteUID { get; set; }
        public string email { get; set; }
        public Profile profile { get; set; }
        public Password password { get; set; }
        public Data data { get; set; }

        public Account()
        {
            this.profile = new Profile();
            this.password = new Password();
        }
    }
}
