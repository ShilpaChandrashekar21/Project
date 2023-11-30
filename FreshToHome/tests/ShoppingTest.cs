using FreshToHome.pageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshToHome.utilities;
using Serilog;

namespace FreshToHome.tests
{
    
    [TestFixture]
    internal class ShoppingTest: CoreCodes
    {
        LocationPage? locationPage;
        FreshToHome_HomePage? homePage;
        ProductDisplayPage? productPage;
        CartPage? cartPage;

        [Test,Category("End-To-End Testing")]
        public void  BuyOnlineTest() 
        {
            string? curDir = Directory.GetParent(@"../../../").FullName;
            string? fileName = curDir + "/logs/log_" +
                DateTime.Now.ToString("dd/'end-to-end'-ddMMyyyy-hhmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            locationPage = new LocationPage(driver);
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/testData/InputData.xlsx";
            string? sheetName = "Search";

            List<SearchProduct> excelDataList = ExcelUtils.ReadCreateAccountExcelData(excelFilePath, sheetName);
          
            homePage = locationPage.InputPincode();
            
            foreach (var excelData in excelDataList)
            {

                string? searchtext = excelData?.ProductName;
                string? productposition = excelData?.ProductPosition;


                Log.Information($"Searching for : {searchtext}");

                DefaultWait<IWebDriver> fwait = new DefaultWait<IWebDriver>(driver);
                fwait.Timeout = TimeSpan.FromSeconds(10);
                fwait.PollingInterval = TimeSpan.FromMicroseconds(100);
                fwait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                fwait.Message = "Element Not found";

                IWebElement logo = fwait.Until(d => d.FindElement(By.ClassName("logo")));

                 productPage = homePage.SearchInput(searchtext);
                Log.Information($"Search-Text: {searchtext}");
                
                try
                {
                    Screenshots();
                    Assert.That(driver.Url.Contains(searchtext));

                    LogTestResult("Buy Online Test ", "Buy Online Test success");

                }
                catch (AssertionException ex)
                {
                    LogTestResult("Buy Online Test ", "Buy Online Test failure", $"Buy Online Test failed \n Exception: {ex.Message}");

                }
                //as fluentwait & webdriver wait is not waiting till the page renders , using Impicit wait
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
              
                productPage.SelectProductLink(productposition);
                Log.Information($"Selected Product: {productposition}");
                Screenshots();

                cartPage =productPage.AddProductLink();
                Log.Information("Clicked on the  Add product button");

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                
                IWebElement alertCheckout= driver.FindElement(By.XPath("//span[text() ='Checkout']"));
                alertCheckout.Click();
                Log.Information("Clicked on the  Checkout modal");
                
                driver.Navigate().GoToUrl("https://www.freshtohome.com/");
                Log.Information("Navigating to home page");
            }

            homePage.ClickOnCartButton();
            Log.Information("Clicked on the  Cart button");

            Log.CloseAndFlush();

        }
        
            
    }
}
