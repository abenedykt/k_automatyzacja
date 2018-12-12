using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automatyzacja
{
    internal class NotePage : BasePage
    {
        public string Title => browser.FindElement(By.CssSelector(".entry-title")).Text;

        public string Content => browser.FindElement(By.CssSelector(".entry-content")).Text;

        public IWebElement Response { get => response; set => response = value; }

        private Uri NoteUrl;

        [FindsBy(How = How.CssSelector, Using = ".comment-body")]
        private IList<IWebElement> Responses;

        private IWebElement response;

        internal override bool IsAt()
        {
            throw new NotImplementedException();
        }
        public NotePage(IWebDriver browser, Uri NoteUrl) : base(browser)
        {
            this.NoteUrl = NoteUrl;
            browser.Navigate().GoToUrl(NoteUrl);
        }

        public NotePage(IWebDriver browser) : base(browser)
        {
        }

        public string GetCommentID()
        {
            return response.GetAttribute("id");
        }

        internal string GetResponseID(Uri uri)
        {
            return FindComment(uri).GetAttribute("id");
        }

        internal string GetResponsetoCommentID(string comment, string name)
        {
            var temp = comment.Remove(0, 4);
            return browser.FindElement(By.Id(temp)).FindElements(By.CssSelector(".comment-body"))
                .Single(x => x.FindElement(By.CssSelector(".fn")).Text == name).GetAttribute("id");
        }

        internal void CreateResponse()
        {
            PageFactory.InitElements(browser, this);
            ScrollToElement(By.Id("reply-title"));
            response = Responses.Last();
            response.FindElement(By.CssSelector(".comment-reply-link")).Click();
        }

        internal Uri AddComment(string comment, string name, string email)
        {
            var komentarz = browser.FindElement(By.Id("comment"));
            WaitForClickable(komentarz, 5);
            komentarz.SendKeys(comment);
            MoveToElement(By.Id("email"));
            browser.FindElement(By.Id("author")).SendKeys(name);
            MoveToElement(By.Id("submit"));
            browser.FindElement(By.Id("email")).SendKeys(email);
            MoveToElement(By.CssSelector("nav"));
            browser.FindElement(By.Id("submit")).Click();
            return new Uri(browser.Url);
        }

        internal string GetCommentName(Uri url) => 
            FindComment(url).FindElement(By.CssSelector(".fn")).Text;

        internal string GetCommentContent(Uri url) => 
            FindComment(url).FindElement(By.CssSelector(".comment-content > p")).Text;

        private IWebElement FindComment(Uri url)
        {
            String[] comment_num = url.ToString().Split('#');
            return browser.FindElement(By.Id("div-" + comment_num[1]));
        }
    }
}