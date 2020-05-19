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

        public static void Error(string msg, Exception ex = null)
        {
            log.Error(ex,msg);
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

        public  static void Tract(Exception ex,string msg)
        {
            log.Trace(ex,msg);
        }

    }
}
