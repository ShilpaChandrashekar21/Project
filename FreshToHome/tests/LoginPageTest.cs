using FreshToHome.pageObjects;
using FreshToHome.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshToHome.tests
{
    [TestFixture]
    internal class LoginPageTest : CoreCodes
    {
        LoginPage loginPage;
        [Test,Category("Smoke Test"),Order(1)]
        [TestCase("6363371923")]
        public void ValidPhoneNumberLoginTest(string input)
        {
            driver.Navigate().GoToUrl("https://www.freshtohome.com");
            string? curDir = Directory.GetParent(@"../../../").FullName;
            string? fileName = curDir + "/logs/log_" +
                DateTime.Now.ToString("dd/'login-page'-ddMMyyyy-hhmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            driver.Navigate().Refresh();

             loginPage = new LoginPage(driver);

            loginPage.ClickOnLoginButton();
            Log.Information("Clicked on login button");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            loginPage.PhoneNumberInputCheck(input);
            Log.Information("Entered Phone number");

            loginPage.ClickOnSendOtpButton();
            Log.Information("Clicked on send otp button");

            
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                Assert.That(driver.FindElement(By.Id("loginOtpMsg")).Text.Contains("mobile"));
                LogTestResult("ValidPhoneNumberLoginTest ", "Valid PhoneNumber Login Test success");

            }
            catch (AssertionException ex)
            {
                LogTestResult("ValidPhoneNumberLoginTest ", "Valid PhoneNumber Login Test failure", $"ValidPhoneNumberLoginTest failed \n Exception: {ex.Message}");

            }
        
           
            loginPage.ClickOnLogingUsingPasswordButton();
            Log.Information("Clicked on login using password button");

            loginPage.ClickOnRegisterButton();
            Log.Information("Clicked on register button");

        Log.CloseAndFlush();
        }

        [Test, Category("Regression Test")]
        [Order(2)]
        public void InvalidPhoneNumberLoginTest()
        {
           
            string? curDir = Directory.GetParent(@"../../../").FullName;
            string? fileName = curDir + "/logs/log_" +
                DateTime.Now.ToString("dd/'login-page-invalid'-ddMMyyyy-hhmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            driver.Navigate().Refresh();


            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/testData/InputData.xlsx";
            string? sheetName = "LoginData";
            List<SearchProduct> excelDataList = ExcelUtils.ReadCreateAccountExcelData(excelFilePath, sheetName);
           
             loginPage = new LoginPage(driver);

            loginPage.ClickOnLoginButton();
            Log.Information("Clicked on login button");

            foreach (var excelData in excelDataList)
            {
                string? phonenumber = excelData?.PhoneNumber;
                if (phonenumber == null)
                {
                    Console.WriteLine("The phone number cannot be null");
                }
                else
                {
                    Log.Information($"Logging for : {phonenumber}");
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

                    loginPage.PhoneNumberInputCheck(phonenumber);
                    Log.Information("Entered Phone number");

                    loginPage.ClickOnSendOtpButton();
                    
                    try
                    {

                        IWebElement errorElement = driver.FindElement(By.XPath("//div[contains(@class, 'youama-ajaxlogin-error') and contains(text(), 'This is not a valid mobile number!')]"));

                        string actualErrorMessage = errorElement.Text;

                        Screenshots();

                        Assert.That(actualErrorMessage == "This is not a valid mobile number!");

                        LogTestResult("InvalidPhoneNumberLoginTest ", "Invalid PhoneNumber Login Test success");

                    }
                    catch (AssertionException ex)
                    {
                        LogTestResult("InvalidPhoneNumberLoginTest ", "Invalid PhoneNumber Login Test failure", $"InvalidPhoneNumberLoginTest failed \n Exception: {ex.Message}");

                    }

                    loginPage.PhoneNumberInputClear();
                }
               
                
            
            }

            Log.CloseAndFlush();


        }
    }
}
