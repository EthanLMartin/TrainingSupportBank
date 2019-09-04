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
            string directory = @"\Work\Training\SupportBank\Files\Transactions2014.csv";
            AccountsHandler accounts = new AccountsHandler(directory);

            accounts.ListAllOwed();
            Console.ReadLine();
        }
    }
}
