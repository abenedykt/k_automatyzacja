using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace Automatyzacja
{
    public class Class2 : IDisposable
    {
        private IWebDriver browser;
        public Class2() => browser = new ChromeDriver();
        public void Dispose() => browser.Quit();
        [Fact]
        public void Add_comment_and_view_it_on_a_note()
        {
            String comment = Faker.Lorem.Sentence();
            String name = Faker.Internet.Email();

            var mainPage = new MainPage(browser);
            NotePage notePage = mainPage.OpenNotePage(browser);
            Uri noteadress = notePage.AddComment(comment, name, name);
            notePage = new NotePage(browser, noteadress);

            Assert.Equal(name, actual: notePage.GetCommentName(noteadress));
            Assert.Equal(comment, actual: notePage.GetCommentContent( noteadress));
        }   
    }
}