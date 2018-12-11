using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace popDay2
{
	internal class LoginPage
	{
		private IWebDriver browser;

		public LoginPage(IWebDriver browser)
		{
			this.browser = browser;
			browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-admin");
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

		internal KokpitPage Login(string userName, string password)
		{
			WaitForClickable(By.Id("user_login"), 5);
			var userLogin = browser.FindElement(By.Id("user_login"));
			userLogin.SendKeys(userName);
			WaitForClickable(By.Id("user_pass"), 5);
			var pass = browser.FindElement(By.Id("user_pass"));
			pass.SendKeys(password);
			WaitForClickable(By.Id("wp-submit"), 5);
			var login = browser.FindElement(By.Id("wp-submit"));
			login.Click();
			return new KokpitPage(browser);
 		}

		internal bool IsAt()
		{
			return browser.FindElement(By.Id("user_login")) != null &&
				browser.FindElement(By.Id("user_pass")) != null;
		}

	}
}