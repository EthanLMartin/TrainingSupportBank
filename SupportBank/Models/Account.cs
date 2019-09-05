using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Account
    {
        public string Name { get; set; }
        public decimal? Balance { get; private set; } = null;

        public Account(string name) {
            this.Name = name;
        }

        public decimal? ProcessTransactions(List<Transaction> transactions)
        {
            if (Balance == null)
            {
                Balance = 0;
                foreach (Transaction transaction in transactions)
                {
                    if (transaction.from == Name)
                    {
                        Balance -= transaction.amount;
                    }
                    if (transaction.to == Name)
                    {
                        Balance += transaction.amount;
                    }
                }
            }

            return this.Balance;
        }
    }
}
