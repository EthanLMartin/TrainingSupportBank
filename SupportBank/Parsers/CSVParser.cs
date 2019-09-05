using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class CSVParser : IParser
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public List<Transaction> ParseFile(string directory)
        {
            List<string> lines = File.ReadAllLines(directory).ToList();
            List<Transaction> transactions = new List<Transaction>();

            logger.Log(LogLevel.Debug, "Reading file: " + directory);
            logger.Log(LogLevel.Info, "First line of CSV should be titles. If the first line is a transaction it will be ignored");
            lines.RemoveAt(0);

            foreach (string line in lines)
            {
                bool success = TryParseLine(line, out Transaction transaction);
                if (success)
                {
                    transactions.Add(transaction);
                } else
                {
                    logger.Log(LogLevel.Error, "Error found in line " + lines.IndexOf(line).ToString() + " of " + directory);
                }
            }

            logger.Log(LogLevel.Debug, "Finishing reading file " + directory);
            logger.Log(LogLevel.Debug, transactions.Count.ToString() + " transactions read, " + (lines.Count - transactions.Count).ToString() + " lines ignored");

            return transactions;
        }

        private bool TryParseLine(string line, out Transaction transaction)
        {
            transaction = new Transaction();
            string[] data = line.Split(',');

            try
            {
                transaction.date = DateTime.Parse(data[0]);
            }
            catch
            {
                logger.Log(LogLevel.Error, line + " contains invalid date");
                Console.WriteLine("ERROR: Invalid date found in line:\n" + line + "\nPlease use DD/MM/YYYY \n");
                return false;
            }

            transaction.from = data[1];
            transaction.to = data[2];
            transaction.narrative = data[3];

            try
            {
                transaction.amount = Convert.ToDecimal(data[4]);
            }
            catch
            {
                logger.Log(LogLevel.Error, line + " contains invalid amount");
                Console.WriteLine("ERROR: Invalid amount found within line:\n" + line + "\n");
                return false;
            }

            return true;

        }
    }
}
