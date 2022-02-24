using Bot.DTOs;
using Bot.Services;
using System;

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
            if(Convert.ToDecimal(prices.Split('.')[0]) > price)
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
