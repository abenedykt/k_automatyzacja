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
    public class Class1
    {
        [Fact]
        public void ExampleTest()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Quit();
        }
    }
}
