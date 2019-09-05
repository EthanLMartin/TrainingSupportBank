using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class ConsoleInterface
    {
        public UserChoiceMainMenu PromptMenuSelection()
        {
            while (true)
            {
                Console.WriteLine("Please select an option: \n " +
                    "1. List all owed \n " +
                    "2. List transactions for a singular name \n " +
                    "3. Import a file \n " +
                    "4. Export transactions as JSON \n " +
                    "Q. Exit the application");
                WriteSeparator();

                string input = Console.ReadLine();
                WriteSeparator();
                if (input == "1")
                {
                    return UserChoiceMainMenu.All;
                }
                else if (input == "2")
                {
                    return UserChoiceMainMenu.Account;
                }
                else if (input == "3")
                {
                    return UserChoiceMainMenu.Import;
                }
                else if (input == "4")
                {
                    return UserChoiceMainMenu.Export;
                }
                else if (input == "Q")
                {
                    return UserChoiceMainMenu.Exit;
                }
                else
                {
                    Console.WriteLine("Invalid selection, please try again.");
                }
            }
        }

        public UserSelectFileType PromptFileTypeSelection(string message = "")
        {
            while (true)
            {
                Console.WriteLine(message + "\nPlease select the file type: \n " +
                    "1. JSON \n " +
                    "2. CSV \n " +
                    "3. XML \n " +
                    "Q. Exit Selection and Return to Menu");

                WriteSeparator();

                string input = Console.ReadLine();
                WriteSeparator();
                if (input == "1" || input == "JSON")
                {
                    return UserSelectFileType.JSON;
                }
                else if (input == "2" || input == "CSV")
                {
                    return UserSelectFileType.CSV;
                }
                else if (input == "3" || input == "XML")
                {
                    return UserSelectFileType.XML;
                }
                else if (input == "Q")
                {
                    return UserSelectFileType.Return;
                }
                else
                {
                    Console.WriteLine("Invalid selection, please try again.");
                }
            }
        }

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

        public void DisplayAllTransactions(List<Transaction> transactions)
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
                Console.WriteLine(TransactionString(transaction, fromGap, toGap, narrativeGap));
            }
        }

        public void DisplayAllOwed(List<Account> accounts)
        {
            foreach (Account account in accounts)
            {
                Console.WriteLine(account.Name + " is owed: " + account.Balance.ToString());
            };
        }

        public string AskForInput(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            WriteSeparator();
            return input;
        }

        public void WriteSeparator()
        {
            Console.WriteLine("-----------------");
        }
    }
}
