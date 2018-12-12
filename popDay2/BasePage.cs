using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace popDay2
{
	internal abstract class BasePage
	{
		protected readonly IWebDriver browser;
		public BasePage(IWebDriver browser)
		{
			this.browser = browser;
		}

		internal void WaitForClickable(By by, int seconds)
		{
			var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
		}
		internal void WaitForClickable(IWebElement element, int seconds)
		{
			var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
		}
		internal void MoveToElement(By selector)
		{
			var element = browser.FindElement(selector);
			MoveToElement(element);
		}
		internal void MoveToElement(IWebElement element)
		{
			Actions builder = new Actions(browser);
			Actions moveTo = builder.MoveToElement(element);
			moveTo.Build().Perform();
		}
		private void ScrollAndInsert(string elementId, string input)
		{
			MoveToElement(By.Id(elementId));
			var comment = browser.FindElement(By.Id(elementId));
			comment.SendKeys(input);
		}

		public void AddNewComment(string fakerUserName, string fakerEmail, string fakerComment)
		{
			ScrollAndInsert("comment", fakerComment);
			ScrollAndInsert("author", fakerUserName);
			ScrollAndInsert("email", fakerEmail);

			MoveToElement(By.ClassName("nav-previous"));
			var submit = browser.FindElement(By.Id("submit"));
			submit.Click();
		}

		public bool IsElementExistOnPage(string elementName, string searchedElement)
		{
			var comments = browser.FindElements(By.ClassName(elementName));
			var foundElement = comments.Single(a => a.Text == searchedElement).Text;
			return foundElement == searchedElement;
		}

		internal override bool IsAt()
		{
			return browser.FindElement(By.ClassName("site-title")) != null &&
				   browser.FindElement(By.ClassName("site-description")) != null;

		internal abstract bool IsAt();
	}
}