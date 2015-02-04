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
        int ticker = 15;

        public PSystemRocketExpl()
        {
            Emitter e = new CircleEmitter();

            e.pStartColor = new Color(255, 180, 60);
            e.pEndColor = new Color(12, 12, 12);
            e.pStartColorDeviation = new Color(0, 0, 0, 0);
            e.pEndColorDeviation = new Color(0, 0, 0, 0);
            e.pStartBrightnessDeviationFactor = 0.1f;
            e.pEndBrightnessDeviationFactor = 0.3f;
            e.pStartScale = 4;
            e.pEndScale = 4;
            e.pStartScaleDeviation = 0;
            e.pEndScaleDeviation = 0;
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

            if (ticker < 7)
            {
                Emitters[0].BurstParticles();
                Emitters[0].PositionOffset = RandomUtil.Next(new Vector2(-45, -45), new Vector2(45, 45));
                Emitters[0].BurstParticles();
                Emitters[0].PositionOffset = RandomUtil.Next(new Vector2(-45, -45), new Vector2(45, 45));
            }
            else
            {
                Emitters[0].BurstParticles();
                Emitters[0].PositionOffset = RandomUtil.Next(new Vector2(-45, -45), new Vector2(45, 45));
            }
            base.Update(gameTime);
        }
    }
}
