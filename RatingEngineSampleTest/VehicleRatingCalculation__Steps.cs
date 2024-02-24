//using System;
//using System.IO;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Newtonsoft.Json;
//using RatingEngineSample;
//using TechTalk.SpecFlow;
//using TechTalk.SpecFlow.Assist;

//namespace RatingEngineSampleTest
//{
//    [Binding]
//    public class VehicleRatingCalculationSteps
//    {
//        RatingEngine ratingEngine;
//        decimal rating;
//        [Given(@"I have a vehicle policy with following data")]
//        public void GivenIHaveAVehiclePolicyWithFollowingData(Table table)
//        {
//            //policy = new Policy
//            //{
//            //    Type = PolicyType.Vehicle,
//            //    Miles = 20000,
//            //    Year = 2000,
//            //    Price = 10000000
//            //};
//            var policy = table.CreateInstance<Policy>();
//            string json = JsonConvert.SerializeObject(policy);
//            File.WriteAllText("policy.json", json);
//        }

//        [When(@"I calculate policy rating")]
//        public void WhenICalculatePolicyRating()
//        {
//            rating = ratingEngine.Rate();
//        }

//        [Then(@"the result should be (.*)")]
//        public void ThenTheResultShouldBe(decimal p0)
//        {
//            Assert.AreEqual(p0, rating);
//        }

//    }
//}
