using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UITests.PageObjectModels
{
    public class HomePage
    {
        private const string PageUrl = "https://localhost:44315/";
        private const string PageTitle = "Catalog - Microsoft.eShopOnWeb";

        private readonly IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GenerationToken => driver.FindElement(By.Id("token")).Text;

        public ReadOnlyCollection<string> DropdownMenuItemTexts
        {
            get
            {
                var dropdownMenuItemTexts = new List<string>();
                foreach (var dropDownMenuItem in driver.FindElements(By.ClassName("esh-identity-item")))
                {
                    var div = dropDownMenuItem.FindElement(By.TagName("div"));
                    dropdownMenuItemTexts.Add(div.Text.ToLower());
                }
                return dropdownMenuItemTexts.AsReadOnly();
            }
        }

        internal void DisplayDropdownMenuItems() => driver.FindElement(By.ClassName("esh-identity-name")).Click();
        internal string GetIdentityName() => driver.FindElement(By.ClassName("esh-identity-name")).Text;

        public void NavigateTo()
        {
            driver.Navigate().GoToUrl(PageUrl);
            EnsurePageLoaded();
        }

        public void EnsurePageLoaded()
        {
            bool pageHasLoaded = driver.Url == PageUrl && driver.Title == PageTitle;

            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = '{driver.Url}' Page Source: \r\n {driver.PageSource}");
            }
        }

        internal LoginPage ClickLoginPageLink()
        {
            driver.FindElement(By.ClassName("esh-identity-name--upper")).Click();
            return new LoginPage(driver);
        }
    }
}
