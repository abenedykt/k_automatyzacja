using OpenQA.Selenium;

namespace Automatyzacja
{
    internal class LoginPage : BasePage
    {
        public LoginPage(IWebDriver browser) : base(browser) => 
            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin");
        internal override bool IsAt() => browser.FindElement(By.Id("user_login")) != null &&
        browser.FindElement(By.Id("user_pass")) != null;
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
    }
}