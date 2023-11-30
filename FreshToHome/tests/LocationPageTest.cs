using FreshToHome.pageObjects;
using FreshToHome.utilities;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace FreshToHome.tests
{
    [TestFixture,Category("End-To-End Testing")]
    internal class LocationPageTest: CoreCodes
    {
        LocationPage? locationPage;

        [Test,Order(2)]
        public void UAELocationPageTest()
        {
            string? curDir = Directory.GetParent(@"../../../").FullName;
            string? fileName = curDir + "/logs/log_" +
                DateTime.Now.ToString("dd/'Uae-location'-ddMMyyyy-hhmmss") + ".txt";

                Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();

             locationPage = new LocationPage(driver);
          
            locationPage.ClickOnLocationButton();
            Log.Information("CLicked On Location Button");

            locationPage.ClickOnSelectCountryListButton();
            Log.Information("CLicked On Country-List Button");

            locationPage.ClickOnSelectCountryButton();
            Log.Information("CLicked On Country Button");
            Log.Information("Page refreshed");

            locationPage.ClickOnLocationButton();
            Log.Information("CLicked On Location Button");

            locationPage.ClickOnSelectCityButton();
            Log.Information("CLicked On City Button");
            try
            {
                Screenshots();
                Assert.That(driver.Url.Contains("ae"));
                LogTestResult("UAELocationPageTest ", "UAE Location Page Test success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("UAELocationPageTest ", "UAE Location Page Test  failure", $"UAE Location Page Test  failed \n Exception: {ex.Message}");

            }

            Log.CloseAndFlush();

        }

        FreshToHome_HomePage? homePage;

        [Test,Order(1), Category("End-To-End Testing")]
        public void IndiaLocationPageTest()
        {
            string? curDir = Directory.GetParent(@"../../../").FullName;
            string? fileName = curDir + "/logs/log_" +
                DateTime.Now.ToString("dd/'location-page-India'-ddMMyyyy-hhmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();

             locationPage = new LocationPage(driver);
            
            homePage = locationPage.InputPincode();
            Log.Information("Entered Pincode");

            DefaultWait<IWebDriver> fwait = new DefaultWait<IWebDriver>(driver);
            fwait.Timeout = TimeSpan.FromSeconds(10);
            fwait.PollingInterval = TimeSpan.FromMicroseconds(100);
            fwait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fwait.Message = "Element Not found";

            IWebElement logo = fwait.Until(d => d.FindElement(By.ClassName("logo")));
            try
            {
                Screenshots();
                Assert.That(driver.FindElement(By.Id("currentCity")).Text == "560079");
                LogTestResult("IndiaLocationPageTest ", "India Location Page Test success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("IndiaLocationPageTest", "India Location Page Test failure", $"India Location Page Test failed \n Exception: {ex.Message}");

            }


            Log.CloseAndFlush();

        }
    }
}
