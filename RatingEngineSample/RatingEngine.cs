using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;

namespace RatingEngineSample
{
    public class RatingEngine
    {
        private readonly IRatingEngine _ratingEngine;
        public RatingEngine(IRatingEngine ratingEngine)
        {
            _ratingEngine = ratingEngine;
        }
        public OprationResult Rateing()
        {
            Console.WriteLine("Starting rate.");
            string policyJson = File.ReadAllText("policy.json");
            var policy = JsonConvert.DeserializeObject<Policy>(policyJson,
            new StringEnumConverter());
           var result = _ratingEngine.Rate(policy);
            return result;
        }
        #region [ Old approach Rate ]
        //public decimal Rating { get; set; }
        //public void Rate()
        //{

        //    Console.WriteLine("Starting rate.");

        //    Console.WriteLine("Loading policy.");

        //    // load policy - open file policy.json
        //    string policyJson = File.ReadAllText("policy.json");

        //    var policy = JsonConvert.DeserializeObject<Policy>(policyJson,
        //        new StringEnumConverter());

        //    switch (policy.Type)
        //    {
        //        case PolicyType.Vehicle:
        //            Console.WriteLine("Rating Vehicle policy...");
        //            Console.WriteLine("Validating policy.");
        //            if (DateTime.Now.Year - policy.Year < 5)
        //            {
        //                Rating = policy.Price * (5m / 100);
        //            }
        //            else
        //            {
        //                Rating = policy.Price * (9m / 100);

        //            }
        //            break;

        //        case PolicyType.Life:
        //            Console.WriteLine("Rating LIFE policy...");
        //            Console.WriteLine("Validating policy.");
        //            if (policy.DateOfBirth == DateTime.MinValue)
        //            {
        //                Console.WriteLine("Life policy must include Date of Birth.");
        //                return;
        //            }
        //            if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
        //            {
        //                Console.WriteLine("Centenarians are not eligible for coverage.");
        //                return;
        //            }
        //            if (policy.Amount == 0)
        //            {
        //                Console.WriteLine("Life policy must include an Amount.");
        //                return;
        //            }
        //            int age = DateTime.Today.Year - policy.DateOfBirth.Year;
        //            if (policy.DateOfBirth.Month == DateTime.Today.Month &&
        //                (DateTime.Today.Day < policy.DateOfBirth.Day ||
        //                DateTime.Today.Month < policy.DateOfBirth.Month))
        //            {
        //                age--;
        //            }
        //            decimal baseRate = policy.Amount * age / 200;
        //            if (policy.IsSmoker)
        //            {
        //                Rating = baseRate * 2;
        //                break;
        //            }
        //            Rating = baseRate;
        //            break;

        //        default:
        //            Console.WriteLine("Unknown policy type");
        //            break;
        //    }

        //    Console.WriteLine("Rating completed.");
        //}
        #endregion

    }
    public interface IRatingEngine
    {
        OprationResult Rate(Policy policy);
    }
    public class LifePolice : IRatingEngine
    {
        public OprationResult Rate(Policy policy)
        {
            OprationResult op = new OprationResult("Rating LIFE policy...");
            #region [ Validation ]
            Console.WriteLine("Validating policy.");
            if (policy.DateOfBirth == DateTime.MinValue)
            {
                op.Message = "Life policy must include Date of Birth.";
                return op;
            }
            if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
            {
                op.Message = "Centenarians are not eligible for coverage.";
                return op;
            }
            if (policy.Amount == 0)
            {
                op.Message = "Life policy must include an Amount.";
                return op;
            }
            #endregion
            int age = DateTime.Today.Year - policy.DateOfBirth.Year;
            if (policy.DateOfBirth.Month == DateTime.Today.Month &&
                (DateTime.Today.Day < policy.DateOfBirth.Day ||
                DateTime.Today.Month < policy.DateOfBirth.Month))
            {
                age--;
            }
            decimal baseRate = policy.Amount * age / 200;
            if (policy.IsSmoker)
            {
                op.Result = baseRate * 2;
                op.IsSuccess = true;
                return op;
            }
            op.IsSuccess = true;
            op.Result = baseRate;
            op.Message = "Rating completed.";
            return op;
        }
    }
    public class VehiclePolice : IRatingEngine
    {
        public OprationResult Rate(Policy policy)
        {
            OprationResult op = new OprationResult("Rating Vehicle policy...");
            Console.WriteLine("Validating policy.");
            if (DateTime.Now.Year - policy.Year < 5)
            {
                op.Result = policy.Price * (5m / 100);
            }
            else
            {
                op.Result = policy.Price * (9m / 100);
            }
            op.IsSuccess = true;
            op.Message = "Rating completed.";
            return op;
        }
    }

    public class OprationResult
    {
        public OprationResult(string Operation)
        {
            IsSuccess = false;
            Message = string.Empty;
            Result = null;
            this.Operation = Operation;
        }
        public string Operation { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public object Result{ get; set; }
    }
}
