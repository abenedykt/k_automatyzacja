using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace popDay2
{
	internal class NotePage
	{
		private IWebDriver browser;
		private Uri linkText;

		public NotePage(IWebDriver browser, Uri linkText)
		{
			this.browser = browser;
			this.linkText = linkText;
			browser.Navigate().GoToUrl(linkText);
		}
		public string Title => browser.FindElement(By.ClassName("entry-title")).Text;
		public string Content => browser.FindElement(By.ClassName("entry-content")).Text;

	}
	}