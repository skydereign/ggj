using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;

namespace ggj_engine.Source.Particles
{
    class CircleEmitter : Emitter
    {
        public float Radius;

        public override Particle SpawnParticle()
        {
            Vector2 pPosition = Vector2.Normalize(RandomUtil.Next(new Vector2(-1, -1), new Vector2(1, 1))) * (float)RandomUtil.Next(Radius);
            pPosition = MPSystem.Position + PositionOffset + pPosition;

            Particle p = new Particle(pPosition, this);

            
            return p;
        }
    }
}
