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

        public static float Direction(Vector2 PointA, Vector2 PointB)
        {
            return (float)Math.Atan2(PointA.Y - PointB.Y, PointB.X - PointA.X);
        }

        public static float DegToRad(float angle)
        {
            return (float)(Math.PI*angle/180f);
        }

        public static float RadToDeg(float angle)
        {
            return (float)(angle * 180f / Math.PI);
        }
    }
}
