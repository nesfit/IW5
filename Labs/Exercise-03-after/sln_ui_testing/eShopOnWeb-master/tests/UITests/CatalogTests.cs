using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UITests.Helpers;

namespace UITests
{
    public class CatalogTests : TestsBase
    {
        private IWebDriver webDriver;

        [SetUp]
        public void SetUp()
        {
            webDriver = new ChromeDriver();
            webDriver.Navigate().GoToUrl(HomeUrl);
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
        }


        [Test]
        [Category("Catalog")]
        public void NavigateToTheNextPageInTheCalatog_AddedQueryParameterWithPageIdOne()
        {
            // Act
            IWebElement nextLink = webDriver.FindElement(By.Name("Next"));
            nextLink.Click();

            webDriver.Url.Should().Be($"{HomeUrl}?pageId=1");
            webDriver.Title.Should().Be(HomeTitle);
        }

        [Test]
        [Category("Catalog")]
        public void NavigateToTheNextPageThenPreviousInTheCatalog_AddedQueryParameterWithPageIdZero()
        {
            // Act
            IWebElement nextLink = webDriver.FindElement(By.Name("Next"));
            nextLink.Click();

            IWebElement previousLink = webDriver.FindElement(By.Id("Previous"));
            previousLink.Click();
            DemoHelper.Pause(3);

            webDriver.Url.Should().Be($"{HomeUrl}?pageId=0");
            webDriver.Title.Should().Be(HomeTitle);
        }

        [Test]
        [Category("Catalog")]
        public void CheckingSelectElementsDefaultSelectedOption()
        {
            // Assert
            IWebElement brandFilterSelectElement = webDriver.FindElement(By.Id("CatalogModel_BrandFilterApplied"));
            SelectElement brandFilter = new SelectElement(brandFilterSelectElement);

            IWebElement typesFilterSelectElement = webDriver.FindElement(By.Id("CatalogModel_TypesFilterApplied"));
            SelectElement typesFilter = new SelectElement(typesFilterSelectElement);

            brandFilter.SelectedOption.Text.Should().Be("All");
            typesFilter.SelectedOption.Text.Should().Be("All");
        }

        private List<(string Value, string Text)> GetTypesFilterValuesAndTexts() => new List<(string, string)>
        {
            ("All", "All"),
            ("1", "Mug"),
            ("3", "Sheet"),
            ("2", "T-Shirt"),
            ("4", "USB Memory Stick"),
        };

        [Test]
        [Category("Catalog")]
        public void CheckingSelectElementsOptions()
        {
            // Arrange
            List<Tuple<string, string>> brandFilterValuesAndTexts = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("All", "All"),
                new Tuple<string, string>("2", ".NET"),
                new Tuple<string, string>("1", "Azure"),
                new Tuple<string, string>("5", "Other"),
                new Tuple<string, string>("4", "SQL Server"),
                new Tuple<string, string>("3", "Visual Studio")
            };

            var typesFilterValuesAndTexts = GetTypesFilterValuesAndTexts();

            // Assert
            IWebElement brandFilterSelectElement = webDriver.FindElement(By.Id("CatalogModel_BrandFilterApplied"));
            SelectElement brandFilter = new SelectElement(brandFilterSelectElement);

            IWebElement typesFilterSelectElement = webDriver.FindElement(By.Id("CatalogModel_TypesFilterApplied"));
            SelectElement typesFilter = new SelectElement(typesFilterSelectElement);

            brandFilter.Options.Count.Should().Be(brandFilterValuesAndTexts.Count);
            typesFilter.Options.Count.Should().Be(typesFilterValuesAndTexts.Count);

            for (int i = 0; i < brandFilter.Options.Count; i++)
            {
                brandFilter.Options[i].GetAttribute("value").Should().Be(brandFilterValuesAndTexts[i].Item1);
                brandFilter.Options[i].Text.Should().Be(brandFilterValuesAndTexts[i].Item2);
            }

            typesFilter.Options.Select(option => new { Value = option.GetAttribute("value"), option.Text })
                .Should().Equal(typesFilterValuesAndTexts.Select(x => new { x.Value, x.Text }));
        }

        [Test]
        [Category("Catalog")]
        public void SelectBrandAzureAndTypeMug_NoItemsAreDisplayedInCatalog()
        {
            // Act
            IWebElement brandFilterSelectElement = webDriver.FindElement(By.Id("CatalogModel_BrandFilterApplied"));
            new SelectElement(brandFilterSelectElement).SelectByValue("1");

            IWebElement typesFilterSelectElement = webDriver.FindElement(By.Id("CatalogModel_TypesFilterApplied"));
            new SelectElement(typesFilterSelectElement).SelectByText("Mug");

            webDriver.FindElement(By.TagName("input")).Click();

            // Assert
            IWebElement catalogItemsRow = webDriver.FindElement(By.ClassName("esh-catalog-items"));
            catalogItemsRow.Text.Should().Be("THERE ARE NO RESULTS THAT MATCH YOUR SEARCH");
        }

        [Test]
        [Category("Catalog")]
        public void SelectBrandDotNetAndTypeSheet_TwoSheetsAreDisplayedOnCatalogPage()
        {
            // Act
            IWebElement brandFilterSelectElement = webDriver.FindElement(By.Id("CatalogModel_BrandFilterApplied"));
            new SelectElement(brandFilterSelectElement).SelectByText(".NET");

            IWebElement typesFilterSelectElement = webDriver.FindElement(By.Id("CatalogModel_TypesFilterApplied"));
            new SelectElement(typesFilterSelectElement).SelectByText("Sheet");

            webDriver.FindElement(By.TagName("input")).Click();

            // Assert
            IWebElement pagerItem = webDriver.FindElement(By.XPath("//div[@class='container']//div[1]//div[1]//article[1]//nav[1]//div[2]//span[1]"));
            pagerItem.Text.Should().Be("Showing 2 of 2 products - Page 1 - 1");

            var items = webDriver.FindElements(By.ClassName("esh-catalog-item"));
            items.Should().HaveCount(2);
            items[0].Text.Should().Be($".NET FOUNDATION SHEET{Environment.NewLine}12,00");
            items[1].Text.Should().Be($"CUP<T> SHEET{Environment.NewLine}8,50");
        }
    }
}
