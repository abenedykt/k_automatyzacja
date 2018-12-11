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
        public NotePage(IWebDriver browser, Uri NoteUrl) : base(browser)
        {
            this.NoteUrl = NoteUrl;
            browser.Navigate().GoToUrl(NoteUrl);
        }





    }
}