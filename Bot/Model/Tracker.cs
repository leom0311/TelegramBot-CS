using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Model
{
    public class Tracker
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public  string Address { get; set; }
        public DateTime UpdateTime  { get; set; }

    }
}
