using Bot.BlockChain;
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
        public static string GetPrice(string slug)
        {
            string price = Api.GetCryptoPriceBySlug(slug);
            string result = price + " USD";
            return result;
        }
        public static bool Reminder(string slug, decimal price)
        {
            string prices = Api.GetCryptoPriceBySlug(slug);
            if (Convert.ToDecimal(prices) >= price)
            {
                return true;
            }
            return false;
        }
        public static bool TrackerAdd(string address)
        {
           string transaction = BlockWeb3.GetAddressTrack(address).Result;
           if(transaction == null)
            {
                return false;
            }
            return true;
        }
        public static string GetBalance(string type, string address)
        {
            string balance;
            switch (type)
            {
                case "ETH":
                    string price = BlockWeb3.GetBalance(address).Result;
                    balance = price.Substring(0, price.Length - 18) + "." +
                        price.Substring(price.Length - 18);
                    break;
                case "BTC":
                    balance = "21312";
                    break;
                default:
                    balance = "Please choose ETH or BTC correctly";
                    break;
            }
            Console.WriteLine(balance.Length);
            return balance;
        }
    }
}
