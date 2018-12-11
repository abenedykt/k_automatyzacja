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

namespace popDay2
{
	public class Class1 : IDisposable
	{
		private IWebDriver browser;

		public Class1()
		{
			browser = new ChromeDriver();
		}
		[Fact]
		public void canPublishNewPubliclyVisibleNote()
		{
			var exampleTitle = Faker.Lorem.Sentence();
			var exampleContent = Faker.Lorem.Paragraph();


			var loginPage = new LoginPage(browser);
			Assert.True(loginPage.IsAt());

			var kokpit = loginPage.Login("automatyzacja", "jesien2018");
			var newNotePage = kokpit.NavigateToNewNote();
			Assert.True(newNotePage.IsAt());
			var newNoteUrl = newNotePage.Publish(exampleTitle, exampleContent);
			WaitForClickable(By.Id("publish"), 5);

			newNotePage.Logout();

			var notePage = new NotePage(browser, newNoteUrl);
			Assert.Equal(exampleTitle, notePage.Title);
			Assert.Equal(exampleContent, notePage.Content);
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