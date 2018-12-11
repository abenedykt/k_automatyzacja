using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using Xunit;

namespace Automatyzacja
{
    public class Class1 : IDisposable
    {
        private IWebDriver browser;
        public Class1() => browser = new ChromeDriver();
        public void Dispose() => browser.Quit();

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
        public void Can_publish_new_note_that_is_available_to_external_user()
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

            Assert.Equal(exampleTitle, notePage.Title);
            Assert.Equal(exampleContent, notePage.Content);
           
        }
    }
}