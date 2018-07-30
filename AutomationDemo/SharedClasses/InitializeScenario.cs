using AutomationDemo.ApplicationPageClasses;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AutomationDemo.SharedClasses
{
    public class InitializeScenario
    {
        protected Application application;
        protected IWebDriver browser;
        protected HomePage homePage;

        public InitializeScenario()
        {
            ScenarioContext.Current.TryGetValue<IWebDriver>(out browser);
        }
    }
}
