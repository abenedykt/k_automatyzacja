using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Automatyzacja
{
    public class Class1 : IDisposable
    {
        private IWebDriver browser;

        public Class1()
        {
            browser = new ChromeDriver();
        }

        public void Dispose()
        {
            browser.Quit();
        }

        [Fact]
        public void ExampleTest()
        {
            
        }
    }
}
