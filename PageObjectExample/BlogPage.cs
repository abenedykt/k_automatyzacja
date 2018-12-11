using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Xunit;

namespace PageObjectExample
{
    internal class BlogPage : BasePage
    {
        public BlogPage(IWebDriver browser) : base(browser)
        {
            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net");
            WaitForClickable(By.ClassName("site-header-main"), 5);
            var comments = browser.FindElement(By.ClassName("comments-link"));
            comments.Click();
        }

        internal override bool IsAt()
        {
            return browser.FindElement(By.ClassName("comment-reply-title")) != null;
        }

        internal void AddNewComment(string exampleComment, string exampleAuthor, string exampleEmail)
        {
            var comment = browser.FindElement(By.Id("comment"));
            comment.Click();
            comment.SendKeys(exampleComment);

            ScrollToElement(By.Id("author"));
            var newAuthor = browser.FindElement(By.Id("author"));
            newAuthor.SendKeys(exampleAuthor);

            ScrollToElement(By.Id("email"));
            var email = browser.FindElement(By.Id("email"));
            email.SendKeys(exampleEmail);

            ScrollToElement(By.ClassName("meta-nav"));
            var submit = browser.FindElement(By.Id("submit"));
            submit.Click();
        }

        internal IEnumerable<string> Comments()
        {
            return browser.FindElements(By.ClassName("comment-content")).Select(x => x.Text);
        }
    }
}