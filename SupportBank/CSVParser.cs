using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class CSVParser
    {
        public List<Transaction> ParseFile(string directory)
        {
            List<string> lines = System.IO.File.ReadAllLines(directory).ToList();
            List<Transaction> transactions = new List<Transaction>();

            lines.RemoveAt(0);

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(',');
                Transaction transaction = CreateTransation(splitLine);
                transactions.Add(transaction);
            }

            return transactions;
        }

        private Transaction CreateTransation(string[] data)
        {
            Transaction transaction = new Transaction();
            // transaction.date = DateTime.Parse(data[0], "dd-MM-yy");
            transaction.from = data[1];
            transaction.to = data[2];
            transaction.narrative = data[3];
            transaction.amount = Convert.ToDouble(data[4]);
            return transaction;
        }
    }
}
