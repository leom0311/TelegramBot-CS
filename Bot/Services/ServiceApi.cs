using Bot.Configs;
using Bot.DTOs;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services
{
    public static class ServiceApi
    {
        public static async Task<CryptoData> GetCrypto(string token)
        {
            var http = RestService.For<ICryptoClient>("https://pro-api.coinmarketcap.com");
            var response = await http.GetCrypto(token);
            JsonConvert.DeserializeObject<CryptoData>(response.ToString());
            return response;
        }
    }
}
