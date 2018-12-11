using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PageObjectEx
{
    internal class LoginPage : BasePage
    {

        public LoginPage(IWebDriver browser) : base(browser)
        {
            browser.Navigate().GoToUrl("https://automatyzacja.benedykt.net/wp-admin");
        }


        internal KokpitPage Login(string userName, string password)
        {
            WaitForClickable(By.Id("user_login"),5);
            var userLogin = browser.FindElement(By.Id("user_login"));
            userLogin.SendKeys(userName);

            WaitForClickable(By.Id("user_pass"), 5);
            var passwordElement = browser.FindElement(By.Id("user_pass"));
            passwordElement.SendKeys(password);

            WaitForClickable(By.Id("wp-submit"), 5);
            var login = browser.FindElement(By.Id("wp-submit"));
            login.Click();

            return new KokpitPage(browser);

        }

        internal override bool IsAt()
        {
            return browser.FindElement(By.Id("user_login")) != null &&
                 browser.FindElement(By.Id("user_pass")) != null;
        }


    }
}