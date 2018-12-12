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
            //przekazywanie cookisow
            var exampleCooke = new Cookie("pierniczek", "!@#$%^&*()", "");
            browser.Manage().Cookies.AddCookie(exampleCooke);


            Uri url = new Uri("http://automatyzacja.benedykt.net/uncategorized/modi-aliquid-sunt-numquam/", UriKind.Absolute);

            komentarz comment = CreateNewComment();

            var blogPage = new NotePage(browser, url);

            Assert.True(blogPage.IsAt());
            blogPage.AddNewComment(comment);

            Assert.True(blogPage.IsElementExistOnPage(blogPage.commentElementId, comment.Comment));
            Assert.True(blogPage.IsElementExistOnPage(blogPage.userNameElementId, comment.UserName));
        }

        [Fact]
        public void PopCommentToComment()
        {
            Uri url = new Uri("http://automatyzacja.benedykt.net/uncategorized/modi-aliquid-sunt-numquam/", UriKind.Absolute);

            komentarz comment = CreateNewComment();
            komentarz newComment = CreateNewComment();

            var blogPage = new NotePage(browser, url);

            Assert.True(blogPage.IsAt());
            blogPage.AddNewComment(comment);
            blogPage.AddAnswerToComment(comment, newComment);

            Assert.True(blogPage.IsAnswerExist(newComment));
            Assert.True(blogPage.IsElementExistOnPage(blogPage.commentElementId, newComment.Comment));
            Assert.True(blogPage.IsElementExistOnPage(blogPage.userNameElementId, newComment.UserName));
        }

        private komentarz CreateNewComment()
        {
            komentarz comment = new komentarz();
            comment.Comment = Faker.Lorem.Sentence();
            comment.UserName = Faker.Name.First();
            comment.Email = Faker.Internet.Email();
            return comment;
        }

        public void Dispose()
        {
            browser.Quit();
        }
    }
}
