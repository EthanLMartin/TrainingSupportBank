using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace SupportBank
{
    class XMLParser : IParser
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public List<Transaction> ParseFile(string directory)
        {
            logger.Log(LogLevel.Debug, "Reading file: " + directory);
            List<Transaction> transactions = new List<Transaction>();
            XDocument data = new XDocument();

            try
            {
                data = XDocument.Load(directory);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file.");
                logger.Log(LogLevel.Error, e);
                logger.Log(LogLevel.Error, "Error parsing " + directory + " as XML file");
                return transactions;
            }

            IEnumerable<XElement> transactionData = data.Descendants("SupportTransaction");

            foreach (XElement transaction in transactionData)
            {
                try
                {
                    transactions.Add(ParseXElement(transaction));
                } 
                catch (Exception e)
                {
                    logger.Log(LogLevel.Error, "Error in entry of XML file " + directory + "\n Error message: " + e);
                    Console.WriteLine("Error found in XML file, please fix");
                }
            }
            logger.Log(LogLevel.Debug, "File reading finished. Sucessfully read "+ transactions.Count.ToString() + " transactions.");

            return transactions;
        }

        private Transaction ParseXElement(XElement node)
        {
            Transaction transaction = new Transaction();

            transaction.date = ParseXMLDate(node.Attribute("Date").Value);

            XElement parties = node.Descendants("Parties").FirstOrDefault();
            transaction.from = parties.Element("From").Value;
            transaction.to = parties.Element("To").Value;

            transaction.narrative = node.Element("Description").Value;
            transaction.amount = Convert.ToDecimal(node.Element("Value").Value);

            return transaction;
        }

        private DateTime ParseXMLDate(string days)
        {
            return DateTime.FromOADate(Convert.ToInt32(days));
        }
    }
}
