using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Transaction
    {
        public DateTime date;
        public string from;
        public string to;
        public string narrative;
        public double amount;

        public override string ToString()
        {
            string gap = " | ";
            string result = "";

            result += date.ToString().Split(' ')[0];
            result += gap;
            result += from;
            result += gap;
            result += to;
            result += gap;
            result += narrative;
            result += gap;
            result += amount.ToString();

            return result;
        }
    }
}
