using OpenQA.Selenium;

namespace PageObjectExample
{
    internal class LoginPage : BasePage
    {
    //private IWebDriver browser;
    public LoginPage(IWebDriver browser): base(browser)
        {
            //this.browser = browser;
            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin");
        }

    internal KokpitPage Login(string userName, string password)
            {
                WaitForClickable(By.Id("user_login"), 5);
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
   

    
        //public LoginPage(IWebDriver browser)
        //{
        //    this.browser = browser;
        //    browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin");
        //}

        //internal KokpitPage Login(string userName, string password)
        //{
        //    WaitForClickable(By.Id("user_login"), 5);
        //    var userLogin = browser.FindElement(By.Id("user_login"));
        //    userLogin.SendKeys(userName);

        //    WaitForClickable(By.Id("user_pass"), 5);
        //    var passwordElement = browser.FindElement(By.Id("user_pass"));
        //    passwordElement.SendKeys(password);

        //    WaitForClickable(By.Id("wp-submit"), 5);
        //    var login = browser.FindElement(By.Id("wp-submit"));
        //    login.Click();

        //    return new KokpitPage(browser);
        //}

        internal override bool IsAt()
        {
            return browser.FindElement(By.Id("user_login")) != null &&
                   browser.FindElement(By.Id("user_pass")) != null;
            //lub 
            //if (browser.FindElement(By.Id("user_login")) != null &&
            //    browser.FindElement(By.Id("user_pass")) != null)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}