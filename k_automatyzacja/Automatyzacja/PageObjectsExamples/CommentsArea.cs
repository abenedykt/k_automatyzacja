using OpenQA.Selenium;
using PageObjecttsExamples;
using System;

namespace PageObjectsExamples
{
    internal class CommentsArea: BasePage
    {
       
        public CommentsArea(IWebDriver browser) : base(browser)
        {
            browser.FindElement(By.ClassName("comments-area"));
        }

        internal override bool IsAt()
        {
            throw new NotImplementedException();
        }
    }
}