using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class TransactionsHolder
    {
        private List<Transaction> transactions;
        private Dictionary<string, Account> accounts = new Dictionary<string, Account>();

        public TransactionsHolder(List<Transaction> transactions)
        {
            this.transactions = transactions;
            MakeUniqueAccounts(this.transactions);
        }

        public void ListAllOwed()
        {
            foreach(var pair in this.accounts)
            {
                string name = pair.Key;
                Account account = pair.Value;
                List<Transaction> transactions = getTransactionsOf(name);

                Console.WriteLine(name + " owes: " + account.GetOwed(transactions).ToString());
            }
        }

        public void ListTransactions(string name)
        {
            List<Transaction> transactions = getTransactionsOf(name);
            Console.WriteLine("Date       | From | To | Narrative | Amount");
            foreach (var transaction in transactions)
            {
                Console.WriteLine(transaction.ToString());
            }
        }

        private void MakeUniqueAccounts(List<Transaction> transactions)
        {
            HashSet<string> uniqueNames = new HashSet<string>();
            foreach (Transaction transaction in transactions)
            {
                uniqueNames.Add(transaction.from);
                uniqueNames.Add(transaction.to);
            }

            foreach (string name in uniqueNames)
            {
                accounts.Add(name, new Account(name));
            }


        }

        private List<Transaction> getTransactionsOf(string name)
        {
            return (this.transactions.Where(transaction => (transaction.to == name || transaction.from == name))).ToList();
        }
    }
}
