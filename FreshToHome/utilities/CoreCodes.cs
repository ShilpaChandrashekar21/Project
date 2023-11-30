using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshToHome.utilities
{
    internal class CoreCodes
    {
        
        public IWebDriver? driver;

        public ExtentReports extent;
        ExtentSparkReporter sparkReporter;
        public ExtentTest test;


        [OneTimeSetUp]
        public void InitializeBrowser()
        {
            ReadConfigFiles.ReadConfigProperty();

            string currDir = Directory.GetParent(@"../../../").FullName;
            
            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currDir + "/extentReport/extent-report"
                + DateTime.Now.ToString("yyyy_MM_dd/HH-mms-s") + ".html");

            extent.AttachReporter(sparkReporter);

            if (ReadConfigFiles.properties["browser"].ToLower() == "chrome") 
            {
               driver = new ChromeDriver();
            }
            else if (ReadConfigFiles.properties["browser"].ToLower() == "edge")
            {
                driver = new EdgeDriver();
            }

            driver.Url = ReadConfigFiles.properties["baseUrl"];
            driver.Manage().Window.Maximize();
        }

        public void Screenshots()
        {
            ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
            Screenshot screenshot = takesScreenshot.GetScreenshot();

            string curDirec = Directory.GetParent(@"../../../").FullName;
            string filePath = curDirec + "/screenshots/ss_" + DateTime.Now.ToString("ddMMyyyy-hhmmss") + ".png";

            screenshot.SaveAsFile(filePath);
        }

        public static void ScrollIntoView(IWebDriver driver, IWebElement element)
        {
          
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true); ", element);

        }

        protected void LogTestResult(string testName, string result, string errorMessage = null)
        {
            Log.Information(result);
            test = extent.CreateTest(testName);
            if (errorMessage == null)
            {
                Log.Information(testName + " Passed");
                test.Pass(result);

            }
            else
            {
                Log.Error($"Test failed for {testName} \n Exception:\n{errorMessage}");
                test.Fail(result);
            }

        }

        

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driver.Quit();
            extent.Flush();
        }
    }
}
