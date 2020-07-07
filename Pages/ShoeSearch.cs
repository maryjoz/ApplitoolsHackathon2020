using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ApplitoolsHackathon2020.Pages
{
    // page model to keep page methods and locators separate from the tests
    public class ShoeSearch
    {
        #region Private Fields

        private IWebDriver _driver;

        #endregion Private Fields

        #region Public Constructors

        public ShoeSearch(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        #endregion Public Constructors

        #region Public Properties

        public IWebElement SideBarFilters => _driver.FindElement(By.Id("sidebar_filters"));
        public IWebElement BlackFilter => _driver.FindElement(By.Id("colors__Black"));
        public IWebElement FilterButton => _driver.FindElement(By.Id("filterBtn"));
        public IWebElement ResetButton => _driver.FindElement(By.Id("resetBtn"));
        public IList<IWebElement> Products => _driver.FindElements(By.Id("product_"));
        public IWebElement FirstBlackShoe => _driver.FindElement(By.Id("product_1"));
        public IWebElement OpenFilter => _driver.FindElement(By.Id("ti-filter"));

        #endregion Public Properties

        #region Public Methods

        public void Open(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        #endregion Public Methods
    }
}