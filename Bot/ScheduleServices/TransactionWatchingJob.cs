using Bot.BlockChain;
using Bot.Commands;
using Bot.Data;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.ScheduleServices
{
    public class TransactionWatchingJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var schedulerContext = context.Scheduler.Context;
            var bot = (TelegramBotClient)schedulerContext.Get("bot");
            using (SqliteDbContext db = new SqliteDbContext())
            {
                var obj = db.Trackers.ToList();
                foreach(var item in obj)
                {
                    var reuslt = Commands.BotCommand.TrackerAdd(item.Address);
                    if (reuslt.Status)
                    {
                        Message message = await bot.SendTextMessageAsync(item.UserId,reuslt.Msg);
                    }
                }
            }
        }
    }
}
