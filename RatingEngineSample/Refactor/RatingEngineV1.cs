using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingEngineSample.Refactor
{
    public class RatingEngineV1
    {
        private readonly ConsoleLogger logger;
        private readonly PolicyLoader policyLoader;
        private readonly AgeCalculator agecalculator;
        private readonly PolicySerilizer policySerilizer;
        public decimal Rating { get; set; }
        public void Rate()
        {
            logger.Log("Starting rate.");

            logger.Log("Loading policy.");

            // load policy - open file policy.json
            string policyJson = policyLoader.Load("policy.json");

            var policy = policySerilizer.DeserializePolicy(policyJson);

            switch (policy.Type)
            {
                case PolicyType.Vehicle:
                    logger.Log("Rating Vehicle policy...");
                    logger.Log("Validating policy.");

                    if (DateTime.Now.Year - policy.Year < 5)
                    {
                        Rating = policy.Price * (5 / 100);
                    }
                    else
                    {
                        Rating = policy.Price * (9 / 100);

                    }
                    break;

                case PolicyType.Life:
                    logger.Log("Rating LIFE policy...");
                    logger.Log("Validating policy.");
                    if (policy.DateOfBirth == DateTime.MinValue)
                    {
                        logger.Log("Life policy must include Date of Birth.");
                        return;
                    }
                    if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
                    {
                        logger.Log("Centenarians are not eligible for coverage.");
                        return;
                    }
                    if (policy.Amount == 0)
                    {
                        logger.Log("Life policy must include an Amount.");
                        return;
                    }
                    int age = agecalculator.CalculateAge(policy.DateOfBirth);

                    decimal baseRate = policy.Amount * age / 200;
                    if (policy.IsSmoker)
                    {
                        Rating = baseRate * 2;
                        break;
                    }
                    Rating = baseRate;
                    break;

                default:
                    logger.Log("Unknown policy type");
                    break;
            }

            logger.Log("Rating completed.");
        }
    }

    public class ConsoleLogger
    {
        public void Log(string msg)
        {
            Console.Write(msg);
        }
    }

    public class PolicySerilizer
    {
        public Policy DeserializePolicy(string policyJson)
        {
            //Formatting 
            return JsonConvert.DeserializeObject<Policy>(policyJson,
                new StringEnumConverter());
        }
    }

    public class AgeCalculator
    {
        public int CalculateAge(DateTime date)
        {
            int age = DateTime.Today.Year - date.Year;
            if (date.Month == DateTime.Today.Month &&
                DateTime.Today.Day < date.Day ||
                DateTime.Today.Month < date.Month)
            {
                age--;
            }
            return age;
        }
    }

    public class PolicyLoader
    {
        public string Load(string path)
        {
            return "";
        }
    }
}
