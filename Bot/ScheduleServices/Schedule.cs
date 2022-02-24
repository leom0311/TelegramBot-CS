using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
namespace Bot.ScheduleServices
{
    public class Schedule
    {
        public static async Task SchedulJob()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<JobApi>()
               .WithIdentity("jobName", "jobGroup")
               .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("triggerName", " triggerGorup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
