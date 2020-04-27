using LSH.Infrastructure.Dapper;
using LSH.Infrastructure.QuartzNet;
using Quartz;
using System;

namespace Console_Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(TimeSpan.Zero);
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

            //PageInfo pageInfo = new PageInfo();
            //pageInfo.DataCount = 200;
            //pageInfo.PageSize = 0;




            Console.WriteLine(2&3);



         

            
            Console.ReadKey();
        }
    }
}
