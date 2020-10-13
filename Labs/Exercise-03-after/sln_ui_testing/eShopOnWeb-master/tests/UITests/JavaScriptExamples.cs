using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using UITests.Helpers;

namespace UITests
{
    public class JavaScriptExamples
    {
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
        public void ClickOverlayedButton()
        {
            // Arrange
            string path = AppContext.BaseDirectory;
            string pathToTestTheFile = Path.GetFullPath(Path.Combine(path, "../../../../../src-for-testing/OverlayTest.html"));
            webDriver.Navigate().GoToUrl(pathToTestTheFile);

            // Act
            webDriver.FindElement(By.Id("open-overlay")).Click();

            string script1 = "document.getElementById('test-button').click()";
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)webDriver;

            javaScriptExecutor.ExecuteScript(script1);
            DemoHelper.Pause();

            javaScriptExecutor.ExecuteScript(script1);
            DemoHelper.Pause();

            javaScriptExecutor.ExecuteScript(script1);
            DemoHelper.Pause();

            // Assert
            webDriver.FindElement(By.Id("pText")).Text.Should().Be("You can find the original code here https://jsfiddle.net/agib/jratazn7/. AAA AAA AAA");

            string script2 = "return document.getElementById('pText').innerHTML;";
            string paragraphText = (string)javaScriptExecutor.ExecuteScript(script2);

            paragraphText.Should().Be("You can find the original code here https://jsfiddle.net/agib/jratazn7/. AAA  AAA  AAA ");
        }
    }
}
