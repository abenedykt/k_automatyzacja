using System;
using System.Linq;
using OpenQA.Selenium;

namespace PageObjectExample
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
            var menuElements = browser.FindElements(By.ClassName("wp-menu-name"));
            var posts = menuElements.Single(a => a.Text == "Wpisy");
            posts.Click();

            var newPost = browser.FindElement(By.ClassName("page-title-action"));
            newPost.Click();
            return  new NewNotePage(browser);
        }
    }
}