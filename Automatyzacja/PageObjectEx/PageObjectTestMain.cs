using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PageObjectEx
{
    public class Wordpress_pop_tests:IDisposable
    {
        
        private IWebDriver browser;

        public Wordpress_pop_tests()
        {
            browser = new ChromeDriver();
        }

        [Fact]
        public void TC01()
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
        public void TC02()
        {
            var exampleAuthor = Faker.Name.First();
            var exampleContent = Faker.Lorem.Paragraph();
            var exampleEmail = Faker.Internet.Email();
 
            var commentPage = new NotePage(browser, NotePage.NoteUrlVar);
            Assert.True(commentPage.IsAt());
            commentPage.CommentPublish(exampleAuthor, exampleEmail, exampleContent);
            
          
            Assert.Contains(exampleContent, commentPage.GetCommentList());
            Assert.Contains(exampleAuthor, commentPage.GetAuthorList());
            
           
        }
        


            public void Dispose()
        {
            browser.Quit();
        }
    }
}
