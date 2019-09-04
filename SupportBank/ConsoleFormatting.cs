using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class ConsoleFormatting
    {
        private string TransactionString(Transaction transaction, int fromGap, int toGap, int narrativeGap)
        {
            string gap = " | ";
            string result = "";

            result += transaction.date.ToString().Split(' ')[0];
            result += gap;
            result += transaction.from;
            result += new string(' ', fromGap - transaction.from.Length) + gap;
            result += transaction.to;
            result += new string(' ', toGap - transaction.to.Length) + gap;
            result += transaction.narrative;
            result += new string(' ', narrativeGap - transaction.narrative.Length) + gap;
            result += transaction.amount.ToString();

            return result;
        }

        public void DisplayAll(List<Transaction> transactions)
        {
            if (transactions.Count() == 0) {
                Console.WriteLine("No transactions found for account name");
                return;
            }
            int fromGap = transactions.Select(trans => trans.from.Length).Max();
            int toGap = transactions.Select(trans => trans.to.Length).Max();
            int narrativeGap = Math.Max(transactions.Select(trans => trans.narrative.Length).Max(), 9);

            string firstLine = "Date       | From";
            firstLine += new string(' ', fromGap - 4);
            firstLine += " | To";
            firstLine += new string(' ', toGap - 2);
            firstLine += " | Narrative";
            firstLine += new string(' ', narrativeGap - 9);
            firstLine += " | Amount";
            Console.WriteLine(firstLine);
            foreach (var transaction in transactions)
            {
                Console.WriteLine(TransactionString(transaction ,fromGap, toGap, narrativeGap));
            }
        }
    }
}
