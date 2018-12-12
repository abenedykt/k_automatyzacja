using System;
using System.Linq;
using OpenQA.Selenium;
using PageObjecttsExamples;

namespace PageObjectsExamples
{
    internal class ClickOnReplyButton: BasePage
    {
        public ClickOnReplyButton(IWebDriver browser) : base(browser)
        {
            browser.FindElement(By.ClassName("comment-reply-link")).Click();
        }

        

        internal override bool IsAt()
        {
            throw new NotImplementedException();
        }
    }
}