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
            TransactionsHolder accounts = new TransactionsHolder(transactions);
            PromptHandlerClass promptHandler = new PromptHandlerClass();

            while (true)
            {
                UserChoice selection = promptHandler.PromptSelection();

                switch (selection)
                {
                    case UserChoice.All:
                        accounts.ListAllOwed();
                        break;
                    case UserChoice.Account:
                        Console.WriteLine("Enter an account name to list all transactions");
                        string name = Console.ReadLine();
                        accounts.ListTransactions(name);
                        break;
                    case UserChoice.Exit:
                        return;
                }

                Console.WriteLine("-----------------");

            }
        }
    }
}
