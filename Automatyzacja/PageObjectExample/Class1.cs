using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace Automatyzacja
{
    public class Class1 : IDisposable
    {
        private IWebDriver browser;
        public Class1() => browser = new ChromeDriver();
        public void Dispose() => browser.Quit();
        [Fact]
        public void Can_publish_new_note_that_is_available_to_external_user()
        {
            var loginPage = new LoginPage(browser);

            Assert.True(loginPage.IsAt());

            var kokpit = loginPage.Login(user: "automatyzacja", pass: "jesien2018");
            var newNotePage = kokpit.NavigateToNewNote();

            Assert.True(newNotePage.IsAt());

            var exampleContent = Faker.Lorem.Paragraph();
            var exampleTitle = Faker.Lorem.Sentence();
            var newNoteUrl = newNotePage.Publish(title: exampleTitle, content: exampleContent);
            newNotePage.Logout();
            var notePage = new NotePage(browser: browser, NoteUrl: newNoteUrl);

            Assert.Equal(expected: exampleTitle, actual: notePage.Title);
            Assert.Equal(expected: exampleContent, actual: notePage.Content);          
        }
    }
}