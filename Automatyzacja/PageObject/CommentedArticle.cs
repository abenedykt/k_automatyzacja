using System;
using System.Linq;
using OpenQA.Selenium;

namespace PageObjectTest
{
    internal class CommentedArticle: BasePage
    {
        public CommentedArticle(IWebDriver browser):base(browser)
        {
        }

        internal override bool IsAt()
        {
            throw new System.NotImplementedException();
        }

        internal bool HasComment(string komentarz, string podpis)
        {
            return browser.FindElements(By.CssSelector(".comment-content>p")).SingleOrDefault(x => x.Text == komentarz) != null &&
            browser.FindElements(By.CssSelector(".fn")).Single(x => x.Text == podpis)!=null;
        }

        internal void AddResponse(string comment, string name, string mail, string komId)
        {
            ScrollToElement(By.CssSelector("#reply-title"));
            browser.FindElements(By.CssSelector(".comment-reply-link")).Single(x => x.GetAttribute("href").Contains (komId)).Click();
            WaitForClickable(By.Id("comment"), 5);
            ScrollToElement(By.Id("email"));
            browser.FindElement(By.Id("comment")).SendKeys(comment);
            browser.FindElement(By.Id("author")).SendKeys(name);
            browser.FindElement(By.Id("email")).SendKeys(mail);
            ScrollToElement(By.Id("submit"));
            browser.FindElement(By.Id("submit")).Submit();
        }

        internal bool HasCommenedComment(string komentarz, string podpis,string komId)
        {
            var e = browser.FindElements(By.CssSelector(".comment.depth-2")).SingleOrDefault(x => x.GetAttribute("id").ToString().Contains(komId));
            return e != null;
        }
    }
}