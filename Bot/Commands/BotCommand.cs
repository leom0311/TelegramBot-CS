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

        public static AddressInfo TrackerAdd(string address)
        {
            string transaction = BlockWeb3.GetAddressTrack(address).Result;
            AddressInfo info = new AddressInfo() { Status = true, Msg = transaction };
            if (transaction == "")
            {
                info.Status = false;
            }
            return info;
        }

        public static string GetBalance(string address)
        {
            string price = BlockWeb3.GetBalance(address).Result;
            return address + " balance is " + price + " ETH";
        }
        public static double[] GetChanges(string slug)
        {
            string[] words = slug.Split(' ');
            return Api.GetChangesBySlug(words[1]).ToArray();
        }
    }
}
