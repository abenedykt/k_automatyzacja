using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace PageObjectExample
{
    internal class NewNotePage : BasePage
    {

        public NewNotePage(IWebDriver browser):base(browser)
        {
        }

        internal override bool IsAt()
        {
            return browser.Title.StartsWith("Dodaj nowy wpis");
        }

        internal Uri Publish (string title, string contetnt)
        {
            var noteTitle = browser.FindElement(By.Id("title-prompt-text"));
            noteTitle.Click();
            var titleelement = browser.FindElement(By.Id("title"));
            
            titleelement.SendKeys(title);

            browser.FindElement(By.Id("content-html")).Click();

            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);


            var content = browser.FindElement(By.Id("content"));
            
            content.SendKeys(contetnt);

            var publishButton = browser.FindElement(By.Id("publish"));
            publishButton.Click();

            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);
            var postUrl = browser.FindElement(By.CssSelector("#sample-permalink > a"));
            var url = postUrl.GetAttribute("href");

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

        }

        
    }
}