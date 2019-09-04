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
            CSVParserClass parserCSV = new CSVParserClass();
            string directory = @"\Work\Training\SupportBank\Files\Transactions2014.csv";

            foreach (List<string> line in parserCSV.ParseFile(directory))
            {
                foreach (string value in line)
                {
                    Console.Write(value + " | ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
