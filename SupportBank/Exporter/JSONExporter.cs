using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class JSONExporter : IExporter
    {
        public void ExportTransactions(List<Transaction> transactions, string basePath)
        {
            string exportedFile = JsonConvert.SerializeObject(transactions, Formatting.Indented);

            int version = 0;
            string fileDir = basePath + @"\Transactions" + version.ToString().PadLeft(4, '0') + ".json";
            while (File.Exists(fileDir))
            {
                version += 1;
                fileDir = basePath + @"\Transactions" + version.ToString().PadLeft(4, '0') + ".json";
            }

            File.Create(fileDir).Close();
            File.WriteAllText(fileDir, exportedFile);
        }
    }
}
