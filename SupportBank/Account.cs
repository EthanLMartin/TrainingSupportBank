using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Account
    {
        public string name;
        private double? balance = null;

        public Account(string name) {
            this.name = name;
        }

        public double? GetOwed(List<Transaction> transactions)
        {
            if (balance == null)
            {
                balance = 0;
                foreach (Transaction transaction in transactions)
                {
                    if (transaction.from == name)
                    {
                        balance -= transaction.amount;
                    }
                    if (transaction.to == name)
                    {
                        balance += transaction.amount;
                    }
                }
            }

            return this.balance;
        }
    }
}
