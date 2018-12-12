using OpenQA.Selenium;

namespace PageObjectTest
{
    internal class MainPage:BasePage
    {
        public MainPage(IWebDriver browser):base(browser)
        {
            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/");
        }

        internal override bool IsAt()
        {
            return browser.Title.Contains("Blog");
        }
    }
}