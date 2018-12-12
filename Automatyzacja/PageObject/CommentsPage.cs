using System;
using OpenQA.Selenium;

namespace PageObjectTest
{
    internal class CommentsPage:BasePage
    {
        public CommentsPage(IWebDriver browser) :base(browser)
        {
        }

        internal override bool IsAt()
        {
            return browser.FindElement(By.CssSelector("h2.entry-title")).Displayed;
        }

        internal void Publish(string comment, string name, string mail,string website)
        {
            //WaitForClickable(By.CssSelector("h2.entry-title"), 5);
            var commentbutton = browser.FindElement(By.ClassName("comments-link"));
            commentbutton.Click();
            WaitForClickable(By.Id("comment"),5);
            var title = browser.FindElement(By.ClassName("entry-title"));
            var content = browser.FindElement(By.ClassName("entry-content>p"));
            
            ScrollToElement(By.Id("email"));
            var commentContent = browser.FindElement(By.Id("comment"));
            commentContent.SendKeys(comment);
            var podpisElement = browser.FindElement(By.Id("author"));
            podpisElement.SendKeys(name);
            var mailelement = browser.FindElement(By.Id("email"));
            mailelement.SendKeys(mail);
            var witrynalement = browser.FindElement(By.Id("url"));
            witrynalement.SendKeys(website.ToString());
            ScrollToElement(By.Id("submit"));
            var submit = browser.FindElement(By.Id("submit"));
            submit.Submit();
        }
    }
}