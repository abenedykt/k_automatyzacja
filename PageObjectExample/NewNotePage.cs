using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace PageObjectExample
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

        internal Uri Publish(string exampleTitle, string exampleContent)
        {
            var newTitle = browser.FindElement(By.Id("title-prompt-text"));
            newTitle.Click();
            var inputTitle = browser.FindElement(By.Id("title"));

            inputTitle.SendKeys(exampleTitle);

            WaitForClickable(By.Id("publish"), 5);

            browser.FindElement(By.Id("content-html")).Click();
            // sprawdzamy czy sa buttony - bo inaczej nie dziala
            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.Id("edit-slug-buttons"), 5);
            //
            var content = browser.FindElement(By.Id("content"));
            content.Click();

            content.SendKeys(exampleContent);

            WaitForClickable(By.Id("publish"), 5);

            var publishButton = browser.FindElement(By.Id("publish"));
            publishButton.Click();


            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.Id("edit-slug-buttons"), 5);

            var editButton = browser.FindElement(By.Id("edit-slug-buttons"));

            //editButton.Click();
            var postUrl = browser.FindElement(By.CssSelector("#sample-permalink > a"));
            var url = postUrl.GetAttribute("href");

            return new Uri(url);
        }

        internal void Logout()
        {
            MoveToElement(By.Id("wp-admin-bar-top-secondary"));
            WaitForClickable(By.Id("wp-admin-bar-logout"), 5);
            var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
            logout.Click();
            WaitForClickable(By.Id("nav"), 5);
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