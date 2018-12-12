using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
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
        public void Can_publish_new_note_that_is_avaliable_to_external_users()
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
        public void Can_add_a_comment_to_existing_note()
        {
            //zaloguj się
            // dodaj notatkę
            // opublikuj ją (i pobierz urla)
            // wyloguj
            // otworz notatke
            // dodaj komentarz
            // sprawdz ze koentarz sie opublikował        

            var noteTitle = Faker.Lorem.Sentence();
            var noteContent = Faker.Lorem.Paragraph();
            var exampleComment = CreateExampleComment();

            var loginPage = new LoginPage(browser);
            Assert.True(loginPage.IsAt());
            var kokpit = loginPage.Login("automatyzacja", "jesien2018");

            var newNotePage = kokpit.NavigateToNewNote();
            Assert.True(newNotePage.IsAt());
            var newNote = newNotePage.Publish(noteTitle, noteContent);

            newNotePage.Logout();

            var notePage = new NotePage(browser, newNote);
            Assert.True(notePage.IsAt());
            notePage.AddComment(exampleComment);
            Assert.True(notePage.HasComment(exampleComment));

        }

        private Comment CreateExampleComment()
        {
            return new Comment
            {
                Email = Faker.Internet.Email(),
                Text = Faker.Lorem.Paragraph(),
                Author = Faker.Name.First()
            };
        }

        public void Dispose()
        {
            browser.Quit();
        }
    }
}
