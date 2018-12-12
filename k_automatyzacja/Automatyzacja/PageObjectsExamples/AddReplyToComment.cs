using OpenQA.Selenium;
using PageObjecttsExamples;

namespace PageObjectsExamples
{
    internal class AddReplyToComment: BasePage 

    {
    public AddReplyToComment(IWebDriver browser) : base(browser)
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
            throw new System.NotImplementedException();
        }
    }
}