using Bot.DTOs;
using Bot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Commands
{
    class BotCommand
    {
        public static CryptoData AllCrypto()
        {
            CryptoData cryptoData = Api.MakeRequestAll();
            return cryptoData;
        }
        public  static string GetPrice(string slug)
        {
            string price = Api.makeRequestById(slug);
            string result = price+" USD";
            return result;
        }
        public static bool Reminder(string slug,decimal price)
        {
            string prices = Api.makeRequestById(slug);
            if(Convert.ToDecimal(prices) == price)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
