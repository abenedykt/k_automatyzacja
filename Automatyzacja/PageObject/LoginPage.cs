using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PageObjectTest
{
    internal class LoginPage :BasePage
    {
        public LoginPage(IWebDriver browser):base(browser)
        {
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
        internal override bool IsAt()
        {
           return browser.FindElement(By.Id("user_login")) != null &&
                    browser.FindElement(By.Id("user_pass")) != null;
        }
    }
}