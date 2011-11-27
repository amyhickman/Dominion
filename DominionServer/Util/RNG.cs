using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominion.Util
{
    public static class RNG
    {
        private static Random _rand = new Random();

        public static int Next()
        {
            lock (_rand)
            {
                return _rand.Next();
            }
        }

        public static int Next(int low, int max)
        {
            lock (_rand)
            {
                return _rand.Next(low, max);
            }
        }

        public static double NextDouble()
        {
            lock (_rand)
            {
                return _rand.NextDouble();
            }
        }
    }
}