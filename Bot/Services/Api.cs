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
        static private string API_KEY { get; } = "cb2ac294-d395-4788-8302-2286c00db261";
        public static CryptoData MakeRequestAll()
        {
            string header = API_KEY;
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", header);
            client.Headers.Add("Accepts", "application/json");
            var result = client.DownloadString(URL.ToString());
            return JsonConvert.DeserializeObject<CryptoData>(result);
        }

        public static string makeRequestById(string slug)
        {
            string header = API_KEY;
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", header);
            client.Headers.Add("Accepts", "application/json");
            var result = client.DownloadString(URL.ToString());
            CryptoData obj =  JsonConvert.DeserializeObject<CryptoData>(result);
            foreach(Crypto item in obj.Data)
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
