using System;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Bot.ScheduleServices;
using Bot.Commands;
using System.Threading.Tasks;
using Bot.Configs;
using System.IO;

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
                        "Our services: \r\n" + "/price - List of price \r\n" + "/[cryptoname] - Price of cryptovalue \r\n" +
                        "/reminder [cryptoname] [expected price] - Remind when crypto value will be higher than expected price \r\n"
                        +"/graph [cryptoname] - Show graph for 1 days to 90 days changes \r\n"
                        +"/balances [wallet address] - Balance of address \r\n"
                        +"/subscribe-to-wallter [wallet address] - When any transaction happend, bot sends notification \r\n");
                    break;
                case "/graph":
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, "Your Graphic is loading...");
                    string image = BotOperation.DrawGraph(text);
                    Console.WriteLine(image);
                    var FileUrl = image;
                    using (var stream = File.Open(FileUrl, FileMode.Open))
                    {
                        Bot.SendPhotoAsync(e.Message.Chat.Id,stream, "The trade represent 1-90 days changes");
                    }
                    break;
                case "/price":
                    ReplyKeyboardMarkup rkm = BotOperation.OrderCrypto();
                    Bot.SendTextMessageAsync(
                        e.Message.Chat.Id,
                        "Choose one of these cryptovalues",
                        replyMarkup: rkm);
                    break;
          
                case "/subscribe-to-wallet":
                    string trackerInfo = BotOperation.TrackAddress(e.Message.Chat.Id,text).Result;
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, trackerInfo);
                    break;

                case "/balances":
                    string addressBalance = BotOperation.BalanceAddress(e.Message.Chat.Id);
                    Bot.SendTextMessageAsync(e.Message.Chat.Id,addressBalance);
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

