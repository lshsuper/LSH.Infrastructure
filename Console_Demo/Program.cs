using LSH.Infrastructure;
using LSH.Infrastructure.Dapper;
using LSH.Infrastructure.QuartzNet;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Console_Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(DateTime.Now.ToString("yyMMddHHmmssfff"));
            #region Quartz
            //QuartzNetContext _ctx = new QuartzNetContext();

            ////构建调度器
            //_ctx.CreateScheduler(new QuartzNetSchedulerOption()
            //{
            //    SchedulerID = "lsh",
            //    SchedulerName = "lsh_name"
            //});

            //_ctx.CreateScheduler(new QuartzNetSchedulerOption()
            //{
            //    SchedulerID = "lsh02",
            //    SchedulerName = "lsh02_name"
            //});

            //var sayHelloJob = _ctx.CreateJob(new QuartzNetJobOption()
            //{

            //    JobGroup = "say",
            //    JobName = "say_hello",
            //    JobType = typeof(SayHelloJob)

            //});

            //var sayGoodByeJob = _ctx.CreateJob(new QuartzNetJobOption()
            //{

            //    JobGroup = "say",
            //    JobName = "say_goodbye",
            //    JobType = typeof(SayGoodByeJob)

            //});

            //var sayHelloTrigger = _ctx.CreateTrigger(new QuartzNetTriggerOption()
            //{
            //    Cron = "*/5 * * * * ?",
            //    TriggerName = "5_name",
            //    TriggetGroup = "5_group"
            //});


            //var sayGoodByeTrigger = _ctx.CreateTrigger(new QuartzNetTriggerOption()
            //{
            //    Cron = "*/10 * * * * ?",
            //    TriggerName = "10_name",
            //    TriggetGroup = "10_group"
            //});
            //_ctx.ScheduleJob(sayHelloTrigger, sayHelloJob, "lsh_name");

            //_ctx.ScheduleJob(sayGoodByeTrigger, sayGoodByeJob, "lsh02_name");

            //_ctx.StartScheduler("lsh_name");
            //_ctx.StartScheduler("lsh02_name");


            //var end = DateTime.Now.AddSeconds(20);
            //while (true)
            //{
            //    if (DateTime.Now == end)
            //    {
            //        _ctx.StopScheduler("lsh_name");
            //        Console.WriteLine("停止lsh_name");
            //        break;
            //    }
            //} 
            #endregion




            List<User> u = new List<User>() {
                new User(){Name="lsh",Id=1 },
                 new User(){Name="lsh02",Id=2 },
                  new User(){Name="lsh03",Id=3 },
                   new User(){Name="ls",Id=4 },
                   new User(){Name="ls02",Id=5 },
                   new User(){Name="ls03",Id=6 },
            };
            var express = ExpressionBuilder.Build<User>(new List<ConditionOption>()
            {
               new ConditionOption(){ Left="Id",Right=5, ConditionType=ConditionType.Less,ConnType=ConditionConnType.Or },
              new ConditionOption(){ Left="Name",Right="ls03", ConditionType=ConditionType.Equal },
            });
            var us = u.Where(express);
            foreach (var item in us)
            {
                Console.WriteLine(item.Name);
            }
            Console.ReadKey();
        }



    }

    public class User
    {


        public string Name { get; set; }

        public int Id { get; set; }

    }
}
