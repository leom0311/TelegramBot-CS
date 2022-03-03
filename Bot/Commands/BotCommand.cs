using Bot.DTOs;
using Bot.Services;
using System;

namespace Bot.Commands
{
    class BotCommand
    {
        public static CryptoData AllCrypto()
        {
            CryptoData cryptoData = Api.GetAllCrypto();
            return cryptoData;
        }
        public  static string GetPrice(string slug)
        {
            string price = Api.GetCryptoPriceBySlug(slug);
            string result = price+" USD";
            return result;
        }
        public static bool Reminder(string slug,decimal price)
        {
            string prices = Api.GetCryptoPriceBySlug(slug);
            if(Convert.ToDecimal(prices) >= price)
            {
                return true;
            }
            return false;
        }
    }
}
