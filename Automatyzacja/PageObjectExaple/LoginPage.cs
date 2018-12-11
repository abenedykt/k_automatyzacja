using System;
using OpenQA.Selenium;

namespace PageObjectExample
{
    internal class LoginPage
    {
        private IWebDriver browser;

        public LoginPage(IWebDriver browser)
        {
            this.browser = browser;
        }

        internal KokpitPage Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        internal bool IsAt()
        {
            throw new NotImplementedException();
        }
    }
}