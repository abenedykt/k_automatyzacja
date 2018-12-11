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
    public class Wordpress_pop_tests : IDisposable
    {
        private IWebDriver browser;

        public Wordpress_pop_tests()
        {
                browser = new ChromeDriver();
        }

        [Fact]
        public void Can_publish_new_note_that_is_avaliable_to_external_user()
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
        public void Can_publish_new_comment_that_is_avaliable_to_external_user()
        {
            var exampleComment = Faker.Lorem.Sentence();
            var exampleAuthor = Faker.Name.First();
            var exampleEmail = Faker.Internet.Email();

            var blogPage = new BlogPage(browser);
            Assert.True(blogPage.IsAt());

            blogPage.AddNewComment(exampleComment, exampleAuthor, exampleEmail);

            Assert.Contains(exampleComment, blogPage.Comments());

        }

        public void Dispose()
        {
           browser.Quit();
        }
    }
}
