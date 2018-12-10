using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace Automatyzacja
{
    public class Class1 : IDisposable
    {
        private IWebDriver browser;

        public Class1() => browser = new ChromeDriver();
        public void Dispose() => browser.Quit();

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

        [Fact]
        public void ExampleTest()
        {
            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin");
            WaitForClickable(By.Id("user_login"), 5);
            browser.FindElement(By.Id("user_login")).SendKeys("automatyzacja");
            WaitForClickable(By.Id("user_pass"), 5);
            browser.FindElement(By.Id("user_pass")).SendKeys("jesien2018");
            WaitForClickable(By.Id("wp-submit"), 5);
            browser.FindElement(By.Id("wp-submit")).Click();

        }
    }
}
