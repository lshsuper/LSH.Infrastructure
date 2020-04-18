using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Console_Demo
{
  public  class SayGoodByeJob:IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("good bye");
            return Task.FromResult(true);
        }
    }
}
