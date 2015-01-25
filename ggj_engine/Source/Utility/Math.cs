using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Utility
{
    static class MathExt
    {
        public static float Truncate(this float value, int digits)
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * value) / mult;
            return (float)result;
        }

        public static float Direction(Vector2 from, Vector2 to)
        {
            //return (float)Math.Atan2(to.Y - from.Y, from.X - to.X);
            return (float)Math.Atan2(from.Y - to.Y, to.X - from.X);
        }
    }
}
