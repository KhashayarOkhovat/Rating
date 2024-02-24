using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{

    public class EmployeeTaskCalculator
    {
        public decimal Calculate(Employee emp)
        {
            if (emp is HourlyEmployee)
            {
                return (decimal)(0.01) * 100000;
            }
            else
            {
                return (decimal)(30 / emp.WorkExperience ) * 100000; ;
            }
        }
    }

    public abstract class Employee
    {
        public int WorkExperience { get; set; }

    }
    public class MonthlyEmployee : Employee
    {

    }
    public class HourlyEmployee : Employee
    {

    }
}
