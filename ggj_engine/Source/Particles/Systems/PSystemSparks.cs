using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;

namespace ggj_engine.Source.Particles
{
    public class PSystemSparks : PSystem
    {
        public PSystemSparks()
        {
            Emitter e = new PointEmitter();

            e.MPSystem = this;
            e.pStartColor = new Color(220, 220, 0);
            e.pEndColor = new Color(255, 255, 255);
            e.pStartColorDeviation = new Color(0, 0, 0, 0);
            e.pEndColorDeviation = new Color(0, 0, 0, 0);
            e.pMinAccel = new Vector2(0, 0);
            e.pMaxAccel = new Vector2(0, 0);
            e.pMinAngle = new Vector2(-1, -1);
            e.pMaxAngle = new Vector2(1, 1);
            e.pMinVel = 0.5f;
            e.pMaxVel = 3f;
            e.pMinFriction = 1;
            e.pMaxFriction = 1;
            e.pMinLife = 20;
            e.pMaxLife = 50;
            e.pMinSpawn = 100;
            e.pMaxSpawn = 300;
            e.Burst = true;

            AddEmitter(e);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputControl.GetKeyboardKeyPressed(Microsoft.Xna.Framework.Input.Keys.T))
            {
                BurstParticles();
            }

            base.Update(gameTime);
        }
    }
}
