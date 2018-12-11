using OpenQA.Selenium;

namespace PageObjectEx
{
    internal class NewCommentPage : BasePage

    {
        public NewCommentPage(IWebDriver browser) : base(browser)
        {
        }

        private void CommentPublish (string author, string email, string comment)

       {

            var commentElement = browser.FindElement(By.Id("comment"));
        commentElement.SendKeys(comment);

            var authorelement = browser.FindElement(By.Id("author"));
        authorelement.SendKeys(author);

            var emailElement = browser.FindElement(By.Id("email"));
        emailElement.SendKeys(email);

            emailElement.Submit();
        }


        internal override bool IsAt()
        {
            throw new System.NotImplementedException();
        }
    }
}