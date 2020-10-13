using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UITests.PageObjectModels;

namespace UITests
{
    public class HomePageTests : TestsBase
    {
        [Test]
        [Category("Smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver webDriver = new ChromeDriver())
            {
                var homePage = new HomePage(webDriver);
                homePage.NavigateTo();
            }
        }

        [Test]
        [Category("Smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver webDriver = new ChromeDriver())
            {
                var homePage = new HomePage(webDriver);
                homePage.NavigateTo();

                webDriver.Navigate().Refresh();

                homePage.EnsurePageLoaded();
            }
        }

        [Test]
        [Category("Smoke")]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver webDriver = new ChromeDriver())
            {
                var homePage = new HomePage(webDriver);
                homePage.NavigateTo();

                string initialToken = homePage.GenerationToken;

                webDriver.Navigate().GoToUrl(LoginUrl);
                webDriver.Navigate().Back();

                homePage.EnsurePageLoaded();

                string reloadedToken = homePage.GenerationToken;
                reloadedToken.Should().NotBe(initialToken);
            }
        }

        [Test]
        [Category("Smoke")]
        public void ReloadHomePageOnForward()
        {
            using (IWebDriver webDriver = new ChromeDriver())
            {
                var homePage = new HomePage(webDriver);
                homePage.NavigateTo();

                string initialToken = homePage.GenerationToken;

                webDriver.Navigate().Back();
                webDriver.Navigate().Forward();

                homePage.EnsurePageLoaded();

                string reloadedToken = homePage.GenerationToken;
                reloadedToken.Should().NotBe(initialToken);
            }
        }
    }
}
