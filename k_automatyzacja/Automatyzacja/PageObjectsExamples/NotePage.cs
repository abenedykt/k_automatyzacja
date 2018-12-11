using OpenQA.Selenium;
using System;

namespace PageObjecttsExamples
{
    internal class NotePage : BasePage
    {

        public NotePage(IWebDriver browser, Uri newNoteUrl) : base(browser)
        {
            browser.Navigate().GoToUrl(newNoteUrl);
        }

        public string Title { get; internal set; }
        public string Content { get; internal set; }

        internal override bool IsAt()
        {
            throw new NotImplementedException();
        }
    }
}