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
    }
}