using Bot.Configs;
using Bot.DTOs;
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
    }
}
