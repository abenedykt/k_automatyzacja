using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace PageObjectEx
{
    internal class NotePage : BasePage
    {
        
        private Uri newNoteUrl;

        public NotePage(IWebDriver browser, Uri newNoteUrl) : base(browser)
        {
           
            this.newNoteUrl = newNoteUrl;
            browser.Navigate().GoToUrl(newNoteUrl);
        }

        public string Title => browser.FindElement(By.CssSelector(".entry-title")).Text;
        public string Content => browser.FindElement(By.CssSelector(".entry-content")).Text;

        internal override bool IsAt()
        {
            throw new NotImplementedException();
        }
    }
}