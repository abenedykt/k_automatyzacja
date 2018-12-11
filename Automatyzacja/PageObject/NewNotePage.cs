using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace PageObjectTest
{
    internal class NewNotePage
    {
        private IWebDriver browser;

        public NewNotePage(IWebDriver browser)
        {
            this.browser = browser;
        }

        internal bool IsAt()
        {
            return browser.Title.StartsWith("Dodaj nowy wpis");
        }

        internal Uri Publish(string title, string content)
        {
            WaitForClickable(By.Id("title"), 5);
            var noteTitle = browser.FindElement(By.Id("title-prompt-text"));
            noteTitle.Click();
            var titleElement = browser.FindElement(By.Id("title"));
            titleElement.SendKeys(title);

            var html2Text = browser.FindElement(By.Id("content-html"));
            html2Text.Click();

            WaitForClickable(By.Id("publish"), 10);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 10);

            var contentElement = browser.FindElement(By.Id("content"));

            contentElement.SendKeys(content);

            WaitForClickable(By.CssSelector("input#publish"), 10);
            var publish = browser.FindElement(By.CssSelector("input#publish"));
            publish.Click();

            var posturl = browser.FindElement(By.CssSelector("#sample-permalink>a"));
            var url = posturl.GetAttribute("href");
            return new Uri(url);
        }

        internal void Logout()
        {
            MoveToElement(By.Id("wp-admin-bar-my-account"));
            WaitForClickable(By.Id("wp-admin-bar-logout"), 10);
            var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
            logout.Click();
            Assert.NotNull(browser.FindElement(By.Id("user_login")));
            Assert.NotNull(browser.FindElement(By.Id("user_pass")));
        }

        public void WaitForClickable(By by, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }
        public void WaitForClickable(IWebElement element, int seconds)
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