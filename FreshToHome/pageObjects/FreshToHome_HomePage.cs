using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshToHome.pageObjects
{

    internal class FreshToHome_HomePage
    {
        IWebDriver? driver;

        public FreshToHome_HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName, Using = "input-text")]
        private IWebElement? SearchBar { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@class='button']")]
        private IWebElement? SearchButton { get; set; }

        [FindsBy(How = How.ClassName, Using = "profile-icon")]
        private IWebElement? LoginButton { get; set; }

        [FindsBy(How = How.ClassName, Using = "menu-cart-icon")]
        private IWebElement? CartButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='logo']/a/img")]
        private IWebElement? Logo { get; set; }




        public void ClickOnSearchBar()
        {
            SearchBar?.Click();
        }

        public void ClickOnSearchButton()
        {
            SearchButton?.Click();
        }
        public void ClickOnLoginButton()
        {
            LoginButton?.Click();
        }

        public void ClickOnCartButton()
        {
            CartButton?.Click();
        }

        public void ClickOnLogo()
        {
            Logo?.Click();
        }

        public ProductDisplayPage SearchInput(string pname)
        {
            SearchBar?.SendKeys(pname);
            SearchBar?.SendKeys(Keys.Enter);
            return new ProductDisplayPage(driver);

        }

        
        public bool CheckNavbarElemetsStatus(string url)
        {
            try
            {
                var request = (System.Net.HttpWebRequest)
                    System.Net.WebRequest.Create(url);
                request.Method = "HEAD";
                using (var response = request.GetResponse())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
        
