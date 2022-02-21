using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.DTOs
{
    public class Crypto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string symbol { get; set; }
        public DateTime date_added { get; set; }
        public string num_market_pairs { get; set; }
        //public double max_supply { get; set; }
        //public double total_supply { get; set; }
        //public double cmc_rank { get; set; }
        public DateTime last_updated { get; set; }
        public USDs quote { get; set; }
    }
}
