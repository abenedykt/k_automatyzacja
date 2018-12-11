using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automatyzacja
{
    internal class LoginPage
    {
        private IWebDriver browser;

        public LoginPage(IWebDriver browser)
        {
            this.browser = browser;
            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin");
        }

        internal KokpitPage Login(string user, string pass)
        {
            WaitForClickable(By.Id("user_login"), 5);
            browser.FindElement(By.Id("user_login")).SendKeys(user);

            WaitForClickable(By.Id("user_pass"), 5);
            browser.FindElement(By.Id("user_pass")).SendKeys(pass);

            WaitForClickable(By.Id("wp-submit"), 5);
            browser.FindElement(By.Id("wp-submit")).Click();

            return new KokpitPage(browser);
        }

        internal bool IsAt() => browser.FindElement(By.Id("user_login")) != null &&
                browser.FindElement(By.Id("user_pass")) != null;

        private void WaitForClickable(By by, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }
        private void WaitForClickable(IWebElement element, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }
    }
}