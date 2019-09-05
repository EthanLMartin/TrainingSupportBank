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

        private List<Transaction> transactions = new List<Transaction>();
        private Dictionary<string, Account> accountNamePairs = new Dictionary<string, Account>();
        
        public void AddTransactions(List<Transaction> transactions)
        {
            this.transactions.AddRange(transactions);
            this.transactions = this.transactions.OrderBy(item => item.date).ToList();
            UpdateUniqueAccounts();
            UpdateAccounts(transactions);
        }

        public void UpdateAccounts(List<Transaction> transactions)
        {
            logger.Log(LogLevel.Debug, "Updating accounts");
            List<Account> accounts = new List<Account>();
            foreach (var pair in accountNamePairs)
            {
                Account account = pair.Value;
                string name = pair.Key;

                List<Transaction> filteredTransactions = GetTransactions(name, transactions);
                account.ProcessTransactions(filteredTransactions);

            }

            logger.Log(LogLevel.Debug, "Successfully updated accounts.");
        }

        public List<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();

            foreach (var pair in accountNamePairs)
            {
                Account account = pair.Value;
                accounts.Add(account);
            }

            return accounts;
        }

        private void UpdateUniqueAccounts()
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
                if (!accountNamePairs.ContainsKey(name))
                {
                    accountNamePairs.Add(name, new Account(name));
                }
            }

            accountNamePairs = accountNamePairs.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

            logger.Log(LogLevel.Debug, "Successfully updated " + uniqueNames.Count.ToString() + " accounts.");
        }

        public List<Transaction> GetTransactions(string name = null, List<Transaction> transactions = null)
        {
            if (name == null)
            {
                return this.transactions;
            }
            if (transactions == null) {
                transactions = this.transactions;
            }
            return (transactions.Where(transaction => (transaction.to == name || transaction.from == name))).ToList();
        }
    }
}
