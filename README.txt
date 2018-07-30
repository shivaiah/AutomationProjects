This is developed on Visual Studio 2017 Community. 

Incase you use the same then make sure you install "SpecFlow integration for Visual Studio 2017" from the below link.
https://marketplace.visualstudio.com/items?itemName=TechTalkSpecFlowTeam.SpecFlowforVisualStudio2017


Make sure you add all the below nuget packages. You could install exactly below packages or even latest packages.

DotNetSeleniumExtras.PageObjects  3.11.0
Newtonsoft.Json 11.0.2 james Newtonking
Selenium.PhantomJS.WebDriver 2.1.1 by jbaranda 
Selenium.Support 3.13.1
Selenium.WebDriver 3.13.1
Selenium.WebDriver.ChromeDriver 2.41.0
SpecFlow 2.3.2
SpecRun.Runner 1.7.2
SpecRun.SpecFlow 1.7.2
SpecRun.SpecFlow.2-3-0 1.7.2

Once you are done with installation of all the above packages, add all the required references to your solution

Build the solution

Please goto the below path from "AutomationDemo\packages\SpecRun.Runner.1.7.2\templates" and copy 'ReportTemplate.cshtml'

Paste the copied report template to the path "AutomationDemo\AutomationDemo\bin\Debug"

The above two steps are for generating specrun reports to TestResults folder





