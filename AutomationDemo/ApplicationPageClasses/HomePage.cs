using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using AutomationDemo.TestUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace AutomationDemo.ApplicationPageClasses
{
    public class HomePage : BasePage
    {
        /// <summary>
        /// Web Elements for Home page
        /// </summary>

        [FindsBy(How = How.Id, Using = "gcc-header-navigation")]
        private IWebElement PageLoadElement { get; set; }

        [FindsBy(How = How.Id, Using = "gcc-inline-search")]
        private IWebElement SearchTextBox { get; set; }

        [FindsBy(How = How.Id, Using = "gcc-inline-search-submit")]
        private IWebElement SearchBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.tr")]
        private IWebElement BlindsSortLabelTab { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div#gcc-search-results-list")]
        private IWebElement BlindsSearchResultsSection { get; set; }

        string sortTypesCssSeletor = "ul.list.dib.m0.lh-copy.mr3-l >li";

        string blindsCssSelector = "article.gcc-box-1.flex-auto.flex-column.flex";

        string blindsPriceCssSelector = "div.f4.black.fw7 > span";

        /// <summary>
        /// Home Page Constructor
        /// </summary>
        /// <param name="browserDriver"></param>
        public HomePage(IWebDriver browserDriver) : base(browserDriver)
        {
            this.browser = browserDriver;
        }
        /// <summary>
        /// Override method for pageLoad
        /// </summary>
        protected override void WaitForPageLoad()
        {
            if (IsWebElementPresent(PageLoadElement))
            {
                Reporter.Pass("Wait for Blinds HomePage load", "HomePage for blinds is loaded successfully");
            }
            else
            {
                Reporter.Fail("Wait for Blinds HomePage load", "HomePage for blinds is not loaded successfully");
            }
        }
        /// <summary>
        /// Verifies page url
        /// </summary>
        /// <param name="expectedPageUrl"></param>
        /// <returns>HomePage</returns>
        public HomePage VerifyPageUrl(string expectedPageUrl)
        {
            if (browser.Url.Equals(expectedPageUrl))
            {
                Reporter.Pass("Wait for Blinds HomePage load", "HomePage for blinds is loaded successfully");
            }
            else
            {
                Reporter.Fail("Wait for Blinds HomePage load", "HomePage for blinds is not loaded successfully instead the page for "+ browser.Url+ " is loaded");
            }
            return this;
        }
        /// <summary>
        /// Verifies if search results page url has search string in it
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>HomePage</returns>
        public HomePage VerifySearchResultsPageUrl(string[] searchStrings)
        {
            int stringCounter = 0;
            foreach (string searchString in searchStrings)
            {
                if (browser.Url.Contains(searchString.Trim()))
                {
                    stringCounter++;
                }
            }
            if (stringCounter == searchStrings.Length)
            {
                Reporter.Pass("VerifySearchResultsPageUrl", "Search results page url is as expected");
            }
            else
            {
                Reporter.Warning("VerifySearchResultsPageUrl", "Search results page is as NOT as expected instead \"" + browser.Url + "\" is loaded");
            }
        return this;
        }
        /// <summary>
        /// Searches for blinds with the passed search string
        /// </summary>
        /// <param name="BlindsType"></param>
        /// <returns>HomePage</returns>
        public HomePage SearchForBlinds(string BlindsType)
        {
            if (IsWebElementPresent(SearchTextBox))
            {
                try
                {
                    EnterSearchText(BlindsType);
                    ClickSearch();
                }
                catch (Exception E)
                {
                    Reporter.Fail("SearchForBlinds", "Failed to enter the search text, please look into screenshots "+E.Message);
                }
            }
            return this;
        }
        /// <summary>
        /// Enters blinds search string
        /// </summary>
        /// <param name="SearchText"></param>
        private void EnterSearchText(string SearchText)
        {
            try
            {
                EnterDataIntoField(SearchTextBox, SearchText);
                if (SearchTextBox.GetAttribute("value").Contains(SearchText))
                {
                    Reporter.Pass("EnterSearchText", "Search Text for \"" + SearchText + "\" is entered");
                }
                else
                {
                    Reporter.Fail("EnterSearchText", "Search Text for \"" + SearchText + "\" is Not entered");
                }
            }
            catch(Exception EnterSearchTextException)
            {
                throw EnterSearchTextException;
            }
        }
        /// <summary>
        /// Clicks search icon
        /// </summary>
        private void ClickSearch()
        {
            try
            {
                if (IsWebElementPresent(SearchBtn))
                {
                    SearchBtn.Click();
                    waitForResultsPanelLoad();
                    Reporter.Pass("ClickSearch", "Search is hit successfully");
                }
                else
                {
                    Reporter.Fail("ClickSearch", "Failed to hit search");
                }
            }
            catch (Exception ClickSearchException)
            {
                throw ClickSearchException;
            }
        }
        /// <summary>
        /// Verifies if search fetched results
        /// </summary>
        /// <returns>HomePage</returns>
        public HomePage VerifyIfOurSearchFetchedResults()
        {
            try
            {
                if(IsWebElementPresent(BlindsSearchResultsSection))
                {
                    if(IsBlindsSearchSuccessfull())
                    {
                        Reporter.Pass("VerifyIfOurSearchFetchedResults", "Results are fetched successfully");
                    }
                    else
                    {
                        Reporter.Fail("VerifyIfOurSearchFetchedResults", "No Results are fetched");
                    }
                }

            }
            catch(Exception E)
            {
                Reporter.Fail("VerifyIfOurSearchFetchedResults", "Search resulted in No Results/exception"+E.Message);
            }
            return this;
        }

        private bool IsBlindsSearchSuccessfull()
        {
            try
            {
                IList<IWebElement> BlindsList = BlindsSearchResultsSection.FindElements(By.CssSelector(blindsCssSelector));
                return (BlindsList != null && BlindsList.Count >= 1) ? true : false;
            }
            catch(Exception IsBlindsSearchSuccessfullE)
            {
                throw IsBlindsSearchSuccessfullE;
            }
        }
        public HomePage verifyIfSearchResultsAreRefreshed()
        {
            waitForResultsPanelLoad();
            if (IsWebElementPresent(BlindsSearchResultsSection))
            {
                Reporter.EventDone("verifyIfSearchResultsAreRefreshed", "Results are refreshed successfully");
            }
            return this;
        }
        /// <summary>
        /// Clicks on specified sort label based on passed sort type
        /// </summary>
        /// <param name="SortType"></param>
        /// <returns>HomePage</returns>
        public HomePage ClickSpecifiedSortLabelToSortBlinds(string SortType)
        {
            try
            {
                if(IsBlindsSearchSuccessfull() && IsWebElementPresent(BlindsSortLabelTab))
                {
                    IWebElement RequiredBlindSortType = GetBlindsSortType(SortType);
                    if (RequiredBlindSortType != null)
                    {
                        RequiredBlindSortType.Click();
                        waitForResultsPanelLoad();
                        Reporter.EventDone("ClickSpecifiedSortLabelToSortBlinds", "Clicked \"" + SortType + "\" to sort the blinds");
                    }
                    else
                    {
                        Reporter.Warning("ClickSpecifiedSortLabelToSortBlinds", "There is no sort type of \"" + SortType +"\"");
                    }                    
                }
            }
            catch(Exception E)
            {
                Reporter.Fail("ClickSpecifiedSortLabelToSortBlinds", "Click Sort label failed : "+E.Message);
            }
            return this;
        }

        private void waitForResultsPanelLoad()
        {
            new WebDriverWait(browser, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("div#gcc-search-results-list")));
        }
        /// <summary>
        /// Returns required sort label of IWebElement type for blinds sorting
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns>IWebElement</returns>
        private IWebElement GetBlindsSortType(string sortType)
        {
            try
            {
                IList<IWebElement> BlindSortTypeList = BlindsSortLabelTab.FindElements(By.CssSelector(sortTypesCssSeletor));
                foreach (IWebElement BlindSortType in BlindSortTypeList)
                {
                    if (BlindSortType.Text.Trim().ToLower().Equals(sortType.Trim().ToLower()))
                        {
                        return BlindSortType;
                    }
                }
            }
            catch (Exception GetBlindsSortTypeException)
            {
                throw GetBlindsSortTypeException;
            }
            return null;
        }
        /// <summary>
        /// Verifies if sort label is highlighted
        /// </summary>
        /// <param name="SortType"></param>
        /// <returns>HomePage</returns>
        public HomePage VerifyIfSortLabelIsHighlighted(string SortType)
        {
            try
            {
                waitForResultsPanelLoad();
                if (IsBlindsSearchSuccessfull() && IsWebElementPresent(BlindsSortLabelTab))
                {
                    string BlindSortedType = GetBlindsSortedType();
                    if (BlindSortedType != null && SortType.Equals("default"))
                    {
                        Reporter.Pass("VerifyIfSortLabelIsHighlighted for \"" + SortType+"\" sort", "Sort label for \""+ BlindSortedType+ "\" is highlighted by default");
                    }
                    else if (BlindSortedType != null && !SortType.Equals("default") && BlindSortedType.ToLower().Equals(SortType.Trim().ToLower()))
                    {
                        Reporter.Pass("VerifyIfSortLabelIsHighlighted for \"" + SortType + "\" sort", "Sort label for \"" + BlindSortedType+"\" is highlighted");
                    }
                    else
                    {
                        Reporter.Fail("VerifyIfSortLabelIsHighlighted for \"" + SortType + "\" sort", "Sort label instead is highlighted by \"" + BlindSortedType+"\"");
                    }
                }
            }
            catch (Exception E)
            {
                Reporter.Fail("VerifyIfSortLabelIsHighlighted", "Either expected Sort Label \""+ SortType + "\" Is not highlighted/failed to identify the sort label : " + E.Message);
            }
            return this;
        }
        /// <summary>
        /// Gets Blinds sorted label
        /// </summary>
        /// <returns>string</returns>
        private string GetBlindsSortedType()
        {
            try
            {
                string AttributeValue;
                IList<IWebElement> BlindSortTypeList = BlindsSortLabelTab.FindElements(By.CssSelector(sortTypesCssSeletor));
                foreach (IWebElement BlindSortType in BlindSortTypeList)
                {
                    if (TryIfGetAttributeValueSucceeds(BlindSortType, "data-selected", out AttributeValue) && AttributeValue != null &&
                        AttributeValue.Equals("true"))
                    {
                        return BlindSortType.Text.Trim();
                    }
                }
            }
            catch (Exception GetBlindsSortTypeException)
            {
                throw GetBlindsSortTypeException;
            }
            return null;
        }
        /// <summary>
        /// Verifies if Blind products are sorted
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns>HomePage</returns>
        public HomePage VerifyIfBlindProductsAreSorted(string sortType)
        {
            try
            {
                waitForResultsPanelLoad();
                if (IsBlindsSearchSuccessfull() && IsWebElementPresent(BlindsSortLabelTab) && sortType.Trim().ToLower().Contains("price"))
                {
                    string BlindSortedType = GetBlindsSortedType();
                    if (BlindSortedType != null && AreProductsSorted(sortType))
                    {
                        Reporter.Pass("VerifyIfBlindProductsAreSorted for " + sortType + " sort", "Blinds are sorted by \"" + BlindSortedType+"\"");
                    }
                    else
                    {
                        Reporter.Fail("VerifyIfBlindProductsAreSorted for " + sortType + " sort", "Blinds are instead sorted by \"" + BlindSortedType + "\"");
                    }
                }
                else
                {
                    Reporter.Warning("VerifyIfBlindProductsAreSorted for " + sortType + " sort", "Automation for this is not implemented");
                }
            }
            catch(Exception E)
            {
                Reporter.Fail("VerifyIfBlindProductsAreSorted for " + sortType, "Products sorting resulted in exception :"+E.Message);
            }
            return this;
        }
        /// <summary>
        /// Checks if products are sorted according to selected sort label
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns>bool</returns>
        private bool AreProductsSorted(string sortType)
        {
            try
            {
                IList<IWebElement> BlindsList = BlindsSearchResultsSection.FindElements(By.CssSelector(blindsCssSelector));
                List<double> LeftColumnBlindsPriceList = new List<double>();
                List<double> RightColumnBlindsPriceList = new List<double>();
                    for (int index = 0; index < BlindsList.Count(); index++)
                    {
                        double productPrice = double.Parse(BlindsList[index].FindElement(By.CssSelector(blindsPriceCssSelector)).Text.Trim().Remove(0, 1));
                        if (IsEven(index))
                        {
                            LeftColumnBlindsPriceList.Add(productPrice);
                        }
                        else
                        {
                            RightColumnBlindsPriceList.Add(productPrice);
                        }
                    }
                if(sortType.Trim().ToLower().Contains("low-high"))
                {
                    if (IsSorted(LeftColumnBlindsPriceList, "ascending") && IsSorted(RightColumnBlindsPriceList, "ascending"))
                    {
                        return true;
                    }
                }
                else if (sortType.Trim().ToLower().Contains("high-low"))
                {
                    if (IsSorted(LeftColumnBlindsPriceList, "descending") && IsSorted(RightColumnBlindsPriceList, "descending"))
                    {
                        return true;
                    }
                }
            }
            catch (Exception AreProductsSortedE)
            {
                throw AreProductsSortedE;
            }
            return false;
        }
        /// <summary>
        /// Verifies if list of product columns are sorted
        /// </summary>
        /// <param name="ProductPriceList"></param>
        /// <param name="sortOrder"></param>
        /// <returns>bool</returns>
        private bool IsSorted(List<double> ProductPriceList, string sortOrder)
        {
            int ListCounter = 0;
            double previousPrice, currentPrice;
            previousPrice = currentPrice = ProductPriceList.ElementAt(0);
            if (sortOrder == "ascending")
            {
                for (int i = 0; i < ProductPriceList.Count - 1; i++)
                {
                    if (previousPrice <= currentPrice)
                    {
                        ListCounter++;
                        previousPrice = currentPrice;
                        currentPrice = ProductPriceList[i+1];
                    }
                }
            }
            if (sortOrder == "descending")
            {
                for (int i = 0; i < ProductPriceList.Count - 1; i++)
                {
                    if (previousPrice >= currentPrice)
                    {
                        ListCounter++;
                        previousPrice = currentPrice;
                        currentPrice = ProductPriceList[i+1];
                    }
                }
            }
            if (ListCounter == ProductPriceList.Count-1)
            {
                return true;
            }
            return false;
        }
        private bool IsEven(int index)
        {
           return index % 2 == 0 ? true : false;
        }
    }
}
