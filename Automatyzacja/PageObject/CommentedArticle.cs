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
    }
}