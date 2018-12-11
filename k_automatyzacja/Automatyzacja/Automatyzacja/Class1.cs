﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Automatyzacja
{
    public class Class1: IDisposable
    {
        private IWebDriver browser;

        public Class1()
        {
          browser = new ChromeDriver();
        }



        [Fact]
        public void ExampleTest()
        {
            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin");

            WaitForClickable(By.Id("user_login"), 5);
            var userLogin = browser.FindElement(By.Id("user_login"));
            userLogin.SendKeys("automatyzacja");

            WaitForClickable(By.Id("user_pass"), 5);
            var password = browser.FindElement(By.Id("user_pass"));
            password.SendKeys("jesien2018");

            WaitForClickable(By.Id("wp-submit"), 5);
            var login = browser.FindElement(By.Id("wp-submit"));
            login.Click();

            var menuElements = browser.FindElements(By.CssSelector(".wp-menu-name"));
            var posts = menuElements.Single(x => x.Text == "Wpisy");
            posts.Click();

            var submenuItems = browser.FindElements(By.CssSelector(".wp-submenu >li"));
            var newPost = submenuItems.Single(x => x.Text == "Dodaj nowy");
            newPost.Click();

            var noteTitle = browser.FindElement(By.Id("title-prompt-text"));
            noteTitle.Click();
            var title = browser.FindElement(By.Id("title"));
            var expectedTitle= Faker.Lorem.Sentence();
            title.SendKeys(expectedTitle);
            
            WaitForClickable(By.Id("publish"), 5);

            browser.FindElement(By.Id("content-html")).Click();

            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);

            var content = browser.FindElement(By.Id("content"));
            var expectedContent = Faker.Lorem.Paragraph();
            content.SendKeys(expectedContent);
            WaitForClickable(By.Id("publish"), 5);
            var publishButton = browser.FindElement(By.Id("publish"));
            publishButton.Click();

            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);
            var postUrl = browser.FindElement(By.CssSelector("#sample-permalink >a"));
            var url = postUrl.GetAttribute("href");

            var profile = browser.FindElement(By.Id("wp-admin-bar-my-account"));           

            MoveToElement(By.Id("wp-admin-bar-my-account"));
            WaitForClickable(By.Id("wp-admin-bar-logout"), 5);
            var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
            logout.Click();
            Assert.NotNull(browser.FindElement(By.Id("user_login")));
            Assert.NotNull(browser.FindElement(By.Id("user_pass")));

            browser.Navigate().GoToUrl(url);
            Assert.Equal(expectedTitle, browser.FindElement(By.CssSelector(".entry-title")).Text);
            Assert.Equal(expectedContent, browser.FindElement(By.CssSelector(".entry-content")).Text);
    
        }



        [Fact]
        public void ExampleTest2()
        {
            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/");
            var commentButton = browser.FindElement(By.ClassName("comments-link"));
            commentButton.Click();

            var commentContentField = browser.FindElement(By.Id("comment"));
            commentContentField.Click();
            var expectedContent = Faker.Lorem.Paragraph();
            commentContentField.SendKeys(expectedContent);

            var author = browser.FindElement(By.Id("author"));
            author.SendKeys("Test Test");

            var emailAddress = browser.FindElement(By.Id("email"));
            emailAddress.SendKeys("testing.address@gshtest.pl");

            var subminButton = browser.FindElement(By.Id("submit"));
            subminButton.Submit();

            var commentsArea = browser.FindElement(By.ClassName("comments-area"));
            Assert.Contains(expectedContent, browser.FindElements(By.CssSelector(".comment-content")).Select(X=> X.Text));
            Assert.Contains("Test Test", browser.FindElement(By.CssSelector(".comment-author")).Text);
            




        }

        private void WaitForClickable(By by, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }
        private void WaitForClickable(IWebElement element, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }
        public void Dispose()
        {
            browser.Quit();
        }
        private void MoveToElement(By selector)
        {
            var element = browser.FindElement(selector);
            MoveToElement(element);
        }
        private void MoveToElement(IWebElement element)
        {
            Actions builder = new Actions(browser);
            Actions moveTo = builder.MoveToElement(element);
            moveTo.Build().Perform();
        }

    }
}
