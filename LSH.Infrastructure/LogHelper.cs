using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LSH.Infrastructure
{
  public  class LogHelper
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        public static void Error(object msg, Exception ex = null)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("---日志记录开始----");
            //sb.AppendLine(string.Format("[时间]:{0}",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            //sb.AppendLine(string.Format("[级别]:{0}","Error"));
            //sb.AppendLine(string.Format("[异常及信息]:{0}-{1}",msg,exp.ToString()));
            log.Error(ex,msg.ToString());
        }

        public static void Debug(object msg)
        {
            log.Debug(msg); 
        }

        public static void Info(object msg)
        {

            log.Info(msg);
           
        }


        public static void Warn(object msg)
        {
          
            log.Warn(msg);
        }

    }
}
