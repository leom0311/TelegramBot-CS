using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.DTOs
{
    public class CryptOne
    {
        public CryptTwo Data { get; set; }
    }
    public class CryptTwo
    {
        public Crypto One { get; set; }
    }
}
