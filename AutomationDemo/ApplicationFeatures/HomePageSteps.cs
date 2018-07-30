using AutomationDemo.SharedClasses;
using TechTalk.SpecFlow;

namespace AutomationDemo.ApplicationFeatures
{
    [Binding]
    class HomePageSteps : InitializeScenario
    {

        [Given(@"I have launched the WebApplication for blinds\.com")]
        public void GivenIHaveLaunchedTheWebApplicationForBlinds_Com()
        {
            Application webApplication = new Application(browser);
            ScenarioContext.Current.Set<Application>(webApplication);
            homePage = webApplication.LaunchWebApplication();
        }

        [Then(@"I should land on the homepage for blinds with url ""(.*)""")]
        public void ThenIShouldLandOnTheHomepageForBlindsWithUrl(string pageUrl)
        {
            homePage.VerifyPageUrl(pageUrl);
        }

        [When(@"I search for ""(.*)""")]
        public void WhenISearchFor(string searchText)
        {
            homePage.SearchForBlinds(searchText);
        }
        [Then(@"I should see the results for different types of room darkening blinds")]
        public void ThenIShouldSeeTheResultsForDifferentTypesOfRoomDarkeningBlinds()
        {
            homePage.VerifyIfOurSearchFetchedResults();
        }
        [Then(@"I should see the page url having the search string ""(.*)""")]
        public void ThenIShouldSeeThePageUrlHavingTheSearchString(string searchString)
        {
            string[] searchStrings = searchString.Split(new char[] { ' ', '\t' });
            homePage.VerifySearchResultsPageUrl(searchStrings);
        }

        [Then(@"I should see the ""(.*)"" sort label highlighted")]
        public void ThenIShouldSeeTheSortLabelHighlighted(string sortLabel)
        {
            homePage.VerifyIfSortLabelIsHighlighted(sortLabel);
        }

        [When(@"I click the sort label for ""(.*)""")]
        public void WhenIClickTheSortLabelFor(string sortLabel)
        {
            homePage.ClickSpecifiedSortLabelToSortBlinds(sortLabel)
                    .verifyIfSearchResultsAreRefreshed();
        }
        [Then(@"I should see that the blinds are sorted as per the label ""(.*)""")]
        public void ThenIShouldSeeThatTheBlindsAreSortedAsPerTheLabel(string sortLabel)
        {
            homePage.VerifyIfBlindProductsAreSorted(sortLabel);
        }
    }
}
