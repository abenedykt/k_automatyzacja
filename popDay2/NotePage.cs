using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace popDay2
{
	internal class NotePage
	{
		private IWebDriver browser;
		private Uri newNoteUrl;

		public NotePage(IWebDriver browser, Uri newNoteUrl)
		{
			this.browser = browser;
			this.newNoteUrl = newNoteUrl;
		}

		internal string Title()
		{
			throw new NotImplementedException();
		}

		internal string Content()
		{
			throw new NotImplementedException();
		}
	}
}