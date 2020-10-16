using OpenQA.Selenium;
using System;

namespace UITests.PageObjectModels
{
    public class LoginPage
    {
        private const string PageUrl = "https://localhost:44315/Identity/Account/Login";
        private const string PageTitle = "Log in - Microsoft.eShopOnWeb";

        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        internal void EnterEmail(string userEmail) => driver.FindElement(By.Id("Input_Email")).SendKeys(userEmail);
        internal void EnterPassword(string password) => driver.FindElement(By.Id("Input_Password")).SendKeys(password);
        internal HomePage SubmitLoginForm()
        {
            driver.FindElement(By.TagName("button")).Click();
            return new HomePage(driver);
        }

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
    }
}
