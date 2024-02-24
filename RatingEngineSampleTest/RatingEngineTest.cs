using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RatingEngineSample;
using Newtonsoft.Json;
using System.IO;

namespace RatingEngineSampleTest
{
    [TestClass]
    public class RatingEngineTest
    {
        [TestMethod]
        public void Calculate_Rating_Whith_Valid_VehiclePolicy_Info()
        {
            var policy = new Policy
            {
                Type = PolicyType.Vehicle,
                Miles = 20000,
                Year = 2000,
                Price = 10000000
            };
            string json = JsonConvert.SerializeObject(policy);
            File.WriteAllText("policy.json", json);

            var engine = new RatingEngine();
            engine.Rate();
            var result = engine.Rating;

            Assert.AreEqual(900000m, result);
        }

    }
}
