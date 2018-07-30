using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Support.PageObjects;
using AutomationDemo.TestUtilities;

namespace AutomationDemo.ApplicationPageClasses
{
    public abstract class BasePage
    {
        protected IWebDriver browser;

        [FindsBy(How = How.CssSelector, Using = "div#acsMainInvite")]
        protected IWebElement FeedBackPopUp;

        protected abstract void WaitForPageLoad();
        /// <summary>
        /// Constructor for Base Page
        /// </summary>
        /// <param name="browser"></param>
        protected BasePage(IWebDriver browser)
        {
            try
            {
                this.browser = browser;
                PageFactory.InitElements(browser, this);
                WaitForPageLoad();
            }
            catch (System.AggregateException ex)
            {
                Reporter.EventDone("Page", "Exception : " + ex.InnerException);
                throw ex;
            }
        }
        /// <summary>
        /// This method takes Webelement as input and rteurns true if it is present in the webpage else false
        /// </summary>
        /// <param name="IWebElement"></param>
        /// <returns>bool</returns>
        protected bool IsWebElementPresent(IWebElement WebElement)
        {
            try
            {
                return (WebElement.Displayed && WebElement.Enabled) ? true : false;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// Method to check if webelement is present after waiting for certain time in seconds
        /// </summary>
        /// <param name="WebElement"></param>
        /// <param name="WaitTimeInSeconds"></param>
        /// <returns>bool</returns>
        protected bool IsWebElementPresentAfterWait(IWebElement WebElement, int WaitTimeInSeconds)
        {
            try
            {
                new WebDriverWait(browser, TimeSpan.FromSeconds(WaitTimeInSeconds)).Until(webDriver => WebElement.Displayed);
                return true;
            }
            catch (WebDriverException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Method to enter data into any field passed as Webelement
        /// </summary>
        /// <param name="WebElement"></param>
        /// <param name="InputValue"></param>
        protected void EnterDataIntoField(IWebElement WebElement, string InputValue)
        {
            try
            {
                if (IsWebElementPresent(WebElement))
                {
                    WebElement.Click();
                    WebElement.Clear();
                    while (WebElement.GetAttribute("value") != "")
                        WebElement.SendKeys(Keys.Backspace);
                    WebElement.SendKeys(InputValue);
                }
            }
            catch (Exception EnterDataIntoFieldException)
            {
                Reporter.Warning("EnterDataIntoField" , "Failed to enter the text");
                throw EnterDataIntoFieldException;
            }
        }
        /// <summary>
        /// Removes the html code for feedback poppup from doc if exists
        /// </summary>
        protected void RemoveFeedBackPopUpFromDoc()
        {
            if (IsWebElementPresentAfterWait(FeedBackPopUp, 0))
            {

                IJavaScriptExecutor js = (IJavaScriptExecutor)browser;
                string jsScript = string.Format("document.getElementById('{0}').remove();", "acsMainInvite");
                js.ExecuteScript(jsScript);
            }

            if (IsWebElementPresentAfterWait(FeedBackPopUp, 1))
            {
                ((IJavaScriptExecutor)browser).ExecuteScript("$('" + "div#acsMainInvite" + "').hide();");
            }
        }
        /// <summary>
        /// checks if an attribute exists for a webelement and get's it's value is found
        /// </summary>
        /// <param name="WebElement"></param>
        /// <param name="AttributeName"></param>
        /// <param name="AttributeValue"></param>
        /// <returns></returns>
        protected bool TryIfGetAttributeValueSucceeds(IWebElement WebElement, string AttributeName, out string AttributeValue)
        {
            AttributeValue = null;
            try
            {
                AttributeValue = WebElement.GetAttribute(AttributeName).Trim().ToLower();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
