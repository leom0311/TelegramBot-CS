using Bot.Configs;
using Bot.DTOs;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bot.Services
{
    [Headers("Accepts: application/json")]
    public interface ICryptoClient
    {
        [Get("/v1/cryptocurrency/listings/latest")]
        Task<CryptoData> GetCrypto([Header("X-CMC_PRO_API_KEY")] string token);
    }
}
