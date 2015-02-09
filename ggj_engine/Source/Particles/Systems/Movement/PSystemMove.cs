using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Particles
{
    public abstract class PSystemMove : PSystem
    {
        public float Angle;

        public abstract void SetAngle(float angle);
    }
}
