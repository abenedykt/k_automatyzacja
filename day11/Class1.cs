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

namespace day11
{
    public class Class1 : IDisposable
    {
        private IWebDriver browser;
        public Class1()
        {
            browser = new ChromeDriver();
        }
        [Fact]
        public void ExapleTest()
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

            var submenuItems = browser.FindElements(By.CssSelector(".wp-submenu > li"));
            var newPost = submenuItems.Single(x => x.Text == "Dodaj nowy");
            newPost.Click();

            var noteTitle = browser.FindElement(By.Id("title-prompt-text"));
            noteTitle.Click();
            var title = browser.FindElement(By.Id("title"));
            var exampleTitle = Faker.Lorem.Sentence();
            title.SendKeys(exampleTitle);

            browser.FindElement(By.Id("content-html")).Click();
            //czekamy aż buttony będą aktywne
            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);
            var content = browser.FindElement(By.Id("content"));
            var exampleContent = Faker.Lorem.Paragraph();
            content.SendKeys(exampleContent);

            var publishBotton = browser.FindElement(By.Id("publish"));
            publishBotton.Click();

            WaitForClickable(By.Id("publish"), 5);
            WaitForClickable(By.CssSelector(".edit-slug.button"), 5);

            var postURL = browser.FindElement(By.CssSelector("#sample-permalink > a")); //sample-permalink ma wewnątrz "a"
            var url = postURL.GetAttribute("href");

            MoveToElement(By.Id("wp-admin-bar-my-account"));

            WaitForClickable(By.Id("wp-admin-bar-logout"), 5);

            var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
            logout.Click();

            Assert.NotNull(browser.FindElement(By.Id("user_login")));
            Assert.NotNull(browser.FindElement(By.Id("user_pass")));

            browser.Navigate().GoToUrl(url);
            Assert.Equal(exampleTitle, browser.FindElement(By.CssSelector(".entry-title")).Text);
            Assert.Equal(exampleContent, browser.FindElement(By.CssSelector(".entry-content")).Text);
        }

        [Fact]
        public void ExapleTest2()
        {
            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net");
            WaitForClickable(By.ClassName("site-header-main"), 5);
            var comments = browser.FindElement(By.ClassName("comments-link"));
            comments.Click();
            Assert.NotNull(browser.FindElement(By.ClassName("comment-reply-title")));

            var comment = browser.FindElement(By.Id("comment"));
            comment.Click();
            var exampleComment = Faker.Lorem.Sentence();
            comment.SendKeys(exampleComment);

            ScrollToElement(By.Id("author"));
            //WaitForClickable(By.Id("author"), 5);
            var newAuthor = browser.FindElement(By.Id("author"));
            //newAuthor.Click();
            var exampleAuthor = Faker.Name.First();
            newAuthor.SendKeys(exampleAuthor);

            ScrollToElement(By.Id("email"));
            //WaitForClickable(By.Id("email"), 5);
            var email = browser.FindElement(By.Id("email"));
            //email.Click();
            var exampleEmail = Faker.Internet.Email();
            email.SendKeys(exampleEmail);

            ScrollToElement(By.ClassName("meta-nav"));
            //WaitForClickable(By.Id("email"), 5);
            var submit = browser.FindElement(By.Id("submit"));
            submit.Click();

            var allComments = browser.FindElements(By.ClassName("comment-content"));

            var singleComment = allComments.Single(y => y.Text == exampleComment);
            Assert.NotNull(singleComment);


        }

        private void ScrollToElement(By selector)
        {
            IWebElement element = browser.FindElement(selector);
            Actions actions = new Actions(browser);
            actions.MoveToElement(element);
            actions.Perform();
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
