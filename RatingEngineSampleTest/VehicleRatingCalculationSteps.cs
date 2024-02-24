using System;
using TechTalk.SpecFlow;

namespace RatingEngineSampleTest
{
    [Binding]
    public class VehicleRatingCalculationSteps
    {
        [Given(@"I have a vehicle policy with following data")]
        public void GivenIHaveAVehiclePolicyWithFollowingData(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I calculate policy rating")]
        public void WhenICalculatePolicyRating()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be (.*)m")]
        public void ThenTheResultShouldBeM(int p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
