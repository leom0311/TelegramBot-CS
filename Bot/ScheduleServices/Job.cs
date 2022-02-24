using Bot.Commands;
using Bot.Configs;
using Bot.Data;
using Bot.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Bot.ScheduleServices
{
    class JobApi:IJob
    {
        //static private string Token { get; } = Config.getToken("Token");
        //static TelegramBotClient Bot = new TelegramBotClient(Token);

        [Obsolete]
        public  async Task Execute(IJobExecutionContext context)
        {
            using (SqliteDbContext db = new SqliteDbContext())
            {

                List<Reminder> reminder = db.Reminders.ToList();
                foreach (Reminder reminderItem in reminder)
                {
                    if (reminderItem.CompliteStatus == false)
                    {
                        bool result = BotCommand.Reminder(reminderItem.CryptoName, reminderItem.ExceptionPrice);
                        if (result == true)
                        {
                            await Console.Out.WriteLineAsync("Finded");
                            //Bot.StartReceiving();
                            //Bot.OnMessage += Bot_OnMessage;
                        }
                        else
                        {
                            await Console.Out.WriteLineAsync("Checking");
                        }
                    }
                    //else
                    //{
                    //    await Console.Out.WriteLineAsync("Deleting");
                    //    db.Remove(reminderItem);
                    //    await db.SaveChangesAsync();
                    //}
                }

            }
        }

        //[Obsolete]
        //private void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        //{
        //    Bot.SendTextMessageAsync(e.Message.Chat.Id, "hello");
        //}
    }
}
