using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PageObjecttsExamples
{
    public class Class1 : IDisposable
    {
        private IWebDriver browser;

        public Class1()
        {
            browser = new ChromeDriver();
        }

        [Fact]
        public void Can_Publish_new_note()
        {
            var exampleTitle = Faker.Lorem.Sentence();
            var exampleContent = Faker.Lorem.Paragraph();

            var loginPage = new LoginPage(browser);
            Assert.True(loginPage.IsAt());
            var kokpit = loginPage.Login("automatyzacja", "jesien2018");

            var newNotePage = kokpit.NavigateToNewNote();
            Assert.True(newNotePage.IsAt());
            var newNoteUrl = newNotePage.Publish(exampleTitle, exampleContent);

            newNotePage.Logout();

            var notePage = new NotePage(browser, newNoteUrl);


        }

        public void Dispose()
        {
            browser.Quit();
        }
    }

    internal class LoginPage : BasePage
    {
        public LoginPage(IWebDriver browser) : base(browser)
        {

            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin");
        }

        internal KokpitPage Login(string userName, string password)
        {
            WaitForClickable(By.Id("user_login"), 5);
            var userLogin = browser.FindElement(By.Id("user_login"));
            userLogin.SendKeys(userName);

            WaitForClickable(By.Id("user_pass"), 5);
            var passwordElement = browser.FindElement(By.Id("user_pass"));
            passwordElement.SendKeys(password);

            WaitForClickable(By.Id("wp-submit"), 5);
            var login = browser.FindElement(By.Id("wp-submit"));
            login.Click();

            return new KokpitPage(browser);
        }

        internal override bool IsAt()
        {
            if (browser.FindElement(By.Id("user_login")) != null &&
                browser.FindElement(By.Id("user_pass")) != null)
            {
                return true;
            }

            else

            {
                return false;
            }

        }

       
    }
}