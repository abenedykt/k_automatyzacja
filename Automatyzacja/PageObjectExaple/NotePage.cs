using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace PageObjectExample
{
    internal class NotePage
    {
        private IWebDriver browser;
        private Uri newNoteUrl;

        public NotePage(IWebDriver browser, Uri newNoteUrl)
        {
            this.browser = browser;
            this.newNoteUrl = newNoteUrl;
        }

        public string Title { get; internal set; }
        public string Content { get; internal set; }
    }
}