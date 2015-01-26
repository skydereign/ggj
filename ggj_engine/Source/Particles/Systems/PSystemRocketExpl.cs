using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;

namespace ggj_engine.Source.Particles
{
    public class PSystemRocketExpl : PSystem
    {
        int ticker = 25;
        int ticker2;

        public PSystemRocketExpl()
        {
            Emitter e = new CircleEmitter();

            e.MPSystem = this;
            e.pStartMinColor = new Color(255, 180, 60);
            e.pStartMaxColor = new Color(255, 180, 60);
            e.pEndMinColor = new Color(12, 12, 12);
            e.pEndMaxColor = new Color(32, 32, 32);
            e.pMinAccel = new Vector2(0, 0);
            e.pMaxAccel = new Vector2(0, 0);
            e.pMinAngle = new Vector2(-1, -1);
            e.pMaxAngle = new Vector2(1, 1);
            e.pMinVel = 3f;
            e.pMaxVel = 6f;
            e.pMinFriction = 0.7f;
            e.pMaxFriction = 0.7f;
            e.pMinLife = 30;
            e.pMaxLife = 40;
            e.pMinSpawn = 100;
            e.pMaxSpawn = 100;
            e.Burst = true;
            ((CircleEmitter)e).Radius = 7;

            AddEmitter(e);
        }

        public override void Update(GameTime gameTime)
        {
            ticker--;
            if (ticker <=0)
            {
                Kill(60);
            }

            if (ticker2 < 7)
            {
                emitters[0].BurstParticles();
                emitters[0].PositionOffset = RandomUtil.Next(new Vector2(-45, -45), new Vector2(45, 45));
                emitters[0].BurstParticles();
                emitters[0].PositionOffset = RandomUtil.Next(new Vector2(-45, -45), new Vector2(45, 45));
            }
            else
            {
                emitters[0].BurstParticles();
                emitters[0].PositionOffset = RandomUtil.Next(new Vector2(-45, -45), new Vector2(45, 45));
            }
            base.Update(gameTime);
        }
    }
}
