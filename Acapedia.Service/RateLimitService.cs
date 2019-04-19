using System;
using Acapedia.Data.Contracts;

namespace Acapedia.Service
{
    public class RateLimitService : IRateLimit
    {
        private DateTime Time;
        private int CallCount;
        private int Delay;

        public RateLimitService ()
        {
            Time = DateTime.Now;
            CallCount = 0;
            Delay = 0;
        }

        public bool LimitRate ()
        {
            if (Delay > 0)
            {
                Delay--;
                Time = DateTime.Now;
                return true;
            }

            CallCount++;
            double Elapsed = Time.Subtract(DateTime.Now).TotalSeconds * -1;
            double Ratio = CallCount / Elapsed;

            if (Ratio > 5)
            {
                Delay = 50;

                Time = DateTime.Now;

                CallCount = 0;

                return true;
            }

            return false;
        }
    }
}
