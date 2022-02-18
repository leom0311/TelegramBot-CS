using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot
{
        public class mp
    {
        public decimal price { get; set; }
    }
    public class USDs
        {
            public mp USD { get; set; } 
        }
        public class Crypto
        {
            public int id { get; set; }
            public string name { get; set; }
            public DateTime last_updated { get; set; }
            public USDs quote { get; set; }
        }

        public class CryptoData
        {
            public List<Crypto> Data { get; set; }
        }
}

