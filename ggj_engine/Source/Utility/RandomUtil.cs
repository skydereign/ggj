using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

        public static Vector2 Next(Vector2 min, Vector2 max)
        {
            Vector2 v;
            v.X = (float)(random.NextDouble() * (max.X - min.X) + min.X);
            v.Y = (float)(random.NextDouble() * (max.Y - min.Y) + min.Y);
            return v;
        }

        public static Vector3 Next(Vector3 min, Vector3 max)
        {
            Vector3 v;
            v.X = (float)(random.NextDouble() * (max.X - min.X) + min.X);
            v.Y = (float)(random.NextDouble() * (max.Y - min.Y) + min.Y);
            v.Z = (float)(random.NextDouble() * (max.Z - min.Z) + min.Z);
            return v;
        }

        public static Vector4 Next(Vector4 min, Vector4 max)
        {
            Vector4 v;
            v.X = (float)(random.NextDouble() * (max.X - min.X) + min.X);
            v.Y = (float)(random.NextDouble() * (max.Y - min.Y) + min.Y);
            v.Z = (float)(random.NextDouble() * (max.Z - min.Z) + min.Z);
            v.W = (float)(random.NextDouble() * (max.W - min.W) + min.W);
            return v;
        }
    }
}
