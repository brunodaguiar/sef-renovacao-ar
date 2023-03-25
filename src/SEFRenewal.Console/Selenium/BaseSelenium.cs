using OpenQA.Selenium;

namespace SEFRenewal.Console.Selenium
{
    public abstract class BaseSelenium
    {
        protected IWebDriver driver;

        public BaseSelenium(IWebDriver driver)
        {
            this.driver = driver;
        }

        public BaseSelenium(IWebDriver driver, string startUrl)
        {
            this.driver = driver;

            driver.Navigate().GoToUrl(startUrl);
        }
    }
}
