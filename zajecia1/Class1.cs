using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace zajecia1
{
    public class Class1 : IDisposable
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

            WaitForClickable(By.ClassName("wp-menu-name"), 5);

            var menuElements = browser.FindElements(By.ClassName("wp-menu-name"));
            var posts = menuElements.Single(a => a.Text == "Wpisy");
            posts.Click();

            var newPost = browser.FindElement(By.ClassName("page-title-action"));
            newPost.Click();

            var newTitle = browser.FindElement(By.Id("title-prompt-text"));
            newTitle.Click();
            var inputTitle = browser.FindElement(By.Id("title"));
            var entryTitle = Faker.Lorem.Sentence();
            inputTitle.SendKeys(entryTitle);

            WaitForClickable(By.Id("publish"), 5);

            browser.FindElement(By.Id("content-html")).Click();
            // sprawdzamy czy sa buttony - bo inaczej nie dziala
            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.Id("edit-slug-buttons"), 5);
            //
            var content = browser.FindElement(By.Id("content"));
            content.Click();

            var entryContent = Faker.Lorem.Sentence();
            content.SendKeys(entryContent);

            WaitForClickable(By.Id("publish"), 5);

            var publishButton = browser.FindElement(By.Id("publish"));
            publishButton.Click();
            

            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.Id("edit-slug-buttons"), 5);

            var editButton = browser.FindElement(By.Id("edit-slug-buttons"));

            //editButton.Click();
            var postUrl = browser.FindElement(By.CssSelector("#sample-permalink > a"));
            var url = postUrl.GetAttribute("href");

            MoveToElement(By.Id("wp-admin-bar-top-secondary"));
            WaitForClickable(By.Id("wp-admin-bar-logout"), 5);
            var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
            logout.Click();
            WaitForClickable(By.Id("nav"), 5);

            Assert.NotNull(browser.FindElement(By.Id("user_login")));
            Assert.NotNull(browser.FindElement(By.Id("user_pass")));

            browser.Navigate().GoToUrl(url);
            Assert.NotNull(browser.FindElement(By.ClassName("entry-content")));
            Assert.NotNull(browser.FindElement(By.ClassName("entry-header")));

            Assert.Equal(entryTitle, browser.FindElement(By.ClassName("entry-title")).Text);
            Assert.Equal(entryContent, browser.FindElement(By.ClassName("entry-content")).Text);


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

        public void Dispose()
        {
            browser.Quit();
        }


    }
}
