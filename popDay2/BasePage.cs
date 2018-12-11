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

	}
}