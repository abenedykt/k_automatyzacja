﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Faker;

namespace warsztatSelenium
{
    public class Class1 :IDisposable
    {
		private IWebDriver browser;

			public Class1()
		{
			browser = new ChromeDriver();
		}

		[Fact]
		public void ExampleTest()

		{
			browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/wp-login.php");
			WaitForClickable(By.Id("user_login"), 5);
			var userLogin = browser.FindElement(By.Id("user_login"));
			userLogin.SendKeys("automatyzacja");
			WaitForClickable(By.Id("user_pass"), 5);
			var pass = browser.FindElement(By.Id("user_pass"));
			pass.SendKeys("jesien2018");
			WaitForClickable(By.Id("wp-submit"), 5);
			var login = browser.FindElement(By.Id("wp-submit"));
			login.Click();
			var menuElements = browser.FindElements(By.CssSelector(".wp-menu-name"));
			var posts = menuElements.Single(x => x.Text == "Wpisy");
			posts.Click();
			var menuPostsElements = browser.FindElements(By.CssSelector(".wp-submenu > li"));
			var newPost = menuPostsElements.Single(a => a.Text == "Dodaj nowy");
			newPost.Click();
			var noteTitle = browser.FindElement(By.Id("title"));
			noteTitle.SendKeys(Faker.Lorem.Sentence());
			browser.FindElement(By.Id("content-html"));
			var noteField = browser.FindElement(By.ClassName("wp-editor-area"));
			noteField.Click();
			noteField.SendKeys(Faker.Lorem.Paragraph());
			var submit = browser.FindElement(By.Id("publish"));
			submit.Click();
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

		public void Dispose()
		{
			browser.Quit();
		}

	}
}