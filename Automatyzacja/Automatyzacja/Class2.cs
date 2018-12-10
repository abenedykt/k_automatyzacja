using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using Xunit;

namespace Automatyzacja
{
    public class Class2 : IDisposable
    {
        private IWebDriver browser;

        public Class2() => browser = new ChromeDriver();
        public void Dispose() => browser.Quit();

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

        private void ScrollToElement(By selector)
        {
            IWebElement element = browser.FindElement(selector);
            Actions actions = new Actions(browser);
            actions.MoveToElement(element);
            actions.Perform();
        }

        [Fact]
        public void NoteTest()
        {

            browser.Navigate().GoToUrl("http://automatyzacja.benedykt.net/");
            browser.FindElements(By.CssSelector(".entry-title")).First().Click();
            var komentarz = browser.FindElement(By.Id("comment"));
            WaitForClickable(komentarz, 5);

            String comment = Faker.Lorem.Sentence();
            komentarz.SendKeys(comment);
            String name = Faker.Internet.Email();
            browser.FindElement(By.Id("author")).SendKeys(name);
            browser.FindElement(By.Id("email")).SendKeys(name);
            browser.FindElement(By.Id("submit")).Click();
            String url = browser.Url;

            //alternatywne przejście do komentarza ze strony głównej
            //String title_rememberd = browser.FindElement(By.CssSelector(".entry-title")).Text;
            //browser.FindElement(By.CssSelector(".site-title")).Click();
            //var title = browser.FindElements(By.CssSelector(".entry-title")).Single(x => x.Text == title);
            //ScrollToElement(title);
            //title.Click();
            //następnie linia 75

            browser.Navigate().GoToUrl(url);
            String[] comment_num = url.Split('#');
            String searched = "div-" + comment_num[1];
            var komant = browser.FindElement(By.Id(searched));
            Assert.Equal(name, komant.FindElement(By.CssSelector(".fn")).Text);
            Assert.Equal(comment, komant.FindElement(By.CssSelector(".comment-content > p")).Text);
        }
    }
}
