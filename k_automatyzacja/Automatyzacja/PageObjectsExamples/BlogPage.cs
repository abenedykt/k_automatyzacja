using OpenQA.Selenium;
using PageObjecttsExamples;
using System;
 


namespace PageObjectsExamples
{
    internal class BlogPage : BasePage
    {
        public BlogPage(IWebDriver browser) : base(browser)
        {

            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/");
        }

        internal override bool IsAt()
        {
            return true;
            //throw new NotImplementedException();
        }

        public void NavigateToComment()
        {
            MoveToElement(By.ClassName("comments-link"));
            WaitForClickable(By.ClassName("comments-link"), 5);
            
        }
        public void ClickOnAddCommentButton()
        {
            
            MoveToElement(By.ClassName("comments-link"));
            WaitForClickable(By.ClassName("comments-link"), 5);
            var navigateToCommentButton = browser.FindElement(By.ClassName("comments-link"));
            navigateToCommentButton.Click();
        }

        internal void CreateNewComment()
        {
            var commentContentField = browser.FindElement(By.Id("comment"));
            commentContentField.Click();
            var expectedContent = Faker.Lorem.Paragraph();
            commentContentField.SendKeys(expectedContent);

            var author = browser.FindElement(By.Id("author"));
            author.SendKeys("Test Test");

            var emailAddress = browser.FindElement(By.Id("email"));
            emailAddress.SendKeys("testing.address@gshtest.pl");

            var subminButton = browser.FindElement(By.Id("submit"));
            subminButton.Submit();
        }
       

        internal void AddReplyToComment()
        {
            var addReplyToComment =browser.FindElement(By.ClassName("comment-reply-link"));
            addReplyToComment.Click();

            var commentContentField = browser.FindElement(By.Id("comment"));
            commentContentField.Click();
            var expectedContent = Faker.Lorem.Paragraph();
            commentContentField.SendKeys(expectedContent);

            var author = browser.FindElement(By.Id("author"));
            author.SendKeys("Test Test");

            var emailAddress = browser.FindElement(By.Id("email"));
            emailAddress.SendKeys("testing.address@gshtest.pl");
            var subminButton = browser.FindElement(By.Id("submit"));
            subminButton.Submit();
        }
        
    }

}
        