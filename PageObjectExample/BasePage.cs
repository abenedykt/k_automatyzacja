using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace PageObjectExample
{
    internal abstract class BasePage
    {
        protected readonly IWebDriver browser;

        public BasePage(IWebDriver browser)
        {
            this.browser = browser;
        }

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
        internal abstract bool IsAt(); //pamięć podręczna
        protected void ScrollToElement(By selector)
        {
            IWebElement element = browser.FindElement(selector);
            Actions actions = new Actions(browser);
            actions.MoveToElement(element);
            actions.Perform();
        }
    }
}