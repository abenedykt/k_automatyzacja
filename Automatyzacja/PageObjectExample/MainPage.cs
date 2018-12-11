using System;
using System.Linq;
using OpenQA.Selenium;

namespace Automatyzacja
{
    internal class MainPage :BasePage
    {

        public MainPage(IWebDriver browser) : base(browser) => 
            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/");

        internal override bool IsAt()
        {
            throw new System.NotImplementedException();
        }

        internal NotePage OpenNotePage(IWebDriver browser)
        {
            browser.FindElements(By.CssSelector(".comments-link")).First().Click();
            return new NotePage(browser);
        }
    }
}