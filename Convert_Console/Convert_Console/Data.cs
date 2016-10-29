namespace Convert_Console
{
    public class Data
    {
        public bool terms { get; set; }
        public int portal_clubPortal { get; set; }
        public int portal_DKP { get; set; }
        public int portal_IP { get; set; }


        public Data()
        {
            this.terms = true;
            this.portal_clubPortal = 0;
            this.portal_DKP = 0;
            this.portal_IP = 0;
        }
    }
}
