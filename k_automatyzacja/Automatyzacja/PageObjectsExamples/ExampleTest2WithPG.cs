﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjecttsExamples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PageObjectsExamples
{
    public class ExampleTest2WithPG : IDisposable
    {
        private IWebDriver browser;

        public ExampleTest2WithPG()
        {
            browser = new ChromeDriver();
        }

        [Fact]
        public void Can_Add_new_Comment()
        {

            var blogPage = new BlogPage(browser);
            Assert.True(blogPage.IsAt());
            blogPage.NavigateToComment();
            blogPage.ClickOnAddCommentButton();
            var createNewComment = new CreateNewComment(browser);
            createNewComment.Submit();

            var commentsArea = browser.FindElement(By.ClassName("comments-area"));

            //Assert.Contains("Test Test", browser.FindElement(By.CssSelector(".comment-author")).Text);
            //Assert.True(createNewComment.HasComment(expectedComment, expectedTitle));
            //Assert.Contains("sdfghj", createNewComment.AllComments());
        }


        [Fact]

        public void Add_Comment_To_Newly_Created_Comment()
        {
        var exampleTitle = Faker.Lorem.Sentence();
        var exampleContent = Faker.Lorem.Paragraph();

        var loginPage = new LoginPage(browser);
        Assert.True(loginPage.IsAt());
        var kokpit = loginPage.Login(LoginSettings.UserLogin, LoginSettings.UserPassword);   
            
        var newNotePage = kokpit.NavigateToNewNote();
        Assert.True(newNotePage.IsAt());

        var newNoteUrl = newNotePage.Publish(exampleTitle, exampleContent);
        newNotePage.Logout();

        var blogPage = new BlogPage(browser);
        Assert.True(blogPage.IsAt());         
        blogPage.ClickOnAddCommentButton();
        blogPage.CreateNewComment();
        blogPage.AddReplyToComment();
         
              
         //Assert.
        
         

         
        }

        public void Dispose()
        {
            browser.Quit();
        }
    }

   
}


