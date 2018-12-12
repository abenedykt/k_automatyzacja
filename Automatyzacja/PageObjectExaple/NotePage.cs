using System;
using System.Linq;
using OpenQA.Selenium;

namespace PageObjectExample
{
    internal class NotePage : BasePage
    {
        private readonly Uri newNoteUrl;

        public NotePage(IWebDriver browser, Uri newNoteUrl):base(browser)
        {
            this.newNoteUrl = newNoteUrl;

            browser.Navigate().GoToUrl(newNoteUrl);
        }

        public string Title => browser.FindElement(By.CssSelector(".entry-title")).Text;
        public string Content => browser.FindElement(By.CssSelector(".entry-content")).Text;

        internal override bool IsAt()
        {
            return browser.FindElements(By.CssSelector("body.single-post")).Any();
        }

        internal void AddComment(Comment commentToPublish)
        {
            ScrollToElement(By.Id("email"));
            browser.FindElement(By.Id("comment")).SendKeys(commentToPublish.Text);
            browser.FindElement(By.Id("author")).SendKeys(commentToPublish.Author);
            browser.FindElement(By.Id("email")).SendKeys(commentToPublish.Email);

            ScrollToElement(By.CssSelector("nav"));
            browser.FindElement(By.Id("submit")).Click();
        }

        internal bool HasComment(Comment comment)
        {
            return AuthorIsSet(comment)
                && CommentIsSet(comment);
        }

        private bool CommentIsSet(Comment comment)
        {
            var comments = browser.FindElements(By.CssSelector("li.comment"));

            return comments.Any(c => c.FindElement(By.ClassName("comment-content")).Text == comment.Text);
        }

        private bool AuthorIsSet(Comment comment)
        {
            var comments = browser.FindElements(By.CssSelector("li.comment"));

            return comments.Any(c => c.FindElement(By.CssSelector("b.fn")).Text == comment.Author);
        }
    }
}