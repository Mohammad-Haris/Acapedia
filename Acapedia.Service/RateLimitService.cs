using System;
using System.Diagnostics;
using Acapedia.Data.Contracts;

namespace Acapedia.Service
{
    public class RateLimitService : IRateLimit
    {
        private DateTime Time;
        private int CallCount;
        private Stopwatch Watch;

        public RateLimitService ()
        {
            Time = DateTime.Now;
            CallCount = 0;
            Watch = new Stopwatch();
        }

        public bool LimitRate ()
        {
            if (Watch.IsRunning)
            {
                if (Watch.ElapsedMilliseconds < 5000)
                {
                    return true;
                }

                Time = DateTime.Now;
                Watch.Reset();
            }

            CallCount++;
            double Elapsed = Time.Subtract(DateTime.Now).TotalSeconds * -1;
            double Ratio = CallCount / Elapsed;

            if (Ratio > 2)
            {
                Watch.Start();

                CallCount = 0;

                return true;
            }

            return false;
        }
    }
}
