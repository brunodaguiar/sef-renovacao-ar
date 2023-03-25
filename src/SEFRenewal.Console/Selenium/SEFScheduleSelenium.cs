using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SEFRenewal.Console.Configurations;

namespace SEFRenewal.Console.Selenium
{
    public class SEFScheduleSelenium : BaseSelenium
    {
        public Configuration Configuration { get; set; }
        public SEFScheduleSelenium() : base(new FirefoxDriver())
        {
            Configuration = new();
        }

        public void Start()
        {
            driver.Navigate().GoToUrl(Configuration.HomeUrl);

            Login();

            driver.Navigate().GoToUrl("https://www.sef.pt/pt/mySEF/Pages/default.aspx");

            if (!IsLoggedIn()) throw new Exception("Login error");

            IsRenewalScheduleAvailable();

            driver.Quit();
            driver.Dispose();
        }

        private bool IsRenewalScheduleAvailable()
        {
            IWebElement renewalLink = driver.FindElement(By.Id("renovacaoAutomaticaLink"));

            renewalLink.Click();

            RenewalAuthenticattion();

            IWebElement error = driver.FindElement(By.ClassName("error-row"));

            string errorValue = error.Text;
            if (errorValue != string.Empty) return false;

            return true;
        }

        private void RenewalAuthenticattion()
        {
            IWebElement passwordInput = driver.FindElement(By.Id("txtAuthPanelPassword"));
            IWebElement documentIdInput = driver.FindElement(By.Id("txtAuthPanelDocument"));
            IWebElement authenticationSubmit = driver.FindElement(By.Id("btnAutenticaUtilizador"));

            if (passwordInput == null || documentIdInput == null || authenticationSubmit == null) 
                throw new Exception("Renewal Authentication error");

            passwordInput.SendKeys(Configuration.Credentials.Password);
            documentIdInput.SendKeys(Configuration.ResidencePermit.Id);

            authenticationSubmit.Click();

            //Wait            
        }

        private bool IsLoggedIn()
        {
            IWebElement logoutSpan = driver.FindElement(By.Id("ctl00_ucLoginMenu_Logout"));

            return logoutSpan.Displayed;
        }

        private void Login()
        {
            IWebElement loginExpandLink = driver.FindElement(By.Id("pnlLogin")).FindElement(By.TagName("a"));
            loginExpandLink?.Click();

            IWebElement usernameInput = driver.FindElement(By.Id("txtUsername"));
            IWebElement passwordInput = driver.FindElement(By.Id("txtPassword"));
            IWebElement loginSubmit = driver.FindElement(By.Id("btnLogin"));

            if (usernameInput == null || passwordInput == null || loginSubmit == null) throw new Exception("Login error");

            usernameInput.SendKeys(Configuration.Credentials.User);
            passwordInput.SendKeys(Configuration.Credentials.Password);

            loginSubmit.Click();
        }
    }
}
