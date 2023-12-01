using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshToHome.pageObjects
{
    internal class CartPage
    {
        IWebDriver driver;
        public CartPage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName, Using = "menu-cart-icon")]
        private IWebElement? CartButton { get; set; }
        [CacheLookup]

        [FindsBy(How = How.XPath, Using = "//button[@title='Go to Checkout']")]
        private IWebElement CheckOutButton { get; set; }
        [CacheLookup]

        [FindsBy(How = How.XPath, Using = "//button[@title='Go to Cart Page']")]
        private IWebElement? MyCartButton { get; set; }
         [CacheLookup]

        [FindsBy(How = How.XPath, Using = "(//span[@class='dn'])[1]")]
        private IWebElement? IncQuantity { get; set; }

        [FindsBy(How = How.XPath, Using = "(//span[@class='up'])[1]")]
        private IWebElement? DecrQuantity { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@title='Remove item']")]
        private IWebElement? DeleteProduct { get; set; }

        [FindsBy(How = How.Id, Using = "empty_cart_button")]
        private IWebElement? ClearCartButton { get; set; }

        [FindsBy(How = How.XPath, Using = "(//button[@title='Continue Shopping'])[1]")]
        private IWebElement? ContinueShoppingButton { get; set; }

       
        public void ClickOnCartButton()
        {
            CartButton?.Click();
        }

        public void ClickOnCheckoutButton()
        {
            CheckOutButton?.Click();
        }

        public void ClickOnMyCartButton()
        {
            MyCartButton?.Click();
        }
        public void ClickOnIncQuantityButton()
        {
            IncQuantity?.Click();
        }
        public void ClickOnDecrQuantityButton()
        {
            DecrQuantity?.Click();
        }

        public void ClickOnDeleteProduct()
        {
            DeleteProduct?.Click();
        }

        public void ClickOnClearCartButton()
        { 
            ClearCartButton?.Click(); 
        }

        public void ClickOnContinueShoppingButton()
        {
            ContinueShoppingButton?.Click();
        }

    }
}
