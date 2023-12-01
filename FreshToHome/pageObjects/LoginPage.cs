using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshToHome.pageObjects
{
    internal class LoginPage
    {
        IWebDriver driver;
        public LoginPage(IWebDriver driver) 
        { 
        
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName, Using = "profile-icon")]
        private IWebElement? LoginButton { get; set; }
        [CacheLookup]

        [FindsBy(How = How.Id, Using = "youama-phone")]
        private IWebElement? PhoneNumberInput { get; set; }
        [CacheLookup]

        [FindsBy(How = How.Id, Using = "sendOtpBtn")]
        private IWebElement? SendOtpButton { get; set; }
        [CacheLookup]

        [FindsBy(How = How.XPath, Using = "//p[@id='y-to-login']")]
        private IWebElement? LogingUsingPasswordButton { get; set; }

        [FindsBy(How = How.Id, Using = "y-to-register")]
        private IWebElement? RegisterButton { get; set; }

        public void ClickOnLoginButton()
        {
            LoginButton?.Click();
        }

        public void PhoneNumberInputCheck(string pnum)
        {
            PhoneNumberInput?.SendKeys(pnum);
        }

        public void PhoneNumberInputClear()
        {
            PhoneNumberInput?.Clear();
        }

        public void ClickOnSendOtpButton()
        {
            SendOtpButton?.Click();
        }

        public void ClickOnLogingUsingPasswordButton()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", LogingUsingPasswordButton);

            //LogingUsingPasswordButton?.Click();
        }

        public void ClickOnRegisterButton()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", LogingUsingPasswordButton);
            //RegisterButton?.Click();
        }
    }
}
