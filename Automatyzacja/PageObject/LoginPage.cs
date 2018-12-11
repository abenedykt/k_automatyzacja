using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PageObjectTest
{
    internal class LoginPage
    {
        private IWebDriver browser;

        public LoginPage(IWebDriver browser)
        {
            this.browser = browser;
            browser.Navigate().GoToUrl("https://automatyzacja.benedykt.net/wp-admin");
        }

        internal KokpitPage Login(string username, string password)
        {
            WaitForClickable(By.Id("user_login"), 5);
            var userLogin = browser.FindElement(By.Id("user_login"));
            userLogin.SendKeys(username);
            WaitForClickable(By.Id("user_pass"), 5);
            var userPass = browser.FindElement(By.Id("user_pass"));
            userPass.SendKeys(password);
            WaitForClickable(By.Id("wp-submit"), 5);
            var login = browser.FindElement(By.Id("wp-submit"));
            login.Click();
            return new KokpitPage(browser);
        }

        internal bool IsAt()
        {
            return browser.FindElement(By.Id("user_login")) != null &&
            browser.FindElement(By.Id("user_pass")) != null;
        }

        public void WaitForClickable(By by, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }
        public void WaitForClickable(IWebElement element, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }
    }
}