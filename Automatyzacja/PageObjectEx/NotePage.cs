using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace PageObjectEx
{
    internal class NotePage : BasePage
    {
       
        
        public Uri newNoteUrl;

        

        public NotePage(IWebDriver browser, Uri newNoteUrl) : base(browser)
        {

            this.newNoteUrl = newNoteUrl;
            browser.Navigate().GoToUrl(newNoteUrl);
        }

        public string Title => browser.FindElement(By.CssSelector(".entry-title")).Text;
        public string Content => browser.FindElement(By.CssSelector(".entry-content")).Text;

        internal override bool IsAt()
        {
            return browser.FindElement(By.Id("submit")) != null;
        }

        internal void CommentPublish(string author, string email, string comment)

        {

            var commentElement = browser.FindElement(By.Id("comment"));
            commentElement.SendKeys(comment);

            var authorElement = browser.FindElement(By.Id("author"));
            authorElement.SendKeys(author);

            var emailElement = browser.FindElement(By.Id("email"));
            emailElement.SendKeys(email);

            emailElement.Submit();

        }

        internal IEnumerable<string> GetCommentList()

        {
        return browser.FindElements(By.CssSelector(".comment-content")).Select(x => x.Text);

        }

        internal IEnumerable<string> GetAuthorList()

        {
            return browser.FindElements(By.CssSelector(".fn")).Select(x => x.Text);

        }

        internal void AddCommentToComment()
        {

           
            var commentToComment = browser.FindElements(By.CssSelector("a.comment-reply-link")).First();
            commentToComment.SendKeys(Keys.Enter);

        }
        internal IEnumerable<string> GetCommentToCommentAuthor()
        {
            var author = "Ruthie";
            var x = browser.FindElements(By.CssSelector("ol.children>li")).Single(e => e.FindElement(By.ClassName("fn")).Text == author);

            return new[] { "", "" };
        }

        internal bool Has(string exampleAuthor, string exampleContent)
        {
            var comment = browser.FindElements(By.CssSelector("ol.children>li")).Single(e => e.FindElement(By.ClassName("fn")).Text == exampleAuthor && e.FindElement(By.ClassName("comment-content")).Text == exampleContent);
            return comment != null;
        }
    }
}