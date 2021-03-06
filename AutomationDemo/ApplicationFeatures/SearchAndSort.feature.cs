﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.2.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace AutomationDemo.ApplicationFeatures
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("SearchAndSort", Description="\tAs a product Owner\r\n\tI want to verify the Search and Sort functionalities of my " +
        "products ", SourceFile="ApplicationFeatures\\SearchAndSort.feature", SourceLine=0)]
    public partial class SearchAndSortFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SearchAndSort.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "SearchAndSort", "\tAs a product Owner\r\n\tI want to verify the Search and Sort functionalities of my " +
                    "products ", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [TechTalk.SpecRun.FeatureCleanup()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        [TechTalk.SpecRun.ScenarioCleanup()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 5
#line 6
 testRunner.Given("I have launched the WebApplication for blinds.com", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Verify Search", new string[] {
                "mytag"}, SourceLine=9)]
        public virtual void VerifySearch()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search", new string[] {
                        "mytag"});
#line 10
this.ScenarioSetup(scenarioInfo);
#line 5
this.FeatureBackground();
#line 11
   testRunner.Then("I should land on the homepage for blinds with url \"https://www.blinds.com/\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 12
   testRunner.When("I search for \"room darkening blinds\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 13
   testRunner.Then("I should see the results for different types of room darkening blinds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 14
   testRunner.And("I should see the page url having the search string \"room darkening blinds\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
   testRunner.And("I should see the \"default\" sort label highlighted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
   testRunner.When("I click the sort label for \"Price Low-High\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 17
   testRunner.Then("I should see the \"Price Low-High\" sort label highlighted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 18
   testRunner.And("I should see that the blinds are sorted as per the label \"Price Low-High\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
   testRunner.When("I click the sort label for \"Price High-Low\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 20
   testRunner.Then("I should see the \"Price High-Low\" sort label highlighted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 21
   testRunner.And("I should see that the blinds are sorted as per the label \"Price High-Low\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.TestRunCleanup()]
        public virtual void TestRunCleanup()
        {
            TechTalk.SpecFlow.TestRunnerManager.GetTestRunner().OnTestRunEnd();
        }
    }
}
#pragma warning restore
#endregion


