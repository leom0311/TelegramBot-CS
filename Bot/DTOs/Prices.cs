using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.DTOs
{
    public class PriceInfo
    {
        public decimal price { get; set; }
        public decimal volume_24h { get; set; }
        public decimal volume_change_24h { get; set; }
        public decimal percent_change_1h { get; set; }
        public decimal market_cap { get; set; }
    } 
}
