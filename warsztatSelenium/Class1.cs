using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Faker;
using OpenQA.Selenium.Interactions;

namespace warsztatSelenium
{
	public class Class1 : IDisposable
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
			var updatedTitle = noteTitle.GetAttribute("value");
			browser.FindElement(By.Id("content-html"));
			var noteField = browser.FindElement(By.ClassName("wp-editor-area"));
			noteField.Click();
			noteField.SendKeys(Faker.Lorem.Paragraph());
			var content = noteField.GetAttribute("value");
			var submit = browser.FindElement(By.Id("publish"));
			submit.Click();
			WaitForClickable(By.Id("publish"), 5);
			WaitForClickable(By.Id("edit-slug-buttons"), 5);
			var editLinkButton = browser.FindElement(By.Id("edit-slug-buttons"));
			editLinkButton.Click();
			var newLinkInput = browser.FindElement(By.Id("new-post-slug"));
			var partialLinkText = newLinkInput.GetAttribute("value");
			var linkText = "http://automatyzacja.benedykt.net/uncategorized/" + partialLinkText;
			Console.WriteLine(linkText);
			var element = browser.FindElement(By.Id("wp-admin-bar-my-account"));
			MoveToElement(By.Id("wp-admin-bar-my-account"));

			WaitForClickable(By.Id("wp-admin-bar-logout"), 5);
			var logout = browser.FindElement(By.Id("wp-admin-bar-logout"));
			logout.Click();
			Assert.NotNull(browser.FindElement(By.Id("user_login")));
			Assert.NotNull(browser.FindElement(By.Id("user_pass")));
			browser.Navigate().GoToUrl(linkText);
			WaitForClickable(By.ClassName("entry-title"), 5);
			var actualTitleField = browser.FindElement(By.ClassName("entry-title"));
			var actualTitle = actualTitleField.Text;
			var actualContentField = browser.FindElement(By.ClassName("entry-content"));
			var actualContent = actualContentField.Text;
			Console.WriteLine(actualTitle);
			Assert.Equal(updatedTitle, actualTitle);
			Assert.Equal(content, actualContent);
		}

		[Fact]
		public void commenting()
		{
			browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/uncategorized/sed-molestiae-quia-optio-est-voluptas-dolorum-reprehenderit");
			var textArea = browser.FindElement(By.Id("comment"));
			textArea.Click();
			textArea.SendKeys(Faker.Lorem.Sentence());
			var commentField = browser.FindElement(By.Id("comment"));
			var comment = commentField.Text;
			var authorInput = browser.FindElement(By.Id("author"));
			//authorInput.Click();
			authorInput.SendKeys(Faker.Lorem.Sentence());
			var authorText = browser.FindElement(By.Id("author"));
			var authorFakerText = authorText.Text;
			var emailInput = browser.FindElement(By.Id("email"));
			emailInput.Click();
			emailInput.SendKeys(Faker.Internet.Email());
			var submitButton = browser.FindElement(By.Id("submit"));
			submitButton.Click();
			var commentList = browser.FindElement(By.Id("comments"));
			var commentListTexts = commentList.Text;
			Assert.Contains(comment, commentListTexts);
			Assert.Contains(authorFakerText, commentListTexts);
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
