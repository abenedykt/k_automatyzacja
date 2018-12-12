using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using PageObjecttsExamples;
using System;

namespace PageObjectsExamples
{
    internal class CreateNewComment : BasePage
    {


        public CreateNewComment(IWebDriver browser) : base(browser)
        {
            var commentContentField = browser.FindElement(By.Id("comment"));
            commentContentField.Click();
            var expectedContent = Faker.Lorem.Paragraph();
            commentContentField.SendKeys(expectedContent);

            var author = browser.FindElement(By.Id("author"));
            author.SendKeys("Test Test");

            var emailAddress = browser.FindElement(By.Id("email"));
            emailAddress.SendKeys("testing.address@gshtest.pl");
        }

        public void Submit()
        {
            var subminButton = browser.FindElement(By.Id("submit"));
            subminButton.Submit();
        }

        internal override bool IsAt()
        {
            throw new NotImplementedException();
        }
    }
}