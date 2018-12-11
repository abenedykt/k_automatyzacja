using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Automatyzacja
{
    internal class NewNotePage
    {
        private IWebDriver browser;

        public NewNotePage(IWebDriver browser) => this.browser = browser;

        internal bool IsAt() => browser.Title.StartsWith("Dodaj nowy wpis");

        internal Uri Publish(string title, string content)
        {
            browser.FindElement(By.Id("title-prompt-text")).Click();
            browser.FindElement(By.Id("title")).SendKeys(title);

            browser.FindElement(By.Id("content-html")).Click();
            WaitForClickable(By.Id("publish"), 20);
            browser.FindElement(By.Id("content")).SendKeys(content);
            browser.FindElement(By.Id("publish")).Click();
            WaitForClickable(By.Id("publish"), 20);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);
            return new Uri(browser.FindElement(By.CssSelector("#sample-permalink > a")).GetAttribute("href"));

        }

        internal void Logout()
        {
            MoveToElement(browser.FindElement(By.Id("wp-admin-bar-my-account")));
            var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
            WaitForClickable(logout, 10);
            logout.Click();
        }

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
        private void MoveToElement(By selector)
        {
            var element = browser.FindElement(selector);
            MoveToElement(element);
        }
        private void MoveToElement(IWebElement element)
        {
            Actions builder = new Actions(browser);
            Actions moveTo = builder.MoveToElement(element);
            moveTo.Build().Perform();
        }


    }
}