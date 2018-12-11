using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Automatyzacja
{
    internal class NotePage
    {
        private IWebDriver browser;
        private Uri NoteUrl;

        public NotePage(IWebDriver browser, Uri NoteUrl)
        {
            this.browser = browser;
            this.NoteUrl = NoteUrl;
        }

        public IEnumerable<char> Title { get; internal set; }
        public IEnumerable<char> Content { get; internal set; }
    }
}