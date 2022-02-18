using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot
{
    class Program
    {
        static private string Token { get; } = "5240186629:AAFD3-VZxhpuFaeJt5kxURliyjuYe00J66w";
        static private string API_KEY { get; } = "cb2ac294-d395-4788-8302-2286c00db261";
        static readonly TelegramBotClient Bot = new TelegramBotClient(Token);

        [Obsolete]
        static void  Main(string[] args)
        {
            Bot.StartReceiving();
            Bot.OnMessage += Bot_OnMessage;
            Console.ReadLine();
        }


        [Obsolete]
        private static  void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            string text = e.Message.Text;
            CryptoData crypto = GetExternalResponse();
            if (text.StartsWith("/") && text.Split(new char[] {'/'})[1] == "crypto")
            {

                var rkm = new ReplyKeyboardMarkup();
                var rows = new List<KeyboardButton[]>();
                var cols = new List<KeyboardButton>();
                foreach (Crypto p in crypto.Data)
                {
                    string name = p.name;
                    cols.Add(new KeyboardButton("" + name));
                    rows.Add(cols.ToArray());
                    cols = new List<KeyboardButton>();
                }
                rkm.Keyboard = rows.ToArray();
                Bot.SendTextMessageAsync(
                   e.Message.Chat.Id,
                   "Choose one of these cryptovalues",
                   replyMarkup: rkm);
            }
            else
            {
                foreach (Crypto p in crypto.Data)
                {
                    if (e.Message.Text == p.name)
                    {
                        Bot.SendTextMessageAsync(e.Message.Chat.Id,p.quote.USD.price.ToString()+" USD");
                    }
                    else
                    {
                        Bot.SendTextMessageAsync(e.Message.Chat.Id,"Crytovalue doesn't exist. Please try again!");
                    }
                }
            }
        }
        private static CryptoData GetExternalResponse()
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            var result =  client.DownloadString(URL.ToString());
            return JsonConvert.DeserializeObject<CryptoData>(result);
        }

    }
   
}

