using System;
using System.Linq;
using OpenQA.Selenium;

namespace PageObjectExample
{
    internal class KokpitPage : BasePage
    {
        //private IWebDriver browser;

        public KokpitPage(IWebDriver browser) :base(browser)
        {
            //this.browser = browser;
        }

        internal override bool IsAt()
        {
            throw new NotImplementedException();
        }

        internal newNotePage NavigateToNewNote()
        {
            var menuElements = browser.FindElements(By.CssSelector(".wp-menu-name"));

            var posts = menuElements.Single(x => x.Text == "Wpisy");
            posts.Click();

            var submenuItems = browser.FindElements(By.CssSelector(".wp-submenu > li"));
            var newPost = submenuItems.Single(x => x.Text == "Dodaj nowy");
            newPost.Click();
            return new newNotePage(browser);
        }
    }
}