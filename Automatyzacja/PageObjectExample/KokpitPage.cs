using System;
using System.Linq;
using OpenQA.Selenium;

namespace Automatyzacja
{
    internal class KokpitPage
    {
        private IWebDriver browser;

        public KokpitPage(IWebDriver browser)
        {
            this.browser = browser;
        }

        internal NewNotePage NavigateToNewNote()
        {
            browser.FindElements(By.CssSelector(".wp-menu-name")).Single(x => x.Text == "Wpisy").Click();
            browser.FindElements(By.CssSelector(".wp-submenu > li")).Single(x => x.Text == "Dodaj nowy").Click();
            return new NewNotePage(browser);
        }
    }
}