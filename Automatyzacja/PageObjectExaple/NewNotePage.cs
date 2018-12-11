using System;
using OpenQA.Selenium;

namespace PageObjectExample
{
    internal class NewNotePage: BasePage
    {
        public NewNotePage(IWebDriver browser):base(browser)
        {
        }

        internal override bool IsAt()
        {
            return browser.Title.StartsWith("Dodaj nowy wpis");
        }

        internal Uri Publish(string title, string content)
        {
            var noteTitle = browser.FindElement(By.Id("title-prompt-text"));
            noteTitle.Click();
            var titleElement = browser.FindElement(By.Id("title"));
            var exampleTitle = Faker.Lorem.Sentence();
            titleElement.SendKeys(title);

            browser.FindElement(By.Id("content-html")).Click();

            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);


            var contentElement = browser.FindElement(By.Id("content"));
            contentElement.SendKeys(content);

            var publishButton = browser.FindElement(By.Id("publish"));
            publishButton.Click();

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

        
    }
}