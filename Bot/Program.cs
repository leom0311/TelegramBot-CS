using System;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Bot.ScheduleServices;
using Bot.Commands;
using System.Threading.Tasks;
using Bot.Configs;

namespace Bot
{
    public class Program
    {
        static private string Token { get; } = Config.getToken("Token");
        static TelegramBotClient Bot = new TelegramBotClient(Token);
       
        [Obsolete]
        static async Task Main(string[] args)
        {
            Bot.StartReceiving();
            await Schedule.SchedulJob(Bot);
            Bot.OnMessage += Bot_OnMessage;
            Console.ReadLine();
        }
        
        [Obsolete]
        public static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            string text = e.Message.Text;
            string keyword = text?.Split(' ')[0];
            switch (keyword)
            {
                case "/start":
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, "Welcome to our crypto reminder bot \r\n" +
                        "Our services: \r\n" + "/price - list of price \r\n" + "/[cryptoname] - price of cryptovalue \r\n" +
                        "/reminder [cryptoname] [expected price] - remind when crypto value will be higher than expected price \r\n");
                    break;
                
                case "/price":
                    ReplyKeyboardMarkup rkm = BotOperation.OrderCrypto();
                    Bot.SendTextMessageAsync(
                        e.Message.Chat.Id,
                        "Choose one of these cryptovalues",
                        replyMarkup: rkm);
                    break;
                
                case "/reminder":
                    var msg = BotOperation.ReminderPrice(e.Message.Chat.Id, text);
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, msg.Result.ToString());
                    break;
                
                default:
                    string price = BotOperation.SendPrice(text);
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, price);
                    break;
            }
        }
    }

}

