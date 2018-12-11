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
            var navigateToCommentButton = browser.FindElement(By.ClassName("comments-link"));
            navigateToCommentButton.Click();
        }

    }

}
        