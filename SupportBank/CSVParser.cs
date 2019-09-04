using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class CSVParser
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public List<Transaction> ParseFile(string directory)
        {
            List<string> lines = System.IO.File.ReadAllLines(directory).ToList();
            List<Transaction> transactions = new List<Transaction>();

            logger.Log(LogLevel.Warn, "First line of CSV should be titles. If the first line is a transaction it will be ignored");
            lines.RemoveAt(0);

            foreach (string line in lines)
            {
                Transaction transaction = new Transaction();
                bool success = LineIntoTransaction(line, transaction);
                if (success)
                {
                    transactions.Add(transaction);
                }
            }

            return transactions;
        }

        private bool LineIntoTransaction(string line, Transaction transaction)
        {
            string[] data = line.Split(',');

            logger.Log(LogLevel.Debug, "At beginning of parsing a transaction line");

            logger.Log(LogLevel.Debug, "Parsing date");
            try
            {
                transaction.date = DateTime.Parse(data[0]);
            }
            catch
            {
                logger.Log(LogLevel.Error, line + " contains invalid date");
                Console.WriteLine("ERROR: Invalid date found in line:");
                Console.WriteLine(line);
                Console.WriteLine("Please use DD/MM/YYYY");
                Console.WriteLine();
                return false;
            }
            logger.Log(LogLevel.Debug, "Parsing from, to and narrative");
            transaction.from = data[1];
            transaction.to = data[2];
            transaction.narrative = data[3];

            logger.Log(LogLevel.Debug, "Parsing amount");
            try
            {
                transaction.amount = Convert.ToDouble(data[4]);
            }
            catch
            {
                logger.Log(LogLevel.Error, line + " contains invalid amount");
                Console.WriteLine("ERROR: Invalid amount found within line:");
                Console.WriteLine(line);
                Console.WriteLine();
                return false;
            }
            logger.Log(LogLevel.Debug, "Reached end of parsing a transaction line");
            return true;

        }
    }
}
