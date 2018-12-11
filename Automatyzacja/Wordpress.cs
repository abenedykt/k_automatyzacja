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
    public class Wordpress : IDisposable
    {
        private IWebDriver browser;

        public Wordpress()
        {
            browser = new ChromeDriver();
        }
               
        [Fact]
        public void Can_create_new_note_that_is_avaliable_to_external_users()
        {
            browser.Navigate().GoToUrl("https://automatyzacja.benedykt.net/wp-admin");

            WaitForClickable(By.Id("user_login"), 5);
            var userLogin = browser.FindElement(By.Id("user_login"));
            userLogin.SendKeys("automatyzacja");

            WaitForClickable(By.Id("user_pass"), 5);
            var password = browser.FindElement(By.Id("user_pass"));
            password.SendKeys("jesien2018");

            WaitForClickable(By.Id("wp-submit"), 5);
            var login = browser.FindElement(By.Id("wp-submit"));
            login.Click();


            var menuElements = browser.FindElements(By.ClassName("wp-menu-name"));

            var posts = menuElements.Single(x => x.Text == "Wpisy");
            posts.Click();

            var submenuItems = browser.FindElements(By.CssSelector(".wp-submenu > li"));
            var newPost = submenuItems.Single(x => x.Text == "Dodaj nowy");
            newPost.Click();


            var noteTitle = browser.FindElement(By.Id("title-prompt-text"));
            noteTitle.Click();
            var title =  browser.FindElement(By.Id("title"));
            var exampleTitle = Faker.Lorem.Sentence();
            title.SendKeys(exampleTitle);

            browser.FindElement(By.Id("content-html")).Click();

            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);


            var content = browser.FindElement(By.Id("content"));
            var exampleContent = Faker.Lorem.Paragraph();
            content.SendKeys(exampleContent);

            var publishButton = browser.FindElement(By.Id("publish"));
            publishButton.Click();

            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);
            var postUrl = browser.FindElement(By.CssSelector("#sample-permalink > a"));
            var url = postUrl.GetAttribute("href");

            MoveToElement(By.Id("wp-admin-bar-my-account"));

            WaitForClickable(By.Id("wp-admin-bar-logout"),5);

            var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
            logout.Click();

            Assert.NotNull(browser.FindElement(By.Id("user_login")));
            Assert.NotNull(browser.FindElement(By.Id("user_pass")));

            browser.Navigate().GoToUrl(url);

            Assert.Equal(exampleTitle, browser.FindElement(By.CssSelector(".entry-title")).Text);
            Assert.Equal(exampleContent, browser.FindElement(By.CssSelector(".entry-content")).Text);

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
        public void Can_add_comment_to_existing_note()
        {
            browser.Navigate().GoToUrl("https://automatyzacja.benedykt.net");

            var addComments = browser.FindElements(By.ClassName("comments-link"));
            var firstComment = addComments.First();
            firstComment.Click();

            ScrollToElement(By.Name("submit"));

            var comment = browser.FindElement(By.Id("comment"));
            comment.Click();
            var exampleComment = Faker.Lorem.Sentence();
            comment.SendKeys(exampleComment);

            var signature = browser.FindElement(By.Id("author"));
            signature.Click();
            var mySignature = Faker.Lorem.Sentence(1);
            signature.SendKeys(mySignature);

            var email = browser.FindElement(By.Id("email"));
            email.Click();
            var myEmail = Faker.Internet.Email();
            email.SendKeys(myEmail);

            ScrollToElement(By.CssSelector("nav"));
            var submit = browser.FindElement(By.Id("submit"));
            submit.Click();

            /*
            var enteredNames = browser.FindElements(By.ClassName("fn"));
            var lastName = enteredNames.Last();
             Assert.Equal(mySignature, lastName);
             */


        }

        private void ScrollToElement(By selector)
        {
            IWebElement element = browser.FindElement(selector);
            Actions actions = new Actions(browser);
            actions.MoveToElement(element);
            actions.Perform();
        }


        public void Dispose()
        {
            browser.Quit();
        }
    }
}
