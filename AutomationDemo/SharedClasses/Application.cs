using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AutomationDemo.TestSettings;
using AutomationDemo.ApplicationPageClasses;
using AutomationDemo.TestUtilities;

namespace AutomationDemo.SharedClasses
{
    public class Application
    {
        private IWebDriver browser;
        protected TestProperties testProperties;
        protected string applicationUrl;
        protected Application webApplication;
        protected HomePage homePage;

        /// <summary>
        /// constructor for Application
        /// </summary>
        /// <param name="browser"></param>
        public Application(IWebDriver browser)
        {
            this.browser = browser;
        }

        public HomePage LaunchWebApplication()
        {
           try
            {
                ScenarioContext.Current.TryGetValue<TestProperties>(out TestProperties testProperties);
                string applicationUrl = testProperties.ApplicationUrl;
                if (!string.IsNullOrEmpty(applicationUrl))
                {
                    browser.Navigate().GoToUrl(applicationUrl);
                    PageRefresh();
                }
            }
            catch (WebDriverException WDE)
            {
                Reporter.Fail("openApplication", "Failed to Launch Application URL" + WDE);
            }
            return new HomePage(browser);
        }
        /// <summary>
        /// Close the browser
        /// </summary>
        public void CloseBrowser()
        {
            try
            {
                browser.Close();
                browser.Quit();
            }
            catch (WebDriverException WDE)
            {
                Reporter.Fail("close", "Failed to close driver" + WDE.Message);
            }
            catch (Exception E)
            {
                Reporter.Fail("close", "Failed to close driver" + E.Message);
            }
        }

        /// <summary>
        /// Refreshes the page
        /// </summary>
        public void PageRefresh()
        {
            browser.Navigate().Refresh();
        }
    }
}

