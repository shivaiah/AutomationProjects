Feature: SearchAndSort
	As a product Owner
	I want to verify the Search and Sort functionalities of my products 

Background: 
 Given I have launched the WebApplication for blinds.com
 

@mytag
Scenario: Verify Search
   Then I should land on the homepage for blinds with url "https://www.blinds.com/"
   When I search for "room darkening blinds"
   Then I should see the results for different types of room darkening blinds
   And I should see the page url having the search string "room darkening blinds"
   And I should see the "default" sort label highlighted
   When I click the sort label for "Price Low-High"
   Then I should see the "Price Low-High" sort label highlighted
   And I should see that the blinds are sorted as per the label "Price Low-High"
   When I click the sort label for "Price High-Low"
   Then I should see the "Price High-Low" sort label highlighted
   And I should see that the blinds are sorted as per the label "Price High-Low"
