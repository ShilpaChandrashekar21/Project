using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshToHome.pageObjects
{
    internal class ProductDisplayPage
    {
        
            IWebDriver? driver;

            public ProductDisplayPage(IWebDriver driver)
            {
                this.driver = driver;
                PageFactory.InitElements(driver, this);
            }


        [FindsBy(How = How.XPath, Using = "(//button[@title='Add to Cart' ])[1]")]
        private IWebElement? AddProduct { get; set; }

        public void SelectProductLink(string num)
        {
            IWebElement selectProduct = driver.FindElement(By.XPath("(//img[@class='curved'])" + "[" + num + "]"));
            selectProduct.Click();
        }

        public CartPage AddProductLink()
        {
            AddProduct?.Click();
            return new CartPage(driver);
        }

    }
}
