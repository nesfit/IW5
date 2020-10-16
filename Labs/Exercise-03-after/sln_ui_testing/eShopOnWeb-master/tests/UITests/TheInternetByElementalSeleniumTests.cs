using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace UITests
{
    public class TheInternetByElementalSeleniumTests
    {
        private IWebDriver webDriver;

        [SetUp]
        public void Setup()
        {
            webDriver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
        }

        [Test]
        [Category("TheInternet")]
        public void DynamicLoadingHiddenElementTest()
        {
            // Arrange
            webDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");

            // Act
            IWebElement helloWorldDiv = webDriver.FindElement(By.Id("finish"));
            TestContext.WriteLine($"Enabled: {helloWorldDiv.Enabled} Displayed: {helloWorldDiv.Displayed}");

            IWebElement startButton = webDriver.FindElement(By.TagName("button"));
            startButton.Click();

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("finish")));

            TestContext.WriteLine($"Enabled: {helloWorldDiv.Enabled} Displayed: {helloWorldDiv.Displayed}");

            // Assert
            helloWorldDiv.Text.Should().Be("Hello World!");
        }

        [Test]
        [Category("TheInternet")]
        public void DynamicLoadingElementRenderedAfterTest()
        {
            // Arrange
            webDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");

            // Act
            IWebElement startButton = webDriver.FindElement(By.TagName("button"));
            startButton.Click();

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(6));
            IWebElement helloWorldDiv = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("finish"))); // ElementIsVisible

            TestContext.WriteLine($"Enabled: {helloWorldDiv.Enabled} Displayed: {helloWorldDiv.Displayed}");

            // Assert
            helloWorldDiv.Text.Should().Be("Hello World!");
        }

        [Test]
        [Category("TheInternet")]
        public void DynamicLoadingElementRenderedAfterTest_WithImplicitWait()
        {
            // Arrange
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            webDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");

            // Act
            IWebElement startButton = webDriver.FindElement(By.TagName("button"));
            startButton.Click();

            IWebElement helloWorldDiv = webDriver.FindElement(By.Id("finish"));

            TestContext.WriteLine($"Enabled: {helloWorldDiv.Enabled} Displayed: {helloWorldDiv.Displayed}");

            // Assert
            helloWorldDiv.Text.Should().Be("Hello World!");
        }

        [Test]
        [Category("TheInternet")]
        public void CheckBoxesSelectionTest()
        {
            // Arrange
            webDriver.Navigate().GoToUrl("http://the-internet.herokuapp.com/checkboxes");

            // Act
            ReadOnlyCollection<IWebElement> checkBoxes = webDriver.FindElements(By.TagName("input"));
            checkBoxes[0].Click();
            checkBoxes[1].Click();

            // Assert
            checkBoxes[0].Selected.Should().BeTrue();
            checkBoxes[1].Selected.Should().BeFalse();
        }

        [Test]
        [Category("TheInternet, Alert")]
        public void AlertPopupDisplaysExpectedValue()
        {
            // Arrange
            webDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/context_menu");

            // Act
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(webDriver.FindElement(By.Id("hot-spot"))).ContextClick().Build().Perform();

            IAlert alert = webDriver.SwitchTo().Alert();

            // Assert
            alert.Text.Should().Be("You selected a context menu");
            alert.Accept();
        }

        [Test]
        public void AlertPopupWithVariableTimeoutDisplaysExpectedValue()
        {
            // Arrange
            string path = AppContext.BaseDirectory;
            string pathToTestTheFile = Path.GetFullPath(Path.Combine(path, "../../../../../src-for-testing/AlertPopupWithTimeout.html"));
            webDriver.Navigate().GoToUrl(pathToTestTheFile);

            // Act
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(webDriver.FindElement(By.Id("hot-spot"))).ContextClick().Build().Perform();

            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(webDriver);
            fluentWait.Timeout = TimeSpan.FromSeconds(11);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(1000);
            // Ignore the exceptions NoAlertPresentException that indicates that the element is not present.
            fluentWait.IgnoreExceptionTypes(new Type[] { typeof(NoAlertPresentException) });
            fluentWait.Message = "Alert not present.";

            fluentWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

            IAlert alert = webDriver.SwitchTo().Alert();

            // Assert
            // https://fluentassertions.com/strings/
            //alert.Text.Should().Contain("You selected a context menu");
            //alert.Text.Should().Match("You selected a context menu + Timeout:*ms.");
            alert.Text.Should().MatchRegex(@"^You selected a context menu \+ Timeout: ([0-9]{4}|1[0-9]{3}0)ms\.$");
        }

        [Test]
        [Category("TheInternet, Alert")]
        public void JsAlert_Accepted()
        {
            // Arrange
            webDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

            // Act
            var buttons = webDriver.FindElements(By.TagName("button"));
            buttons[0].Click();

            IAlert alert = webDriver.SwitchTo().Alert();

            // Assert
            alert.Text.Should().Be("I am a JS Alert");
            alert.Accept();

            webDriver.FindElement(By.Id("result")).Text.Should().Be("You successfuly clicked an alert");
        }

        [Test]
        [Category("TheInternet, Alert")]
        public void JsConfirm_Accepted()
        {
            // Arrange
            webDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

            // Act
            var buttons = webDriver.FindElements(By.TagName("button"));
            buttons[1].Click();

            // Assert
            IAlert alert = webDriver.SwitchTo().Alert();
            alert.Text.Should().Be("I am a JS Confirm");
            alert.Accept();

            webDriver.FindElement(By.Id("result")).Text.Should().Be("You clicked: Ok");
        }

        [Test]
        [Category("TheInternet, Alert")]
        public void JsConfirm_Dismissed()
        {
            // Arrange
            webDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

            // Act
            var buttons = webDriver.FindElements(By.TagName("button"));
            buttons[1].Click();

            IAlert alert = webDriver.SwitchTo().Alert();

            // Assert
            alert.Text.Should().Be("I am a JS Confirm");
            alert.Dismiss();

            webDriver.FindElement(By.Id("result")).Text.Should().Be("You clicked: Cancel");
        }

        [Test]
        [Category("TheInternet, Alert")]
        public void JsPrompt_Accepted()
        {
            // Arrange
            webDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

            // Act
            var buttons = webDriver.FindElements(By.TagName("button"));
            buttons[2].Click();

            IAlert alert = webDriver.SwitchTo().Alert();

            // Assert
            alert.Text.Should().Be("I am a JS prompt");
            alert.SendKeys("Test");
            alert.Accept();

            webDriver.FindElement(By.Id("result")).Text.Should().Be("You entered: Test");
        }

        [Test]
        [Category("TheInternet, Alert")]
        public void JsPrompt_Dismissed()
        {
            // Arrange
            webDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

            // Act
            var buttons = webDriver.FindElements(By.TagName("button"));
            buttons[2].Click();

            IAlert alert = webDriver.SwitchTo().Alert();

            // Assert
            alert.Text.Should().Be("I am a JS prompt");
            alert.Dismiss();

            webDriver.FindElement(By.Id("result")).Text.Should().Be("You entered: null");
        }


        [Test]
        [Category("TheInternet")]
        public void MultipleTabsTest()
        {
            // Arrange
            webDriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/");

            // Act
            Actions actions = new Actions(webDriver);
            actions.KeyDown(Keys.LeftControl)
                .Click(webDriver.FindElement(By.LinkText("A/B Testing")))
                .Build()
                .Perform();

            var allTabs = webDriver.WindowHandles;
            string homePageTab = allTabs[0];
            string abTestingTab = allTabs[1];

            webDriver.SwitchTo().Window(abTestingTab);

            // Assert
            webDriver.Url.Should().Be("https://the-internet.herokuapp.com/abtest");
        }
    }
}
