using System;
using System.Collections.Generic;
using System.Text;

namespace LSH.Infrastructure.QuartzNet
{
  public  class QuartzNetJobOption
    {


        public   string JobName { get; set; }

        public   string JobGroup { get; set; }

        public  Type JobType { get; set; }

    }
}
