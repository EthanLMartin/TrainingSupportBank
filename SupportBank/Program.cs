using NLog.Targets;
using NLog.Config;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SupportBank
{
    class Program
    {
        private static ConsoleInterface promptHandler = new ConsoleInterface();

        static void Main(string[] args)
        {
            LoggerSetup();

            TransactionsRepository transactionRepository = new TransactionsRepository();

            while (true)
            {
                UserChoiceMainMenu selection = promptHandler.PromptMenuSelection();

                switch (selection)
                {
                    case UserChoiceMainMenu.All:
                        promptHandler.DisplayAllOwed(transactionRepository.GetAccounts());
                        break;

                    case UserChoiceMainMenu.Account:
                        string name = promptHandler.AskForInput("Enter an account name to list all transactions");
                        List<Transaction> filteredTransactions = transactionRepository.GetTransactions(name);
                        promptHandler.DisplayAllTransactions(filteredTransactions);
                        break;

                    case UserChoiceMainMenu.Import:
                        List<Transaction> attemptedImport = ImportFile();
                        if (attemptedImport != null)
                        {
                            transactionRepository.AddTransactions(attemptedImport);
                        }
                        break;

                    case UserChoiceMainMenu.Export:
                        string directory = @"C:\Work\Training\SupportBank\Files\Export";
                        (new JSONExporter()).ExportTransactions(transactionRepository.GetTransactions(), directory);
                        break;
                    case UserChoiceMainMenu.Exit:
                        return;
                }

                promptHandler.WriteSeparator();

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
        
        static List<Transaction> ImportFile()
        {
            string directory = promptHandler.AskForInput("Please input a file for import");

            if (File.Exists(directory))
            {
                ParserSelector parserSelector = new ParserSelector();
                IParser parser = parserSelector.SelectParser(directory);
                if (parser != null)
                {
                    return parser.ParseFile(directory);
                }
            }
            else
            {
                Console.WriteLine("File not found, retuning to menu");
            }

            return null;
        }
    }
}
