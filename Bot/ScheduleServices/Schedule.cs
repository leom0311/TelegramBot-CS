using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Telegram.Bot;

namespace Bot.ScheduleServices
{
    public class Schedule
    {
        public static async Task SchedulJob(TelegramBotClient bot)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();
            scheduler.Context.Put("bot",bot);
            IJobDetail job = JobBuilder.Create<JobApi>()
               .WithIdentity("jobName", "jobGroup")
               .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("triggerName", " triggerGorup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(10)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
