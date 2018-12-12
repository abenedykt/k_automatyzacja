using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PageObjectTest
{
    public class Comment:IDisposable
    {
        public Comment()
        {
            browser = new ChromeDriver();
        }
        private IWebDriver browser;
        public void Dispose()
        {
            browser.Quit();
        }
        


        [Fact]
        public void AddComment()
        {
            var mainPage = new MainPage(browser);
            Assert.True(mainPage.IsAt());
            var openComments = new CommentsPage(browser);
            Assert.True(openComments.IsAt());
            var komentarz = Faker.Lorem.Sentence();
            var podpis = Faker.Name.FullName();
            var mail = Faker.Internet.Email();
            var witryna = Faker.Lorem.Sentence();
            openComments.Publish(komentarz, podpis, mail, witryna);

            var commentedArticle = new CommentedArticle(browser);
            Assert.True(condition: commentedArticle.HasComment(komentarz, podpis));
        }
    }
}
