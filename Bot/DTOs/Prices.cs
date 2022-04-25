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
        public decimal percent_change_1h { get; set; }
        public decimal percent_change_24h { get; set; }
        public decimal percent_change_7d { get; set; }
        public decimal percent_change_30d { get; set; }
        public decimal percent_change_60d { get; set; }
        public decimal percent_change_90d { get; set; }

    } 
}
