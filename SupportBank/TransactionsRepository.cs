using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class TransactionsRepository
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private List<Transaction> transactions;
        private Dictionary<string, Account> accountNamePairs = new Dictionary<string, Account>();

        public TransactionsRepository(List<Transaction> transactions)
        {
            this.transactions = transactions;
            MakeUniqueAccounts(this.transactions);
        }

        public List<Account> GetUpdatedAccounts()
        {
            logger.Log(LogLevel.Debug, "Updating accounts");
            List<Account> accounts = new List<Account>();
            foreach (var pair in accountNamePairs)
            {
                Account account = pair.Value;
                string name = pair.Key;

                if (account.Balance == null)
                {
                    List<Transaction> transactions = GetTransactions(name);
                    account.ProcessTransactions(transactions);
                }

                accounts.Add(account);
            }

            return accounts;
        }

        public void ListTransactions(string name)
        {
            
        }

        private void MakeUniqueAccounts(List<Transaction> transactions)
        {
            HashSet<string> uniqueNames = new HashSet<string>();
            foreach (Transaction transaction in transactions)
            {
                uniqueNames.Add(transaction.from);
                uniqueNames.Add(transaction.to);
            }

            logger.Log(LogLevel.Debug, "Creating accounts");
            foreach (string name in uniqueNames)
            {
                accountNamePairs.Add(name, new Account(name));
            }


        }

        public List<Transaction> GetTransactions(string name)
        {
            return (this.transactions.Where(transaction => (transaction.to == name || transaction.from == name))).ToList();
        }
    }
}
