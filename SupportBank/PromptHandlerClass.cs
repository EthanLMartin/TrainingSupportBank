using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class PromptHandlerClass
    {
        public UserChoice PromptSelection()
        {
            while (true)
            {
                Console.WriteLine("Type \"1\" to list all owed, \"2\" for the transactions for a singular name, \"Q\" to exit");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    return UserChoice.All;
                }
                else if (input == "2")
                {
                    return UserChoice.Account;
                }
                else if (input == "Q")
                {
                    return UserChoice.Exit;
                }
                else
                {
                    Console.WriteLine("Invalid selection, please try again.");
                }
            }
        }
    }
}
