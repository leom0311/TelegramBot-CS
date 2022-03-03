using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Model
{
    public class Reminder
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string CryptoName { get; set; }
        public decimal ExceptionPrice { get; set; }
        public bool CompliteStatus { get; set; }
    }
}
