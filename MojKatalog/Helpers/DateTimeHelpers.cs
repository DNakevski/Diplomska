using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Helpers
{
    public static class DateTimeHelpers
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long GetTimestampSeconds(this DateTime date)
        {
            TimeSpan elapsedTime = date - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }

    }
}