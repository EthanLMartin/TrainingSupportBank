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
    class JSONParser
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public List<Transaction> ParseFile(string directory)
        {          
            logger.Log(LogLevel.Debug, "Reading file: " + directory);
            string file = File.ReadAllText(directory);
            List<Transaction> transactions = JsonConvert.DeserializeObject<List<Transaction>>(file);
            logger.Log(LogLevel.Debug, "Finishing reading file " + directory);
            logger.Log(LogLevel.Debug, transactions.Count.ToString() + " transactions read.");

            return transactions;
        }
    }
}
