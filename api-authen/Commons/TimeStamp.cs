using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Authen.Api.Commons
{
    public class TimeStamp
    {
        public static DateTime? SecondsToDateTime(double? unixTimeStamp)
        {
            try
            {
                if (unixTimeStamp == null) return null;
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds(unixTimeStamp.Value).ToLocalTime();
                return dateTime;
            }
            catch
            {
                return null;
            }
            
        }
        public static DateTime? MillisecondsToDateTime(double? unixTimeStamp)
        {
            try
            {
                if(unixTimeStamp == null) return null;
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddMilliseconds(unixTimeStamp.Value).ToLocalTime();
                return dateTime;
            }
            catch
            {
                return null;
            }

        }
        public static double? DateToMilliseconds(DateTime? dateTime)
        {
            if (dateTime == null) return null;
            var baseDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var milliseconds = dateTime.Value.Subtract(baseDate).TotalMilliseconds;
            return milliseconds;
        }
    }
}
