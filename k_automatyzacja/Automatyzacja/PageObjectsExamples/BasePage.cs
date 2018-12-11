using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PageObjecttsExamples
{
    internal class BasePage
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
    }
}