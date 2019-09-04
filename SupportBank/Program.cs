using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = @"\Work\Training\SupportBank\Files\Transactions2014.csv";
            CSVParser parserCSV = new CSVParser();
            List<Transaction> transactions = parserCSV.ParseFile(directory);

            AccountsHolder accounts = new AccountsHolder(transactions);
            accounts.ListTransactions("Todd");
            Console.ReadLine();
        }
    }
}
