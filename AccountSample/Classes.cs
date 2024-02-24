using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountSample
{
    public class BankAccount
    {
        public int Number { get; set; }
        public int Balance { get; set; }

        public virtual void Withdraw(int amount)
        {
            if (amount > Balance)
            {
                throw new Exception("Amount can not be grater than account balance !!!");
            }
            Balance -= amount;
        }
    }

    public class DebitableBankAccount : BankAccount
    {
        public override void Withdraw(int amount)
        {
            Balance -= amount;
        }
    }

    public class MinimumBalanceBankAccount : BankAccount
    {
        public int BalanceLimitation { get; set; }
        public override void Withdraw(int amount)
        {
            if (amount > Balance - BalanceLimitation)
            {
                throw new Exception("Account balance can not be less than BalanceLimitation");
            }
            Balance -= amount;
        }
    }
}
