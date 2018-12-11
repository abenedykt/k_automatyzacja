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
            var submit = browser.FindElement(By.Id("submit"));
            MoveToElement(By.CssSelector("nav"));
            submit.Click();
            Uri uri = new Uri(browser.Url);
            return uri;
        }

        internal string GetCommentName(IWebDriver driver, Uri url)
        {
            var temp = FindComment(driver, url);
            var temp1 = temp.FindElement(By.CssSelector(".fn"));
            return temp1.Text;            
        }

        internal string GetCommentContent(IWebDriver driver, Uri url)
        {
            var temp = FindComment(driver, url);
            var temp1 = temp.FindElement(By.CssSelector(".comment-content > p"));
            return temp1.Text;
        }

        private IWebElement FindComment(IWebDriver driver, Uri noteadress)
        {
            string url = noteadress.ToString();
            String[] comment_num = url.Split('#');
            String searched = "div-" + comment_num[1];
            var obj = driver.FindElement(By.Id(searched));
            return obj;
        }
    }
}