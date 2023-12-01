using FreshToHome.pageObjects;
using FreshToHome.utilities;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshToHome.tests
{
    [TestFixture]
    internal class CartPageTest : CoreCodes
    {
        FreshToHome_HomePage? homePage;
        ProductDisplayPage? productPage;
        CartPage? cartPage;
        [Test,Category("End-to-End Testing")]
       
        public void CartOperationsTest()
        {
            string? curDir = Directory.GetParent(@"../../../").FullName;
            string? fileName = curDir + "/logs/log_" +
                DateTime.Now.ToString("dd/'cart'-ddMMyyyy-hhmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            homePage = new FreshToHome_HomePage(driver);

             productPage = homePage.SearchInput("fish");
            Log.Information("Searched-Text");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            productPage.SelectProductLink("3");
            Log.Information("Selected Product");

             cartPage = productPage.AddProductLink();
            Log.Information("Clicked on the  Add product button");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); 
            
            cartPage.ClickOnCartButton();
            Log.Information("Clicked on the  Cart button");

            cartPage.ClickOnMyCartButton();
            Log.Information("Clicked on the  My-Cart button");
            
            cartPage.ClickOnIncQuantityButton();
            Log.Information("Clicked on the  Increement button");
           
            cartPage.ClickOnDecrQuantityButton();
            Log.Information("Clicked on the  Decreement button");

            cartPage.ClickOnContinueShoppingButton();
            Log.Information("Clicked on the Continue Shopping button");

            homePage.SearchInput("chicken");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3); 

            productPage.SelectProductLink("5");
            Log.Information("Selected Product");

            cartPage.ClickOnCartButton();
            Log.Information("Clicked on the  Cart button");

            cartPage.ClickOnMyCartButton();
            Log.Information("Clicked on the  My-Cart button");

            cartPage.ClickOnClearCartButton();
            Log.Information("Clicked on the  clear-Cart button");

            try
            {
                Screenshots();
                Assert.That(driver.FindElement(By.XPath("//div[@class='page-title']/h1")).Text.Contains("Empty"));

                LogTestResult("CartPageTest ", "Cart Page Test success");

            }
            catch (AssertionException ex)
            {
                LogTestResult("CartPageTest  ", "Cart Page Test failure", $"Cart Page Test failed \n Exception: {ex.Message}");

            }
        
             Log.CloseAndFlush();

        }
    }
}
