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
    public class Class1:IDisposable
    {
        private IWebDriver browser;
        public Class1()
        {
            browser = new ChromeDriver();
   
        }


        [Fact]
        public void TC01_AddNote()
        {    
            browser.Navigate().GoToUrl("https://automatyzacja.benedykt.net/wp-admin");

            WaitForClickable(By.Id("user_login"),5);
            var userLogin = browser.FindElement(By.Id("user_login"));
            userLogin.SendKeys("automatyzacja");

            WaitForClickable(By.Id("user_pass"), 5);
            var userPassword = browser.FindElement(By.Id("user_pass"));
            userPassword.SendKeys("jesien2018");

            WaitForClickable(By.Id("wp-submit"), 5);
            var login = browser.FindElement(By.Id("wp-submit"));
            login.Click();

            var menuElements = browser.FindElements(By.CssSelector(".wp-menu-name"));
            var posts = menuElements.Single(x => x.Text == "Wpisy");
            posts.Click();

            var subMenuItems = browser.FindElements(By.CssSelector(".wp-submenu > li"));
            var newPost = subMenuItems.Single(x => x.Text == "Dodaj nowy");
            newPost.Click();

            var noteTitle = browser.FindElement(By.Id("title-prompt-text"));
            noteTitle.Click();
            var title = browser.FindElement(By.Id("title"));
            title = browser.FindElement(By.Id("title"));
            string titleContent = Faker.Lorem.Sentence();
            title.SendKeys(titleContent);

            var textButton = browser.FindElement(By.Id("content-html"));
            textButton.Click();
            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector("#edit-slug-buttons > button"), 5);
            var content = browser.FindElement(By.Id("content"));
            string expectedContent = Faker.Lorem.Sentence();
            content.SendKeys(expectedContent);

            var publish = browser.FindElement(By.Id("publish"));
            WaitForClickable(By.Id("publish"), 5);
            publish.Click();
            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector("#edit-slug-buttons > button"), 5);
            var link = browser.FindElement(By.CssSelector("#sample-permalink > a"));
            string url = link.GetAttribute("href");

            MoveToElement(By.Id("wp-admin-bar-my-account"));
            WaitForClickable(By.Id("wp-admin-bar-logout"), 5);

            var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
            logout.Click();

            Assert.NotNull(browser.FindElement(By.Id("user_login")));
            Assert.NotNull(browser.FindElement(By.Id("user_pass")));

            browser.Navigate().GoToUrl(url);

            Assert.Equal(titleContent, browser.FindElement(By.CssSelector(".entry-title")).Text);
            Assert.Equal(expectedContent,browser.FindElement(By.CssSelector(".entry-content")).Text);

        }

        [Fact]
        public void TC02_AddComment()
        {
            browser.Navigate().GoToUrl("https://automatyzacja.benedykt.net/uncategorized/autem-vel-rerum-saepe/");

            var publishComment = browser.FindElement(By.Id("submit"));
            WaitForClickable(By.Id("submit"), 5);     
            
            string commentContent = Faker.Lorem.Sentence();
            string emailContent = Faker.Internet.Email();
            string nameContent = Faker.Name.First();

            var comment = browser.FindElement(By.Id("comment"));
            comment.SendKeys(commentContent);
            var author = browser.FindElement(By.Id("author"));
            author.SendKeys(nameContent);
            var email = browser.FindElement(By.Id("email"));
            email.SendKeys(emailContent);
            email.Submit();
            var commentsList = browser.FindElements(By.CssSelector(".comment-content")).Select(x=>x.Text);
            Assert.Contains(commentContent, commentsList);




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
