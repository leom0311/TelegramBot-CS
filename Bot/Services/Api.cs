using Bot.Configs;
using Bot.DTOs;
using Bot.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services
{
    class Api
    {
        static private string API_KEY { get; } = Config.getToken("API_KEY");
        static private string API_URL { get; } = Config.getToken("API_URL");
        static private string ETH_API_URL { get; } = Config.getToken("ETH_API_URL");
        static private string ETH_API_KEY { get; } = Config.getToken("ETH_API_KEY");

        public static CryptoData GetAllCrypto()
        {
            var URL = new UriBuilder(API_URL);
            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            var result = client.DownloadString(URL.ToString());
            return JsonConvert.DeserializeObject<CryptoData>(result);
            //var data = ServiceApi.GetCrypto(API_KEY);
            //return data.Result;
        }

        public static string GetCryptoPriceBySlug(string slug)
        {
            CryptoData obj = GetAllCrypto();
            Console.Write("Requesting...");
            foreach (Crypto item in obj.Data)
            {
                if(item.slug == slug)
                {
                    return item.quote.USD.price.ToString();
                }
            }
            return "Doesn't found";
        }

        public static Balances GetEthBalanceOfAddress(string address)
        {
            var URL = new UriBuilder(ETH_API_URL+$"/api?module=account&action=balance&" +
                $"address={address}&tag=latest&apikey={ETH_API_KEY}");
            var client = new WebClient();
            client.Headers.Add("Accepts", "application/json");
            var result = client.DownloadString(URL.ToString());
            return JsonConvert.DeserializeObject<Balances>(result);
        }

        //public static AddressTracker GetEthTransaction(string address)
        //{
        //    var URL = new UriBuilder(ETH_API_URL + $"/api?module=account&action=txlist" +
        //        $"&address={address}&startblock=0&endblock=99999999&page=10" +
        //        $"&offset=1&sort=asc" +
        //        $"&tag=latest&apikey={ETH_API_KEY}");
        //    var client = new WebClient();
        //    client.Headers.Add("Accepts", "application/json");
        //    var result = client.DownloadString(URL.ToString());
        //    Console.WriteLine(result.ToString());
        //    return JsonConvert.DeserializeObject<AddressTracker>(result);
        //}
    }
}
