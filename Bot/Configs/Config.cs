using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Configs
{
    public class Config
    {
        public static string getToken(string type)
        {
            var builder = new ConfigurationBuilder()
                        .AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();
            string Token = config[$"Config:{type}"].ToString();
            return Token;
        }       
    }

}
