using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingEngineSample.Refactor
{
    public class RatingEngineV2
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

            var policyRateCaclulator = PolicyRateCalculatorFactory.GetPolicyCalculator(policy.Type);
            var rateValue = policyRateCaclulator.Calculate(policy);

            logger.Log("Rating completed.");
        }
    }

    public class PolicyRateCalculatorFactory
    {
        public static IPolicyRateCalculator3 GetPolicyCalculator(PolicyType type)
        {
            switch (type)
            {
                case PolicyType.Life:
                    return new LifePolicyRateCalculator(new ConsoleLogger(), new AgeCalculator());

                case PolicyType.Vehicle:
                    return new AutoPolicyRateCalculator(new ConsoleLogger());

                case PolicyType.Accident:
                    return new AccidentPolicyCalculator(new ConsoleLogger());
                default:
                    throw new Exception("Not supported policy type Exception!!!");

            }

        }
    }
    public interface IPolicyRateCalculator3
    {
        decimal Calculate(Policy policy);

    }

    public abstract class PolicyRateCalculatorBase : IPolicyRateCalculator3
    {
        protected readonly ConsoleLogger logger;
        protected decimal Rating;
        public PolicyRateCalculatorBase(ConsoleLogger logger)
        {

        }

        //spublic abstract bool IsEligible(PolicyType policyType);
        public abstract decimal Calculate(Policy policy);

    }


    public class AccidentPolicyCalculator : PolicyRateCalculatorBase
    {
        public AccidentPolicyCalculator(ConsoleLogger logger) : base(logger)
        {
        }

        public override decimal Calculate(Policy policy)
        {
            throw new NotImplementedException();
        }
    }

    public class AutoPolicyRateCalculator : PolicyRateCalculatorBase
    {

        public AutoPolicyRateCalculator(ConsoleLogger logger) : base(logger)
        {

        }
        public override decimal Calculate(Policy policy)
        {
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

            return Rating;
        }
    }


    public class LifePolicyRateCalculator : PolicyRateCalculatorBase
    {
        private readonly AgeCalculator agecalculator;
        public LifePolicyRateCalculator(ConsoleLogger logger, AgeCalculator ageCalculator) : base(logger)
        {
            this.agecalculator = ageCalculator;
        }
        public override decimal Calculate(Policy policy)
        {
            logger.Log("Rating LIFE policy...");
            logger.Log("Validating policy.");
            if (policy.DateOfBirth == DateTime.MinValue)
            {
                logger.Log("Life policy must include Date of Birth.");
                //Exception
            }
            if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
            {
                logger.Log("Centenarians are not eligible for coverage.");
                //Exception
            }
            if (policy.Amount == 0)
            {
                logger.Log("Life policy must include an Amount.");
                //Exception
            }
            int age = agecalculator.CalculateAge(policy.DateOfBirth);

            decimal baseRate = policy.Amount * age / 200;
            if (policy.IsSmoker)
            {
                Rating = baseRate * 2;
                //break;
            }
            else
            {
                Rating = baseRate;
            }
            return Rating;
        }
    }
}
