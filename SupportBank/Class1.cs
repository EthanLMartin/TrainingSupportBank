using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class AccountsHandler
    {
        private List<Transaction> transactions;
        private HashSet<string> accountNames;

        public AccountsHandler(string directory)
        {
            CSVParser parserCSV = new CSVParser();
            this.transactions = parserCSV.ParseFile(directory);
            this.accountNames = UniqueNames(this.transactions);
        }

        public void ListAllOwed()
        {
            foreach(string name in accountNames)
            {
                Console.WriteLine(name + " owes: " + TotalOwed(name).ToString());
            }
        }

        public void ListTransactions(string name)
        {
            
        }
    
        private double TotalOwed(string name)
        {
            List<Transaction> relevantTransactions = getTransactionsOf(name);
            double totalOwed = 0;
            foreach (Transaction transaction in relevantTransactions)
            {
                if (transaction.from == name)
                {
                    totalOwed -= transaction.amount;
                }
                if (transaction.to == name)
                {
                    totalOwed += transaction.amount;
                }
            }
            return totalOwed;
                
        }

        private HashSet<string> UniqueNames(List<Transaction> transactions)
        {
            HashSet<string> uniqueNames = new HashSet<string>();
            foreach (Transaction transaction in transactions)
            {
                uniqueNames.Add(transaction.from);
                uniqueNames.Add(transaction.to);
            }
            return uniqueNames;
        }

        private List<Transaction> getTransactionsOf(string name)
        {
            return (this.transactions.Where(transaction => (transaction.to == name || transaction.from == name))).ToList();
        }
    }
}
