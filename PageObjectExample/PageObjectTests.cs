using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace PageObjectExample
{
public class PageObjectTests : IDisposable
    {
        private IWebDriver browser;
        public PageObjectTests()
        {
            browser = new ChromeDriver();
        }

        [Fact]
        public void PopTest()
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

        [Fact]
        public void PopNewComment()
        {
            Uri url = new Uri("http://automatyzacja.benedykt.net/uncategorized/modi-aliquid-sunt-numquam/", UriKind.Absolute);

            var fakerComment = Faker.Lorem.Sentence();
            var fakerUserName = Faker.Name.First();
            var fakerEmail = Faker.Internet.Email();
 
            var blogPage = new NotePage(browser, url);

            Assert.True(blogPage.IsAt());
            blogPage.AddNewComment(fakerUserName, fakerEmail, fakerComment);

            Assert.True(blogPage.IsElementExistOnPage(blogPage.commentElementId, fakerComment));
            Assert.True(blogPage.IsElementExistOnPage(blogPage.userNameElementId, fakerUserName));
        }

        public void Dispose()
        {
            browser.Quit();
        }
    }
}
