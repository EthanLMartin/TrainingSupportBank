using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class JSONParser : Parser
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public override List<Transaction> ParseFile(string directory)
        {          
            logger.Log(LogLevel.Debug, "Reading file: " + directory);
            string file = File.ReadAllText(directory);
            List<Transaction> transactions = new List<Transaction>();
            try
            {
                transactions = JsonConvert.DeserializeObject<List<Transaction>>(file);
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, "Failed to read file " + directory + "\n Error message: " + e);
                Console.WriteLine("Failed to read transactions, please fix before continuing");
            }

            logger.Log(LogLevel.Debug, "Finishing reading file " + directory);
            logger.Log(LogLevel.Debug, transactions.Count.ToString() + " transactions read.");

            return transactions;
        }
    }
}
