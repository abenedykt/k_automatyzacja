using System;
using OpenQA.Selenium;

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
            throw new NotImplementedException();
        }

        internal bool IsAt()
        {
            return browser.FindElement(By.Id("user_login")) != null &&
                browser.FindElement(By.Id("user_pass")) != null;
        }
    }
}