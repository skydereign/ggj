using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;

namespace ggj_engine.Source.Particles
{
    class PointEmitter : Emitter
    {
        public override Particle SpawnParticle()
        {
            Vector2 pPosition = MPSystem.Position + PositionOffset;

            Particle p = new Particle(pPosition, this);

            return p;
        }
    }
}
