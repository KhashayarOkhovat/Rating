using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingEngineSample
{
    public class Policy
    {
        public PolicyType Type { get; set; }

        #region Life Insurance
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsSmoker { get; set; }
        public decimal Amount { get; set; }
        #endregion

        #region Auto
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Miles { get; set; }
        public decimal Price { get; set; }
        #endregion

    }

    public enum PolicyType
    {
        Life = 0,
        Vehicle = 1,
        Accident = 2
    }
}
