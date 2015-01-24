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
            Particle p = new Particle();
            p.PStartColor = new Color(RandomUtil.Next(pStartMinColor.ToVector4(),pStartMaxColor.ToVector4()));
            p.PEndColor = new Color(RandomUtil.Next(pEndMinColor.ToVector4(),pEndMaxColor.ToVector4()));
            p.PVel = Vector2.Normalize(RandomUtil.Next(pMinAngle, pMaxAngle)) *  (float)RandomUtil.Next(pMinVel, pMaxVel);
            p.PAccel = RandomUtil.Next(pMinAccel,pMaxAccel);
            p.PFriction = (float)RandomUtil.Next(pMinFriction,pMaxFriction);
            p.PKillLife = (float)RandomUtil.Next(pMinLife, pMaxLife);
            p.PUpdateFreq = pUpdateFreq;

            p.Position = MPSystem.Position;

            return p;
        }
    }
}
