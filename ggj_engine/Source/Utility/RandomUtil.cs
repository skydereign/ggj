using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Utility
{
    public static class RandomUtil
    {
        private static Random random = new Random();

        public static double Next()
        {
            return random.NextDouble();
        }

        public static double Next(double max)
        {
            return random.NextDouble() * max;
        }

        public static double Next(double min, double max)
        {
            return (random.NextDouble() * (max - min) + min);
        }
    }
}
