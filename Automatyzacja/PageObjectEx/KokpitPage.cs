﻿using System;
using System.Linq;
using OpenQA.Selenium;

namespace PageObjectEx
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
            var menuElements = browser.FindElements(By.CssSelector(".wp-menu-name"));
            var posts = menuElements.Single(x => x.Text == "Wpisy");
            posts.Click();

            var subMenuItems = browser.FindElements(By.CssSelector(".wp-submenu > li"));
            var newPost = subMenuItems.Single(x => x.Text == "Dodaj nowy");
            newPost.Click();

            return new NewNotePage(browser);
        }
    }
}