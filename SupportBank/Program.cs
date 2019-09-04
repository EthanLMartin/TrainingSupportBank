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
        private static ConsoleInterface promptHandler = new ConsoleInterface();

        static void Main(string[] args)
        {
            LoggerSetup();

            //string directory = @"\Work\Training\SupportBank\Files\Transactions2014.csv";
            string directory = @"\Work\Training\SupportBank\Files\DodgyTransactions2015.csv";

            CSVParser parserCSV = new CSVParser();
            List<Transaction> transactions = parserCSV.ParseFile(directory);
            TransactionsRepository transactionRepository = new TransactionsRepository(transactions);

            while (true)
            {
                UserChoice selection = promptHandler.PromptSelection();

                switch (selection)
                {
                    case UserChoice.All:
                        promptHandler.DisplayAllOwed(transactionRepository.GetUpdatedAccounts());
                        break;

                    case UserChoice.Account:
                        string name = promptHandler.AskForInput("Enter an account name to list all transactions");
                        List<Transaction> filteredTransactions = transactionRepository.GetTransactions(name);
                        promptHandler.DisplayAllTransactions(filteredTransactions);
                        break;

                    case UserChoice.Exit:
                        return;
                }

                Console.WriteLine("-----------------");

            }
        }

        static void LoggerSetup()
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
        }
    }
}
