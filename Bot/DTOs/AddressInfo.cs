using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.DTOs
{
    public class AddressInfo
    {
        public string to { get; set; }
        public string from { get; set; }
        public double gas { get; set; }
        public double value { get; set; }
        public string timeStamp { get; set; }

    }
}
