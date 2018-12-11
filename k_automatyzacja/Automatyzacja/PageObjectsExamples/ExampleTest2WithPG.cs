using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PageObjectsExamples
{
    public class ExampleTest2WithPG: IDisposable
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

           // Assert.True(createNewComment.HasComment("sss", "aas"));
            //Assert.Contains("sdfghj", createNewComment.AllComments());

            var commentsArea = browser.FindElement(By.ClassName("comments-area"));

           Assert.Contains("Test Test", browser.FindElement(By.CssSelector(".comment-author")).Text);
            








        }

        public void Dispose()
        {
            browser.Quit();
        }
    }

   
}


