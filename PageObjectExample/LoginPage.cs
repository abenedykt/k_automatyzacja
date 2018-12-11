using OpenQA.Selenium;

namespace PageObjectExample
{

    internal class LoginPage : BasePage 
    {
        public LoginPage(IWebDriver browser):base(browser)
        {
            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin");
        }

        internal kokpitPage Login(string userName, string password)
        {
            WaitForClickable(By.Id("user_login"), 5);
            var userLogin = browser.FindElement(By.Id("user_login"));
            userLogin.SendKeys(userName);

            WaitForClickable(By.Id("user_pass"), 5);
            var passwordelement = browser.FindElement(By.Id("user_pass"));
            passwordelement.SendKeys(password);

            WaitForClickable(By.Id("wp-submit"), 5);
            var login = browser.FindElement(By.Id("wp-submit"));
            login.Click();

            return new kokpitPage(browser);

        }

        internal override bool IsAt()
        {
            return browser.FindElement(By.Id("user_login")) != null &&
                browser.FindElement(By.Id("user_pass")) != null;

            /* if (browser.FindElement(By.Id("user_login")) != null &&
            browser.FindElement(By.Id("user_pass")) != null)
            {
            return true;
            } else
            {
            return false;
            }*/
        }

        
    }

    
}