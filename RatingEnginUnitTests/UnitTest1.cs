using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RatingEngineSample;

namespace RatingEnginUnitTests
{
    [TestClass]
    public class RatingEngineTest
    {
        [TestMethod]
        public void ReturnsRatingOf10000For200000LandPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Auto,
                Miles = 20000,
                Year = 2000,
                Price = 10000000
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.AreEqual(10000, result);
        }
    
    }
}
