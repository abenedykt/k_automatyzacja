using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace PageObjectExample
{
    internal class newNotePage : BasePage
    {
        public newNotePage(IWebDriver browser) : base(browser)
        {
        }

        internal bool IsAt()
        {
            return browser.Title.StartsWith("Dodaj nowy wpis");
        }

        internal Uri Publish(string exampleTitle, string exampleContent)
        {
            var noteTitle = browser.FindElement(By.Id("title-prompt-text"));
            noteTitle.Click();
            var title = browser.FindElement(By.Id("title"));
            title.SendKeys(exampleTitle);

            browser.FindElement(By.Id("content-html")).Click();
            //czekamy aż buttony będą aktywne
            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);
            var content = browser.FindElement(By.Id("content"));
            content.SendKeys(exampleContent);

            var publishBotton = browser.FindElement(By.Id("publish"));
            publishBotton.Click();

            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);
            var postUrl = browser.FindElement(By.CssSelector("#sample-permalink > a"));
            var url = postUrl.GetAttribute("href");

            return new Uri(url);
        }

        internal void Logout()
        {
            MoveToElement(By.Id("wp-admin-bar-my-account"));

            WaitForClickable(By.Id("wp-admin-bar-logout"), 5);

            var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
            logout.Click();

        }
        //private void WaitForClickable(By by, int seconds)
        //{
        //    var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
        //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        //}
        //private void WaitForClickable(IWebElement element, int seconds)
        //{
        //    var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
        //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        //}
        //private void MoveToElement(By selector)
        //{
        //    var element = browser.FindElement(selector);
        //    MoveToElement(element);
        //}
        //private void MoveToElement(IWebElement element)
        //{
        //    Actions builder = new Actions(browser);
        //    Actions moveTo = builder.MoveToElement(element);
        //    moveTo.Build().Perform();
        //}
    }
}