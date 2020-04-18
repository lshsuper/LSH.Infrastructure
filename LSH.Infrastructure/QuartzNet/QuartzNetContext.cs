using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Simpl;

namespace LSH.Infrastructure.QuartzNet
{
    public class QuartzNetContext
    {

        private DirectSchedulerFactory _factory;
        public QuartzNetContext()
        {
            _factory = DirectSchedulerFactory.Instance;
        }


        public void ScheduleJob(ITrigger trigger, IJobDetail job, string schdulerName)
        {
            var curScheduler = GetScheduler(schdulerName);
            curScheduler.ScheduleJob(job, trigger);
        }


        public IJobDetail CreateJob(QuartzNetJobOption option)
        {

            IJobDetail job = JobBuilder.Create(option.JobType).WithIdentity(option.JobName, option.JobGroup).Build();
            return job;
        }


        public ITrigger CreateTrigger(QuartzNetTriggerOption option)
        {
            ITrigger trigger = TriggerBuilder.Create().WithIdentity(option.TriggerName, option.TriggetGroup).WithCronSchedule(option.Cron).Build();
            return trigger;
        }

        public void CreateScheduler(QuartzNetSchedulerOption option)
        {
            _factory.CreateScheduler(option.SchedulerName, option.SchedulerID, new DefaultThreadPool()
            {

            }, new RAMJobStore()
            {

            });
        }


        public void StartScheduler(string schedulerName)
        {
            var curScheduler = GetScheduler(schedulerName);
            if (!curScheduler.IsStarted)
                curScheduler.Start();
        }

        public void StopScheduler(string schedulerName)
        {
            var curScheduler = GetScheduler(schedulerName);

            if (!curScheduler.IsShutdown)
                curScheduler.Shutdown();
        }
        private IScheduler GetScheduler(string name)
        {
            return _factory.GetScheduler(name).Result;
        }


    }
}
