using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace popDay2
{
	public class Commenting : IDisposable
	{
		private IWebDriver browser;

		
		public  Commenting()
		{
			browser = new ChromeDriver();
		}
		[Fact]
		public void canAddVisibleComment()
		{
			var comment = Faker.Lorem.Sentence();
			var author = Faker.Lorem.Sentence();
			var email = Faker.Internet.Email();
			//go to site
			var notePage = new NotePage(browser,new Uri( "http://automatyzacja.benedykt.net/uncategorized/sed-molestiae-quia-optio-est-voluptas-dolorum-reprehenderit"));
			Assert.True(notePage.IsAt());
			//add comment author email
			notePage.AddComment(comment, author,email);
			//check content
			var commentList = browser.FindElement(By.Id("comments"));
			var commentListTexts = commentList.Text;
			Assert.Contains(comment, commentListTexts);
			Assert.Contains(author, commentListTexts);
		}

		public void Dispose()
		{
			browser.Quit();
		}
	}
}
