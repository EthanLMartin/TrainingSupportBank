using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class TransactionsRepository
    {
        private List<Transaction> transactions;
        private Dictionary<string, Account> accountNamePairs = new Dictionary<string, Account>();

        public TransactionsRepository(List<Transaction> transactions)
        {
            this.transactions = transactions;
            MakeUniqueAccounts(this.transactions);
        }

        public List<Account> GetUpdatedAccounts()
        {
            List<Account> accounts = new List<Account>();
            foreach (var pair in accountNamePairs)
            {
                Account account = pair.Value;
                string name = pair.Key;

                if (account.Balance == null)
                {
                    List<Transaction> transactions = GetTransactionsOf(name);
                    account.ProcessTransactions(transactions);
                }

                accounts.Add(account);
            }

            return accounts;
        }

        public void ListTransactions(string name)
        {
            ConsoleFormatting formatter = new ConsoleFormatting();
            List<Transaction> transactions = GetTransactionsOf(name);
            formatter.DisplayAll(transactions);
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
                accountNamePairs.Add(name, new Account(name));
            }


        }

        private List<Transaction> GetTransactionsOf(string name)
        {
            return (this.transactions.Where(transaction => (transaction.to == name || transaction.from == name))).ToList();
        }
    }
}
