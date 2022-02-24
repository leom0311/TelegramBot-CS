using Bot.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using Bot.ScheduleServices;
using Bot.Commands;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Bot.Configs;

namespace Bot
{
    public class Program
    {
        static private string Token { get; } = Config.getToken("Token");
        static TelegramBotClient Bot = new TelegramBotClient(Token);
        [Obsolete]
        static async Task  Main(string[] args)
        {
            await Schedule.SchedulJob();
            Bot.StartReceiving();
            Bot.OnMessage += Bot_OnMessage;
            Console.ReadLine();
        }
        [Obsolete]
        public  static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            string text = e.Message.Text;
            string keyword = text?.Split(' ')[0];
            switch (keyword)
            {
                case "/price":
                    ReplyKeyboardMarkup rkm = BotOperation.OrderCrypto();
                    Bot.SendTextMessageAsync(
                        e.Message.Chat.Id,
                        "Choose one of these cryptovalues",
                        replyMarkup: rkm);
                    break;
                case "/reminder":
                    var msg = BotOperation.ReminderPrice(e.Message.Chat.Id,text);
                    Bot.SendTextMessageAsync(e.Message.Chat.Id,"Your reminder setted.");
                    break;
                default:
                    Console.WriteLine(e.Message.Chat.Id);
                    string price = BotOperation.SendPrice(text);
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, price);
                    break;
            }
        }
    }
   
}

