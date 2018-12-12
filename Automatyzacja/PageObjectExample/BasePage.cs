﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Automatyzacja
{
    internal abstract class BasePage
    {
        protected IWebDriver browser;
        public BasePage(IWebDriver browser) => this.browser = browser;
        internal abstract bool IsAt();
        protected void WaitForClickable(By by, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }
        protected void WaitForClickable(IWebElement element, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }
        protected void MoveToElement(By selector)
        {
            var element = browser.FindElement(selector);
            MoveToElement(element);
        }
        protected void MoveToElement(IWebElement element)
        {
            Actions builder = new Actions(browser);
            Actions moveTo = builder.MoveToElement(element);
            moveTo.Build().Perform();
        }
        protected void ScrollToElement(By selector)
        {
            IWebElement element = browser.FindElement(selector);
            ScrollToElement(element);
        }

        protected void ScrollToElement(IWebElement element)
        {
            Actions actions = new Actions(browser);
            actions.MoveToElement(element);
            actions.Perform();
        }
    }
}