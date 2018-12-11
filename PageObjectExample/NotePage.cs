﻿using System;
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

            browser.Navigate().GoToUrl(newNoteUrl);
        }

        public string Title => browser.FindElement(By.ClassName("entry-title")).Text;
        public string Content => browser.FindElement(By.ClassName("entry-content")).Text;
    }
}