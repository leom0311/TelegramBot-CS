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

namespace Bot
{
    public class Program
    {
        static private string Token { get; } = "5240186629:AAFD3-VZxhpuFaeJt5kxURliyjuYe00J66w";

        static readonly TelegramBotClient Bot = new TelegramBotClient(Token);
        [Obsolete]
        static void  Main(string[] args)
        {
            Bot.StartReceiving();
            Bot.OnMessage += Bot_OnMessage;
            Console.ReadLine();
        }

        [Obsolete]
        public  static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            string text = e.Message.Text;
            switch (text)
            {
                case "/price":
                    ReplyKeyboardMarkup rkm = BotOperation.OrderCrypto();
                    Bot.SendTextMessageAsync(
                        e.Message.Chat.Id,
                        "Choose one of these cryptovalues",
                        replyMarkup: rkm);
                    break;
                case "/reminder":
                    ////string msg = BotOperation.ReminderPrice(text);
                    ///Eslinde problem burdadir reminderPrice methodu staticdi ama schedule classi dependency enjection olunub ona gore
                    ///de cagira bilmirem. Meqsed de oduki reminderPrice methodunda schedule islesin bitenden sonra hansi ki reminderin
                    ///vaxti catanda messaj gondersin ona gore orda async/await olunub. So bele:)
                    string msg = ""; 
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, msg);
                    break;
                default:
                    string price = BotOperation.SendPrice(text);
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, price);
                    break;
            }
        }
    }
   
}

