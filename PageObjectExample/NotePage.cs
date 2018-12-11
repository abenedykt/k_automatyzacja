using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace PageObjectExample
{
    internal class NotePage : BasePage
    {
        private Uri newNoteUrl;

        public NotePage(IWebDriver browser, Uri newNoteUrl) : base(browser)
        {
            this.newNoteUrl = newNoteUrl;
            browser.Navigate().GoToUrl(newNoteUrl);
        }

        public string Title => browser.FindElement(By.ClassName("entry-title")).Text;
        public string Content => browser.FindElement(By.ClassName("entry-content")).Text;

        internal override bool IsAt()
        {
            throw new NotImplementedException();
        }

        public void AddNewComment(string fakerUserName, string fakerEmail, string fakerComment)
        {
            ScrollToElement(By.Id("comment"));
            var comment = browser.FindElement(By.Id("comment"));
            comment.SendKeys(fakerComment);

            ScrollToElement(By.Id("author"));
            var userName = browser.FindElement(By.Id("author"));
            userName.SendKeys(fakerUserName);

            ScrollToElement(By.Id("email"));
            var email = browser.FindElement(By.Id("email"));
            email.SendKeys(fakerEmail);

            ScrollToElement(By.ClassName("nav-previous"));
            var submit = browser.FindElement(By.Id("submit"));
            submit.Click();
        }

        public bool IsCommentExist(string fakerComment)
        {
            var comments = browser.FindElements(By.ClassName("comment-content"));
            var foundComment = comments.Single(a => a.Text == fakerComment).Text;
            return foundComment==fakerComment;
        }

        public bool IsUsernameExist(string fakerUserName)
        {
            var users = browser.FindElements(By.ClassName("fn"));
            var foundUser = users.Single(a => a.Text == fakerUserName).Text;
            return foundUser==fakerUserName;
        }
    }
}