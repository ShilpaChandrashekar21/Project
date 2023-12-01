using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshToHome.pageObjects
{
    internal class LocationPage
    {
        IWebDriver? driver;

        public LocationPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

       
         [FindsBy(How = How.Id, Using = "currentCity")]
        private IWebElement? LocationButton { get; set; }
        [CacheLookup]


        [FindsBy(How = How.XPath, Using = "//button[starts-with(@id,'flagstrap-drop-down')]")]
        private IWebElement? SelectCountryListButton { get; set; }
        //ul[@class='dropdown-menu']/li[2]/a

        [FindsBy(How = How.XPath, Using = "//ul[@class='dropdown-menu']/li[2]/a")]
        private IWebElement? SelectCountry { get; set; }

        
        [FindsBy(How = How.XPath, Using = "//ul[@class='main-cities-ul']/li[1]/a")]
        private IWebElement? SelectCity { get; set; }


        [FindsBy(How = How.XPath, Using = "//input[@type='search']")]
        private IWebElement? Pincode { get; set; }



        public void ClickOnLocationButton()
        {
            LocationButton?.Click();
        }
        public void ClickOnSelectCountryListButton()
        {
            SelectCountryListButton?.Click();
        }

        public void ClickOnSelectCountryButton()
        {
            SelectCountry?.Click();
        }

        public void ClickOnSelectCityButton()
        {
            SelectCity?.Click();
        }
        public FreshToHome_HomePage InputPincode()
        {
            Pincode?.SendKeys("560079");
            Pincode?.SendKeys(Keys.Enter);
            return new FreshToHome_HomePage(driver);
        }
    }
}
