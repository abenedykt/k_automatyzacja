using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace popDay2
{
	internal class NotePage : BasePage
	{
			private Uri linkText;

		public NotePage(IWebDriver browser, Uri linkText):base(browser)
		{
	
			this.linkText = linkText;
			browser.Navigate().GoToUrl(linkText);
		}
		public string Title => browser.FindElement(By.ClassName("entry-title")).Text;
		public string Content => browser.FindElement(By.ClassName("entry-content")).Text;

		internal override bool IsAt()
		{
			return browser.FindElement(By.Id("comment")) != null;
		}


		internal void AddComment(string comment, string author, string email)
		{
			var newComment = browser.FindElement(By.Id("comment"));
			newComment.SendKeys(comment);
			
			var newAuthor = browser.FindElement(By.Id("author"));
			newAuthor.SendKeys(author);
		
			var newEmail = browser.FindElement(By.Id("email"));
			newEmail.SendKeys(email);
			var add = browser.FindElement(By.Id("submit"));
			add.Click();
		}
	}
}