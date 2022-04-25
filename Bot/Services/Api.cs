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
        //static private string ETH_API_URL { get; } = Config.getToken("ETH_API_URL");
        //static private string ETH_API_KEY { get; } = Config.getToken("ETH_API_KEY");

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
        public static List<double> GetChangesBySlug(string slug)
        {
            CryptoData obj = GetAllCrypto();
            Console.Write("Requesting...");
            List<double> result = new List<double>();
            foreach(Crypto item in obj.Data)
            {
                if(item.slug == slug)
                {
                    result.Add(Decimal.ToDouble(item.quote.USD.percent_change_1h));
                    result.Add(Decimal.ToDouble(item.quote.USD.percent_change_24h));
                    result.Add(Decimal.ToDouble(item.quote.USD.percent_change_7d));
                    result.Add(Decimal.ToDouble(item.quote.USD.percent_change_30d));
                    result.Add(Decimal.ToDouble(item.quote.USD.percent_change_60d));
                    result.Add(Decimal.ToDouble(item.quote.USD.percent_change_90d));
                }
            }
            return result;
        }
    }
}
