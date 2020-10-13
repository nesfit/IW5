using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.IO;
using UITests.Helpers;
using UITests.PageObjectModels;

namespace UITests
{
    public class LoginPageTests : TestsBase
    {
        private const string LoginTitle = "Log in - Microsoft.eShopOnWeb";

        private IWebDriver webDriver;

        [SetUp]
        public void SetUp()
        {
            webDriver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
        }

        [Test]
        [Category("Smoke")]
        public void RedirectFromHomePageToLoginPage()
        {
            // Arrange
            var homePage = new HomePage(webDriver);
            homePage.NavigateTo();

            // Act
            var loginPage = homePage.ClickLoginPageLink();

            // Assert
            loginPage.EnsurePageLoaded();
        }

        [TestCase("demouser@microsoft.com", 3, new string[] { "my orders", "my account", "log out" })]
        [TestCase("admin@microsoft.com", 4, new string[] { "admin", "my orders", "my account", "log out" })]
        public void LoginAsAUser(string userEmail, int dropdownMenuItemsCount, params string[] dropdownMenuItemsText)
        {
            // Arrange
            var loginPage = new LoginPage(webDriver);
            loginPage.NavigateTo();

            // Act
            loginPage.EnterEmail(userEmail);
            loginPage.EnterPassword(ValidPassword);
            var homePage = loginPage.SubmitLoginForm();

            // Assert
            homePage.DisplayDropdownMenuItems();
            homePage.GetIdentityName().Should().Be(userEmail);
            DemoHelper.Pause();

            var dropdownMenuItemTexts = homePage.DropdownMenuItemTexts;
            dropdownMenuItemTexts.Should().HaveCount(dropdownMenuItemsCount);
            foreach (var dropdownMenuItemText in dropdownMenuItemTexts)
            {
                dropdownMenuItemsText.Should().Contain(dropdownMenuItemText);
            }
        }

        [TestCase("demouser@microsoft.com")]
        [TestCase("admin@microsoft.com")]
        public void LoginAsAUserAndLogout(string userEmail)
        {
            // Arrange
            webDriver.Navigate().GoToUrl(LoginUrl);

            // Act
            IWebElement emailInput = webDriver.FindElement(By.Id("Input_Email"));
            emailInput.SendKeys(userEmail);

            IWebElement passwordInput = webDriver.FindElement(By.Id("Input_Password"));
            passwordInput.SendKeys(ValidPassword);

            IWebElement loginButton = webDriver.FindElement(By.TagName("button"));
            loginButton.Click();

            IWebElement identityName = webDriver.FindElement(By.ClassName("esh-identity-name"));

            Actions actions = new Actions(webDriver);
            actions.MoveToElement(identityName).Click(webDriver.FindElement(By.XPath("//section[@class='esh-identity-drop']//a[last()]"))).Build().Perform();

            // Assert
            webDriver.Url.Should().Be(HomeUrl);

            identityName = webDriver.FindElement(By.ClassName("esh-identity-name"));
            identityName.Text.ToLower().Should().Be("login");
        }

        [Test]
        [UseReporter(typeof(TortoiseImageDiffReporter))]
        //[UseReporter(typeof(BeyondCompareReporter))]
        public void LoginPageRenderingTest()
        {
            // Arrange
            webDriver.Navigate().GoToUrl(LoginUrl);

            // Act
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)webDriver;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile("loginpage.png");

            // Assert
            FileInfo file = new FileInfo("loginpage.png");
            Approvals.Verify(file);
        }
    }
}
