namespace SEFRenewal.Console.Configurations
{
    public class Configuration
    {
        public Configuration()
        {
            // I'll move this to a settings file...eventually...
            HomeUrl = "https://www.sef.pt/pt/Pages/pre-marcacao-online.aspx";
            Credentials = new PasswordCredentials
            {
                Password = "",
                User = "",
            };
            ResidencePermit = new ResidencePermit
            {
                Id = ""
            };
        }

        public string HomeUrl { get; set; }
        public PasswordCredentials Credentials { get; set; }

        public ResidencePermit ResidencePermit { get; set; }
    }

    public class ResidencePermit
    {
        public string Id { get; set; }
    }

    public class PasswordCredentials
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
