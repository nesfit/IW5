using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace UITests
{
    public class MyAccountTests : TestsBase
    {
        [Test]
        public void PhoneNumberIsEmptyByDefault()
        {
            using (IWebDriver webDriver = new ChromeDriver())
            {
                // Arrange
                webDriver.Navigate().GoToUrl(LoginUrl);

                // Act
                IWebElement emailInput = webDriver.FindElement(By.Id("Input_Email"));
                emailInput.SendKeys("demouser@microsoft.com");

                IWebElement passwordInput = webDriver.FindElement(By.Id("Input_Password"));
                passwordInput.SendKeys(ValidPassword);

                IWebElement loginButton = webDriver.FindElement(By.TagName("button"));
                loginButton.Click();

                IWebElement identityName = webDriver.FindElement(By.ClassName("esh-identity-name"));

                Actions action = new Actions(webDriver);
                action.MoveToElement(identityName).Click(webDriver.FindElement(By.XPath("//section[@class='esh-identity-drop']//a[@href=\"/manage/my-account\"]"))).Build().Perform();

                // Assert
                IWebElement phoneNumber = webDriver.FindElement(By.Id("PhoneNumber"));
                phoneNumber.Text.Should().BeEmpty();
            }
        }
    }
}
