using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    interface IExporter
    {
        void ExportTransactions(List<Transaction> transactions, string directory);
    }
}
