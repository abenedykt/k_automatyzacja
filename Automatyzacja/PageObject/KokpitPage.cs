using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PageObjectTest
{
    internal class KokpitPage : BasePage
    {
        public KokpitPage(IWebDriver browser) : base(browser) { }
        internal NewNotePage NavigateToNewNote()
        {
            WaitForClickable(By.CssSelector(".wp-menu-name"), 5);
            var menuElements = browser.FindElements(By.CssSelector(".wp-menu-name"));
            var posts = menuElements.Single(x => x.Text == "Wpisy");
            posts.Click();
            WaitForClickable(By.CssSelector(".wp-menu-name"), 5);
            var submenuItems = browser.FindElements(By.CssSelector(".wp-submenu>li"));
            var newPost = submenuItems.Single(x => x.Text == "Dodaj nowy");
            newPost.Click();
            return new NewNotePage(browser);
        }
    }
}