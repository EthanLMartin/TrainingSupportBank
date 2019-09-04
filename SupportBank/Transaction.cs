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

            result += this.date.ToString();
            result += gap;
            result += this.from;
            result += gap;
            result += this.to;
            result += gap;
            result += narrative;
            result += gap;
            result += amount.ToString();

            return result;
        }
    }
}
