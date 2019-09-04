using NLog.Targets;
using NLog.Config;
using NLog;
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
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;

            //string directory = @"\Work\Training\SupportBank\Files\Transactions2014.csv";
            string directory = @"\Work\Training\SupportBank\Files\DodgyTransactions2015.csv";

            CSVParser parserCSV = new CSVParser();
            List<Transaction> transactions = parserCSV.ParseFile(directory);
            TransactionsRepository accounts = new TransactionsRepository(transactions);
            PromptHandlerClass promptHandler = new PromptHandlerClass();

            while (true)
            {
                UserChoice selection = promptHandler.PromptSelection();

                switch (selection)
                {
                    case UserChoice.All:
                        foreach (Account account in accounts.GetUpdatedAccounts())
                        {
                            Console.WriteLine(account.Name + " is owed: " + account.Balance.ToString());
                        };
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
