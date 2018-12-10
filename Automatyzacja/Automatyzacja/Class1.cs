using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
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
            browser.FindElements(By.CssSelector(".wp-menu-name")).Single(x => x.Text == "Wpisy").Click();
            browser.FindElements(By.CssSelector(".wp-submenu > li")).Single(x => x.Text == "Dodaj nowy").Click();
            browser.FindElement(By.Id("title-prompt-text")).Click();
            browser.FindElement(By.Id("title")).SendKeys(Faker.Lorem.Sentence());

            WaitForClickable(By.Id("publish"), 20);
            browser.FindElement(By.Id("content-html")).Click();
            browser.FindElement(By.Id("content")).SendKeys(Faker.Lorem.Paragraph());
            browser.FindElement(By.Id("publish")).Click();

            WaitForClickable(By.Id("publish"), 20);

            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);
            String url = browser.FindElement(By.CssSelector("#sample-permalink > a")).GetAttribute("href").ToString();

            var element = browser.FindElement(By.Id("wp-admin-bar-my-account"));
            Actions builder = new Actions(browser);
            builder.MoveToElement(element).Perform();
            browser.FindElement(By.Id("wp-admin-bar-logout")).Click();
        }
    }
}
