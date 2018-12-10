using OpenQA.Selenium;
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
    public class Class1 : IDisposable
    {
        private IWebDriver browser;

        public Class1()
        {
            browser = new ChromeDriver();
        }

        public void Dispose()
        {
            browser.Quit();
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

        [Fact]
        public void ExampleTest()
        {
            browser.Navigate().GoToUrl("https://automatyzacja.benedykt.net/wp-admin");
            WaitForClickable(By.Id("user_login"),5);
            var userLogin = browser.FindElement(By.Id("user_login"));

 
            userLogin.SendKeys("automatyzacja");
            WaitForClickable(By.Id("user_pass"), 5);
            var userPass = browser.FindElement(By.Id("user_pass"));
            userPass.SendKeys("jesien2018");
            WaitForClickable(By.Id("rememberme"), 5);
            var rememberme = browser.FindElement(By.Id("rememberme"));
            rememberme.Click();
            WaitForClickable(By.Id("wp-submit"), 5);
            var login = browser.FindElement(By.Id ( "wp-submit"));
            login.Click();

            WaitForClickable(By.CssSelector(".wp-menu-name"),5);
            var menuElements = browser.FindElements(By.CssSelector(".wp-menu-name"));
            var posts = menuElements.Single(x => x.Text == "Wpisy");
            posts.Click();

            WaitForClickable(By.CssSelector(".wp-menu-name"), 5);
            var submenuItems = browser.FindElements(By.CssSelector(".wp-submenu>li"));
            var newPost = submenuItems.Single(x => x.Text == "Dodaj nowy");
            newPost.Click();

            WaitForClickable(By.Id("title"), 5);
            var noteTitle = browser.FindElement(By.Id("title-prompt-text"));
            noteTitle.Click();
            var title = browser.FindElement(By.Id("title"));
            var tytul = Faker.Lorem.Sentence();
            title.SendKeys(tytul);

            var html2Text = browser.FindElement(By.Id("content-html"));
            html2Text.Click();

            WaitForClickable(By.Id("publish"), 10);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 10);

            var tresc = Faker.Lorem.Paragraph();
            var content = browser.FindElement(By.Id("content"));

            content.SendKeys(tresc);

            WaitForClickable(By.CssSelector("input#publish"), 10);
            var publish = browser.FindElement(By.CssSelector("input#publish"));
            publish.Click();

            //WaitForClickable(By.Id("publish"), 10);
            //WaitForClickable(By.CssSelector(".edit-slug.button"), 10);

            var posturl = browser.FindElement(By.CssSelector("#sample-permalink>a"));
            var url = posturl.GetAttribute("href");
            MoveToElement(By.Id("wp-admin-bar-my-account"));
            WaitForClickable(By.Id("wp-admin-bar-logout"),10);
            var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
            logout.Click();
            Assert.NotNull(browser.FindElement(By.Id("user_login")));
            Assert.NotNull(browser.FindElement(By.Id("user_pass")));

            browser.Navigate().GoToUrl(url);
            
            Assert.Equal(tytul,browser.FindElement(By.CssSelector(".entry-header")).Text);
            Assert.Equal(tresc, browser.FindElement(By.CssSelector(".entry-content")).Text);
            //var successmessage = browser.FindElements(By.ClassName(""));
        }
    }
}
