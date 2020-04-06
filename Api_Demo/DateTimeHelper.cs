using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public  class DateTimeHelper
    {
        public static DateTime ConvertFromUTC(DateTime datetime)
        {
            return TimeZoneInfo.ConvertTime(datetime, TimeZoneInfo.Local);
        }

    }
}
