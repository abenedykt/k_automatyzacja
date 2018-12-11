using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Automatyzacja
{
    internal class NotePage : BasePage
    {
        public string Title => browser.FindElement(By.CssSelector(".entry-title")).Text;
        public string Content => browser.FindElement(By.CssSelector(".entry-content")).Text;
        private Uri NoteUrl;
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

        internal Uri AddComment(string comment, string name, string email)
        {
            var komentarz = browser.FindElement(By.Id("comment"));
            WaitForClickable(komentarz, 5);
            komentarz.SendKeys(comment);
            browser.FindElement(By.Id("author")).SendKeys(name);
            browser.FindElement(By.Id("email")).SendKeys(email);
            MoveToElement(By.CssSelector("nav"));
            browser.FindElement(By.Id("submit")).Click();
            return new Uri(browser.Url);
        }

        internal string GetCommentName(IWebDriver driver, Uri url) => 
            FindComment(driver, url).FindElement(By.CssSelector(".fn")).Text;

        internal string GetCommentContent(IWebDriver driver, Uri url) => 
            FindComment(driver, url).FindElement(By.CssSelector(".comment-content > p")).Text;

        private IWebElement FindComment(IWebDriver driver, Uri noteadress)
        {
            String[] comment_num = noteadress.ToString().Split('#');
            String searched = "div-" + comment_num[1];
            return driver.FindElement(By.Id(searched));
        }
    }
}