using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Transaction
    {
        [JsonProperty("Date")]
        public DateTime date;

        [JsonProperty("FromAccount")]
        public string from;

        [JsonProperty("ToAccount")]
        public string to;

        [JsonProperty("Narrative")]
        public string narrative;

        [JsonProperty("Amount")]
        public double amount;
    }
}
