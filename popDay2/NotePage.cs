using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace popDay2
{
	internal class NotePage
	{
		private IWebDriver browser;
		private Uri linkText;

		public NotePage(IWebDriver browser, Uri newNoteUrl)
		{
			this.browser = browser;
			this.linkText = newNoteUrl;
		}
		public string Title => browser.FindElement(By.ClassName("entry-title")).Text;
		public string Content => browser.FindElement(By.ClassName("entry-content")).Text;

	}
	}