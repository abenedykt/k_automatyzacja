using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Xunit;

namespace PageObjectExample
{
    internal class newCommentPage : BasePage
    {
        public newCommentPage(IWebDriver browser) : base(browser)
        {
            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net");
            WaitForClickable(By.ClassName("site-header-main"), 5);
            var comments = browser.FindElement(By.ClassName("comments-link"));
            comments.Click();
        }

        internal override bool IsAt()
        {
           // return browser.FindElement(By.ClassName("comment-reply-title")) != null; 
           // lub
            return browser.FindElement(By.CssSelector("body.single-post")) != null;
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

        internal void AddAnswearToComment(string exampleAnswearToComment, string originalAuthor, string exampleAuthor2, string exampleEmail2)
        {
            var answearToMyComment = browser.FindElements(By.ClassName("comment-reply-link")).Single(x=>x.GetAttribute("aria-label").EndsWith(originalAuthor));

            answearToMyComment.Click();

            var comment2 = browser.FindElement(By.Id("comment"));
            comment2.Click();
            comment2.SendKeys(exampleAnswearToComment);

            ScrollToElement(By.Id("author"));
            var newAuthor2 = browser.FindElement(By.Id("author"));
            newAuthor2.SendKeys(exampleAuthor2);

            ScrollToElement(By.Id("email"));
            var email2 = browser.FindElement(By.Id("email"));
            email2.SendKeys(exampleEmail2);

            ScrollToElement(By.ClassName("meta-nav"));
            var submit = browser.FindElement(By.Id("submit"));
            submit.Click();

            //answearToMyComment.SendKeys(exampleAnswearToComment);
        }
        internal string AnswearToComment(string exampleAuthor2)
        {
            return browser.FindElements(By.ClassName("comment-body"))
                .Single(y => y.FindElement(By.ClassName("fn")).Text == exampleAuthor2)
                .FindElement(By.ClassName("comment-content"))
                .Text;  
        }
    }
}