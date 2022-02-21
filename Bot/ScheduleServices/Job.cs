using Bot.Commands;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bot.ScheduleServices
{
    class JobApi:IJob
    {
        public  async Task Execute(IJobExecutionContext context)
        {
            var schedulerContext = context.Scheduler.Context;
            string slug = (string)schedulerContext.Get("slug");
            decimal price = (decimal)schedulerContext.Get("price");
            bool result = BotCommand.Reminder(slug, price);
            if (result)
            {
                await Console.Out.WriteLineAsync("Found");
                await context.Scheduler.Shutdown();
            }
            else
            {
                await Console.Out.WriteLineAsync("Checking...");
            }
        }
    }
}
