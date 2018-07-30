using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;
using System.Drawing;
using AutomationDemo.TestUtilities;
using AutomationDemo.TestSettings;

namespace AutomationDemo.SharedClasses
{
    [Binding]
    public class CommonSteps
    {
        protected ScenarioContext scenarioContextInstance;
        protected Application webApplication;
        protected IWebDriver browser;
        protected bool runInHeadlessChrome;
        protected string browserName;
        protected string projectRelativePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        protected Reporter report;
        protected TestProperties testProperties = new TestProperties();
        private readonly bool isLoggonEnabled = false;

        [BeforeScenario]
        public void BeforeScenario()
        {
            try
            {
                InitializeTestProperties();
                InitializeTestBrowserDriver();
                InitializeTestReport();
            }
            catch (Exception ex)
            {
                Reporter.Fail("BeforeScenario", ex.Message);
            }
        }
        /// <summary>
        /// Initialize test properties
        /// </summary>
        private void InitializeTestProperties()
        {
            browserName = testProperties.BrowserName;
            runInHeadlessChrome = testProperties.IsChromeHeadless;
            ScenarioContext.Current.Set<TestProperties>(testProperties);
        }
        /// <summary>
        /// Initialize browser driver
        /// </summary>
        private void InitializeTestBrowserDriver()
        {
            try
            {
                browser = getDriver(browserName);
                ScenarioContext.Current.Set<IWebDriver>(browser);
                if (browserName.Contains("phantom") || runInHeadlessChrome)
                {
                    browser.Manage().Window.Size = new Size(1280, 1024);
                }
                else
                {
                    browser.Manage().Window.Maximize();
                }
            }
            catch (WebDriverException)
            {
                throw new Exception(browserName + " is invalid");
            }
        }
        /// <summary>
        /// Initialize Test Report
        /// </summary>
        private void InitializeTestReport()
        {
            Reporter report = new Reporter(ScenarioContext.Current.ScenarioInfo.Title, browser);
            this.report = report;
        }
        /// <summary>
        /// gets web driver for browser passed
        /// </summary>
        /// <param name="browserName"></param>
        /// <returns></returns>
        private IWebDriver getDriver(string browserName)
        {

            IWebDriver driver = null;
            try
            {
                if (browserName.Equals("Chrome", StringComparison.CurrentCultureIgnoreCase) && !runInHeadlessChrome)
                {
                    ChromeOptions chromeOptions = new ChromeOptions();
                    driver = new ChromeDriver(projectRelativePath, chromeOptions);                    
                    return driver;
                }
                else if (browserName.Equals("Chrome", StringComparison.CurrentCultureIgnoreCase) && runInHeadlessChrome)
                {
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments(new List<string>() { "headless" });

                    driver = new ChromeDriver(projectRelativePath, chromeOptions);
                    return driver;
                }
                else if (browserName.Equals("Phantom", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (isLoggonEnabled)
                    {
                        var driverService = PhantomJSDriverService.CreateDefaultService();
                        driverService.LogFile = GetOrCreateLogFilePath(projectRelativePath);
                        driverService.AddArgument("--webdriver-loglevel=DEBUG");
                        driver = new PhantomJSDriver(driverService);

                    }
                    else
                    {
                        driver = new PhantomJSDriver(projectRelativePath);
                    }
                    return driver;
                }
                else
                {
                    throw new Exception("Invalid browser, please pass only chrome");
                }

                driver = new PhantomJSDriver(projectRelativePath);
                return driver;

            }
            catch (Exception E)
            {
                throw new WebDriverException(E.Message);
            }
        }

        private static string GetOrCreateLogFilePath(string logFilePath)
        {

            var fileName = logFilePath + "\\phantomjs_" + Guid.NewGuid().ToString().Replace("-", string.Empty) + ".log";
            return fileName;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                CloseApplication();
                report.CreateTestReport();
            }
            catch (WebDriverException WDE)
            {
                Reporter.Fail("AfterScenario ", "" + WDE.Message);
            }
            finally
            {
                if (browser != null)
                {
                    browser.Quit();
                }
                browser.Dispose();
                GC.SuppressFinalize(browser);
            }
        }
        /// <summary>
        /// Close the web application browser
        /// </summary>
        private void CloseApplication()
        {
            ScenarioContext.Current.TryGetValue<Application>(out webApplication);
            webApplication.CloseBrowser();
        }
    }
}
