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
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            LoggerSetup();

            string directory = @"\Work\Training\SupportBank\Files\Transactions2012.xml";
            //string directory = @"\Work\Training\SupportBank\Files\Transactions2013.json";
            //string directory = @"\Work\Training\SupportBank\Files\Transactions2014.csv";
            //string directory = @"\Work\Training\SupportBank\Files\DodgyTransactions2015.csv";

            //CSVParser parserCSV = new CSVParser();
            //List<Transaction> transactions = parserCSV.ParseFile(directory);

            //JSONParser parserJSON = new JSONParser();
            //List<Transaction> transactions = parserJSON.ParseFile(directory);

            XMLParser parserXML = new XMLParser();
            List<Transaction> transactions = parserXML.ParseFile(directory);

            TransactionsRepository transactionRepository = new TransactionsRepository(transactions);

            while (true)
            {
                UserChoice selection = promptHandler.PromptMenuSelection();

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

                    case UserChoice.Import:
                        TransactionsRepository attemptedRepo = ImportFile();
                        if (attemptedRepo != null)
                        {
                            transactionRepository = attemptedRepo;
                        }
                        break;

                    case UserChoice.Exit:
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
        
        static TransactionsRepository ImportFile()
        {
            string directory = promptHandler.AskForInput("Please input a file for import");
            if (File.Exists(directory))
            {
                ParserSelector parserSelector = new ParserSelector();
                IParser parser = parserSelector.SelectParser(directory);
                if (parser == null)
                {
                    Console.WriteLine("Extension not recognised, please try again.");
                    logger.Log(LogLevel.Debug, "Extension type of " + directory + " is not recognised");
                } else
                {
                    return new TransactionsRepository(parser.ParseFile(directory));
                }
            }
            else
            {
                Console.WriteLine("File not found please try again");
            }
            return null;
        }
    }
}
