Feature:Vehicle Rating Calculation

Scenario: Calculate Vehicle Policy Rating
	Given I have a vehicle policy with following data
		| Type    | Miles | Year | Price    |
		| Vehicle | 20000 | 2000 | 10000000 |
	When I calculate policy rating
	Then the result should be 900000m