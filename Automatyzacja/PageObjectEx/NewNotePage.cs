using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace PageObjectEx
{
    internal class NewNotePage : BasePage
    {
        public NewNotePage(IWebDriver browser) : base(browser)
        {
        }


        internal Uri Publish(string title, string content)
        {
            var noteTitle = browser.FindElement(By.Id("title-prompt-text"));
            noteTitle.Click();
            var titleElement = browser.FindElement(By.Id("title"));
            titleElement = browser.FindElement(By.Id("title"));
            titleElement.SendKeys(title);

            var textButton = browser.FindElement(By.Id("content-html"));
            textButton.Click();
            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector("#edit-slug-buttons > button"), 5);
            var contentElement = browser.FindElement(By.Id("content"));
            contentElement.SendKeys(content);

            var publish = browser.FindElement(By.Id("publish"));
            WaitForClickable(By.Id("publish"), 5);
            publish.Click();
            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector("#edit-slug-buttons > button"), 5);
            var link = browser.FindElement(By.CssSelector("#sample-permalink > a"));
            string url = link.GetAttribute("href");

            return new Uri(url);
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

        internal void Logout()
        {
            MoveToElement(By.Id("wp-admin-bar-my-account"));
            WaitForClickable(By.Id("wp-admin-bar-logout"), 5);

            var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
            logout.Click();

            Assert.NotNull(browser.FindElement(By.Id("user_login")));
            Assert.NotNull(browser.FindElement(By.Id("user_pass")));
        }



        internal override bool IsAt()
        {
            return browser.Title.StartsWith("Dodaj nowy wpis");
        }
    }
}