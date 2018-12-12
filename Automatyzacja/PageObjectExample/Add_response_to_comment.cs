using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace Automatyzacja
{
    public class Add_response_to_comment : IDisposable
    {
        private IWebDriver browser;

        public Add_response_to_comment() => browser = new ChromeDriver();

        public void Dispose() => browser.Quit();

        [Fact]
        public void Can_add_response_to_the_comment_on_a_note()
        {
            String response = Faker.Lorem.Sentence();
            String name = Faker.Internet.Email();            

            MainPage mainPage = new MainPage(browser);
            NotePage notePage = mainPage.OpenNotePage(browser);
            notePage.CreateResponse();

            var commentID = notePage.GetCommentID();

            Uri noteadress = notePage.AddComment(response, name, name);
            notePage = new NotePage(browser, noteadress);

            Assert.Equal(expected: name, actual: notePage.GetCommentName(noteadress));
            Assert.Equal(expected: response, actual: notePage.GetCommentContent(noteadress));
            Assert.Equal(expected: notePage.GetResponseID(noteadress), 
                actual: notePage.GetResponsetoCommentID(commentID, name));
        }
    }
}
