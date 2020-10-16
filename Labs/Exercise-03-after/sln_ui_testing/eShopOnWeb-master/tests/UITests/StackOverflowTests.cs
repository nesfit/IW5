using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UITests.Helpers;

namespace UITests
{
    public class StackOverflowTests
    {
        private IWebDriver webDriver;

        [SetUp]
        public void SetUp()
        {
            webDriver = new ChromeDriver();
            webDriver.Navigate().GoToUrl("https://stackoverflow.com/");
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
        }

        [Test]
        public void CookiesManipulationTest()
        {
            // Assert
            webDriver.FindElement(By.Id("js-gdpr-consent-banner")).Text.Should().Be("By using our site, you acknowledge that you have read and understand our Cookie Policy, Privacy Policy, and our Terms of Service.");
            webDriver.Manage().Cookies.AllCookies.Should().HaveCount(5);
            DemoHelper.Pause(2);

            webDriver.FindElement(By.CssSelector("body.home-page.unified-theme:nth-child(2) div.p8.ff-sans.ps-fixed.b0.l0.r0.z-banner:nth-child(4) div.wmx8.mx-auto.grid.grid__center div.grid--cell:nth-child(2) > a.s-btn.s-btn__muted.s-btn__icon.js-notice-close")).Click();
            webDriver.Manage().Cookies.AllCookies.Should().HaveCount(6);
            DemoHelper.Pause(2);

            webDriver.Manage().Cookies.DeleteAllCookies();
            webDriver.Manage().Cookies.AllCookies.Should().HaveCount(0);
        }
    }
}
