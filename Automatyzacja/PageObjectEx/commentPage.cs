using OpenQA.Selenium;

namespace PageObjectEx
{
    internal class CommentPage : BasePage

    {
        public CommentPage(IWebDriver browser) : base(browser)
        {
            browser.Navigate().GoToUrl("https://automatyzacja.benedykt.net/uncategorized/et-dolor-itaque-neque-ea/");
        }

        internal override bool IsAt()
        {
            return browser.FindElement(By.Id("submit")) != null;
        }

    }


}