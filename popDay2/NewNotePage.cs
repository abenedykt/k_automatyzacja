using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace popDay2
{
	internal class NewNotePage
	{
		private IWebDriver browser;

		public NewNotePage(IWebDriver browser)
		{
			this.browser = browser;
		}

		internal bool IsAt()
		{
			return browser.Title.StartsWith("Dodaj nowy wpis");
		}
		private void WaitForClickable(By by, int seconds)
		{
			var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
		}
		private void WaitForClickable(IWebElement element, int seconds)
		{
			var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
		}


		internal Uri Publish(string title, string content)
		{
			var noteTitle = browser.FindElement(By.Id("title"));
			noteTitle.SendKeys(title);
			var updatedTitle = noteTitle.GetAttribute("value");
			browser.FindElement(By.Id("content-html"));
			var noteField = browser.FindElement(By.ClassName("wp-editor-area"));
			noteField.Click();
			noteField.SendKeys(content);
			var contentValue = noteField.GetAttribute("value");
			var submit = browser.FindElement(By.Id("publish"));
			submit.Click();
			WaitForClickable(By.Id("publish"), 5);
			WaitForClickable(By.Id("edit-slug-buttons"), 5);
			var editLinkButton = browser.FindElement(By.Id("edit-slug-buttons"));
			editLinkButton.Click();
			var newLinkInput = browser.FindElement(By.Id("new-post-slug"));
			var partialLinkText = newLinkInput.GetAttribute("value");
			var linkText = "http://automatyzacja.benedykt.net/uncategorized/" + partialLinkText;

			return new Uri(linkText);
		}

		internal void Logout()
		{
			var element = browser.FindElement(By.Id("wp-admin-bar-my-account"));
			MoveToElement(By.Id("wp-admin-bar-my-account"));

			WaitForClickable(By.Id("wp-admin-bar-logout"), 5);
			var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
			logout.Click();
		}
		private void MoveToElement(By selector)
		{
			var element = browser.FindElement(selector);
			MoveToElement(element);
		}
		private void MoveToElement(IWebElement element)
		{
			Actions builder = new Actions(browser);
			Actions moveTo = builder.MoveToElement(element);
			moveTo.Build().Perform();
		}

	}
}