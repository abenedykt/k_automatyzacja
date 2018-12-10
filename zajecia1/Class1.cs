using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            inputTitle.SendKeys(Faker.Lorem.Sentence());

            browser.FindElement(By.Id("content-html")).Click();

            var content = browser.FindElement(By.Id("content"));
            content.Click();

            content.SendKeys(Faker.Lorem.Sentence());


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


    }
}
