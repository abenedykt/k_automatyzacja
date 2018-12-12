using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace PageObjectExample
{
    internal class NotePage : BasePage
    {
        private Uri newNoteUrl;
        public string commentElementId = "comment-content";
        public string userNameElementId = "fn";

        public NotePage(IWebDriver browser, Uri newNoteUrl) : base(browser)
        {
            this.newNoteUrl = newNoteUrl;
            browser.Navigate().GoToUrl(newNoteUrl);
        }

        public string Title => browser.FindElement(By.ClassName("entry-title")).Text;
        public string Content => browser.FindElement(By.ClassName("entry-content")).Text;

        private void ScrollAndInsert(string elementId, string input)
        {
            ScrollToElement(By.Id(elementId));
            var comment = browser.FindElement(By.Id(elementId));
            comment.SendKeys(input);
        }

        public void AddNewComment(komentarz comment)
        {
            ScrollAndInsert("comment", comment.Comment);
            ScrollAndInsert("author", comment.UserName);
            ScrollAndInsert("email", comment.Email);

            ScrollToElement(By.ClassName("nav-previous"));
            var submit = browser.FindElement(By.Id("submit"));
            submit.Click();
        }

        public bool IsElementExistOnPage(string elementName, string searchedElement)
        {
            var comments = browser.FindElements(By.ClassName(elementName));
            var foundElement = comments.Single(a => a.Text == searchedElement).Text;
            return foundElement == searchedElement;
        }

        internal override bool IsAt()
        {
            return browser.FindElements(By.CssSelector("body.single-post")).Any();
        }

        internal void AddCommentToComment(komentarz comment, komentarz newComment)
        {
            var replys = browser.FindElements(By.ClassName("comment-body"));
            
            var reply = replys.Where(a => a.Text.Contains(comment.Comment));

            reply.First().FindElement(By.TagName("a")).Click();

            AddNewComment(newComment);
        }
    }
}