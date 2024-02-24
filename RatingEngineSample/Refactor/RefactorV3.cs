using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingEngineSample.Refactor
{
    public class RatingEngine
    {
        ILogger _logger;
        ISerializer _serializer;
        public RatingEngine(ILogger logger, ISerializer serializer)
        {
            _serializer = serializer;
            _logger = logger;
        }

        public decimal Rating { get; set; }
        public void Rate()
        {
            _logger.Log("Starting rate.");

            _logger.Log("Loading policy.");

            // load policy - open file policy.json
            string policyJson = "";// File.ReadAllText("policy.json");????

            var policy = _serializer.DeserializeObject<Policy>(policyJson);
            IPolicyRatingCalculator ratingCalculator = PolicyRatingCalculatorFactory.GetCalculator(policy.Type);
            ratingCalculator.CalculateRate(policy);

            _logger.Log("Rating completed.");
        }
    }



    public class PolicyRatingCalculatorFactory
    {
        public static IPolicyRatingCalculator GetCalculator(PolicyType policyType)
        {
            switch (policyType)
            {
                case PolicyType.Life:
                    return new LifePolicyRateCalculator3();
                    break;
                case PolicyType.Vehicle:
                    return new AutoPolicyRateCalculator3();
                    break;
                case PolicyType.Accident:
                    //return new LifePolicyRateCalculator3();
                    //break;
                default:
                    throw new NotSupportedException();
            }
        }
    }

    public interface IPolicyRatingCalculator
    {
        decimal CalculateRate(Policy policy);
    }

    public class LifePolicyRateCalculator3 : IPolicyRatingCalculator
    {
        public decimal Rating { get; set; }
        ILogger _logger;
        public decimal CalculateRate(Policy policy)
        {
            _logger.Log("Rating LIFE policy...");
            _logger.Log("Validating policy.");

            ValidateDateOfBirth(policy.DateOfBirth);
            ValidatePolicyAmount(policy.Amount);

         
            var age = CalculateAge(policy.DateOfBirth);
            decimal baseRate = policy.Amount * age / 200;
            if (policy.IsSmoker)
            {
                Rating = baseRate * 2;                
            }

            Rating = baseRate;
            return Rating;
        }

        public void ValidatePolicyAmount(decimal amount)
        {
            if (amount == 0)
            {
                _logger.Log("Life policy must include an Amount.");
                throw new Exception();
            }
        }

        private void ValidateDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth == DateTime.MinValue)
            {
                _logger.Log("Life policy must include Date of Birth.");
                throw new Exception();
            }

            if (dateOfBirth < DateTime.Today.AddYears(-100))
            {
                _logger.Log("Centenarians are not eligible for coverage.");
                throw new Exception();
            }
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Month == DateTime.Today.Month &&
                DateTime.Today.Day < dateOfBirth.Day ||
                DateTime.Today.Month < dateOfBirth.Month)
            {
                age--;
            }
            return age;
        }

        public decimal Calculate(Policy policy)
        {
            throw new NotImplementedException();
        }
    }

    public class AutoPolicyRateCalculator3 : IPolicyRatingCalculator
    {
        public decimal Rating { get; set; }
        ILogger _logger;
        public decimal CalculateRate(Policy policy)
        {
            _logger.Log("Rating Vehicle policy...");
            _logger.Log("Validating policy.");
            if (DateTime.Now.Year - policy.Year < 5)
            {
                Rating = policy.Price * (5 / 100);
            }
            else
            {
                Rating = policy.Price * (9 / 100);

            }
            return Rating;
        }
    }


    public interface ILogger
    {
        void Log(string text);
    }

    public class ConsoleLoger : ILogger
    {
        public void Log(string text)
        {
            throw new NotImplementedException();
        }
    }

    public interface ISerializer
    {
        string Serialaze(object obj);
        T DeserializeObject<T>(string objString);
    }
}
