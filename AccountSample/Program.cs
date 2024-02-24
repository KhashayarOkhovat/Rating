using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountSample
{
    class Program
    {
        static List<BankAccount> bankAccounts = new List<BankAccount>();
        static void Main(string[] args)
        {
            bankAccounts.Add(new DebitableBankAccount { Number = 100, Balance = 5000 });
            bankAccounts.Add(new BankAccount { Number = 104, Balance = 1000 });
            bankAccounts.Add(new BankAccount { Number = 106, Balance = 500 });
            bankAccounts.Add(new MinimumBalanceBankAccount { Number = 101, Balance = 10000, BalanceLimitation = 9000 });

            foreach (var item in bankAccounts)
            {
                Console.WriteLine($"Number = {item.Number} - Balance ={item.Balance}");
            }

            var payaFile = new Dictionary<int, int>();
            payaFile.Add(100, 1000);
            payaFile.Add(101, 1500);
            payaFile.Add(104, 500);
            payaFile.Add(106, 100);

            ProccessPayaFile(payaFile);

            Console.WriteLine($"--------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var item in bankAccounts)
            {
                Console.WriteLine($"Number = {item.Number} - Balance ={item.Balance}");
            }

            Console.ReadKey();
        }


        static void ProccessPayaFile(Dictionary<int, int> paya)
        {
            foreach (var item in paya)
            {

                var account = bankAccounts.FirstOrDefault(p => p.Number == item.Key);
                if (account is MinimumBalanceBankAccount) continue;
                account.Withdraw(item.Value);
            }
        }


    }



}
