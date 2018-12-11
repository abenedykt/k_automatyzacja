using System;
using System.Linq;
using System.Security.Policy;
using OpenQA.Selenium;

namespace PageObjectExample
{
    internal class BlogPage : BasePage
    {
        Uri url = new Uri("http://automatyzacja.benedykt.net/uncategorized/modi-aliquid-sunt-numquam/", UriKind.Absolute);

        public string commentElementId = "comment-content";
        public string userNameElementId = "fn";

        public BlogPage(IWebDriver browser) : base(browser)
        {
            browser.Navigate().GoToUrl(url);
        }

        private void ScrollAndInsert(string elementId, string input)
        {
            ScrollToElement(By.Id(elementId));
            var comment = browser.FindElement(By.Id(elementId));
            comment.SendKeys(input);
        }

        public void AddNewComment(string fakerUserName, string fakerEmail, string fakerComment)
        {
            ScrollAndInsert("comment", fakerComment);
            ScrollAndInsert("author", fakerUserName);
            ScrollAndInsert("email", fakerEmail);

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
            return browser.FindElement(By.ClassName("site-title")) != null &&
                   browser.FindElement(By.ClassName("site-description")) != null;
        }
    }
}