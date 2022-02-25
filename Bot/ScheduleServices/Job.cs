using Bot.Commands;
using Bot.Configs;
using Bot.Data;
using Bot.Model;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Bot.ScheduleServices
{
    class JobApi:IJob
    {
        [Obsolete]
        public  async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("started");
            var schedulerContext = context.Scheduler.Context;
            var bot = (TelegramBotClient)schedulerContext.Get("bot");
            using (SqliteDbContext db = new SqliteDbContext())
            {
                List<Reminder> reminder = await db.Reminders.ToListAsync();
                foreach (Reminder reminderItem in reminder)
                {
                    if (reminderItem.CompliteStatus == false)
                    {
                        bool result = Commands.BotCommand.Reminder(reminderItem.CryptoName, reminderItem.ExceptionPrice);
                        if (result == true)
                        {
                            await Console.Out.WriteLineAsync("Finded");
                            reminderItem.CompliteStatus = true;
                            await db.SaveChangesAsync();
                            Message message = await bot.SendTextMessageAsync(reminderItem.UserId, $"Congrultalation " +
                                $"Your {reminderItem.CryptoName} is higher than {reminderItem.ExceptionPrice}");
                        }
                        else
                        {
                            await Console.Out.WriteLineAsync("Checking");
                        }
                    }
                    else
                    {
                        db.Remove(reminderItem);
                        await db.SaveChangesAsync();
                    }
                }

            }
        }

       
    }
}
