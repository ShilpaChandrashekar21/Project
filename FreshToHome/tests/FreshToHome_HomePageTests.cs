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
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;

namespace FreshToHome.tests
{
    [TestFixture]
    internal class FreshToHome_HomePageTests : CoreCodes
    {
        FreshToHome_HomePage? homePage;
        [Test,Category("Smoke Test")]

        public void HeaderElementsTest()
        {
            
            string? curDir = Directory.GetParent(@"../../../").FullName;
            string? fileName = curDir + "/logs/log_" +
                DateTime.Now.ToString("dd/'header'-ddMMyyyy-hhmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            driver.Navigate().Refresh();

            homePage = new FreshToHome_HomePage(driver);

            homePage.ClickOnLogo();
            Screenshots();
            Log.Information("Clicked On Logo");

            homePage.ClickOnSearchBar();
            Screenshots();
            Log.Information("Clicked On search-bar");

            homePage.ClickOnSearchButton();
            
            Log.Information("Clicked On search-button");
            try
            {
                Screenshots();
                Assert.That(driver.Url.Contains("search"));
                LogTestResult("HeaderElementsTest ", "Header Elements Test for search success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("HeaderElementsTest  ", "Header Elements Test for search failure", $"Header Elements Test for search failed \n Exception: {ex.Message}");
            }
            
            homePage.ClickOnLoginButton();
            
            Log.Information("Clicked On Login button");
           
            try
            {
                Screenshots();
                Assert.That(driver.FindElement(By.XPath("(//div[@class='youama-window-title'])[2]/h3")).Text.Contains("Login"));
                LogTestResult("HeaderElementsTest ", "Header Elements Test for login success");
             }
            catch (AssertionException ex)
            {
                LogTestResult("HeaderElementsTest  ", "Header Elements Test for login failure", $"Header Elements Test for login failed \n Exception: {ex.Message}");
            }
            
            
            homePage.ClickOnCartButton();
            
            Log.Information("Clicked On Cart button");
            try
            {
                Screenshots();
                Assert.That(driver.FindElement(By.XPath("//div[@class='block-content']/p")).Text.Equals("You have no items in your shopping cart."));
                LogTestResult("HeaderElementsTest ", "Header Elements Test for cart success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("HeaderElementsTest  ", "Header Elements Test for cart failure", $"Header Elements Test for cart failed \n Exception: {ex.Message}");
            }
           

            Log.CloseAndFlush();



        }

        [Test]
        public void ClickOnNavbarElementsTest()
        {
            driver.Navigate().GoToUrl("https://www.freshtohome.com");
            string? curDir = Directory.GetParent(@"../../../").FullName;
            string? fileName = curDir + "/logs/log_" +
                DateTime.Now.ToString("dd/'navbar'-ddMMyyyy-hhmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            driver.Navigate().Refresh();

            homePage = new FreshToHome_HomePage(driver);

           
                List<IWebElement> navElements = driver.
                    FindElements(By.XPath("//ul[@id='nav']/li/a")).ToList();

                foreach (var link in navElements)
                {
                    string url = link.GetAttribute("href");
                    if (url == null)
                    {
                        Console.WriteLine("Url is null");
                        continue;
                    }
                    else
                    {
                        bool isWorking = homePage.CheckNavbarElemetsStatus(url);
                        if (isWorking)
                        {
                       
                            Log.Information(url + " is working");
                       
                        }
                        else
                        {
                            Log.Information(url + " is not working");
                        }

                    }


                }
            Log.CloseAndFlush();
        }

            
           
           
    }


}
