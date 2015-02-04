using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;

namespace ggj_engine.Source.Particles
{
    public class PSystemRocketSmoke : PSystem
    {
        public float Angle;


        public PSystemRocketSmoke()
        {
            Emitter e = new CircleEmitter();

            e.pStartColor = new Color(110, 110, 110);
            e.pEndColor = new Color(180, 180, 180);
            e.pStartColorDeviation = new Color(0, 0, 0, 0);
            e.pEndColorDeviation = new Color(0, 0, 0, 0);
            e.pStartBrightnessDeviationFactor = 0.1f;
            e.pEndBrightnessDeviationFactor = 0.3f;
            e.pStartScale = 4;
            e.pEndScale = 4;
            e.pMinAccel = new Vector2(0, 0);
            e.pMaxAccel = new Vector2(0, 0);
            e.pMinAngle = new Vector2(-1, -1);
            e.pMaxAngle = new Vector2(1, 1);
            e.pMinVel = -1f;
            e.pMaxVel = -3f;
            e.pMinFriction = 0.9f;
            e.pMaxFriction = 0.9f;
            e.pMinLife = 20;
            e.pMaxLife = 50;
            e.pMinSpawn = 4;
            e.pMaxSpawn = 10;
            e.Burst = false;
            ((CircleEmitter)e).Radius = 4;

            AddEmitter(e);
        }

        public override void Update(GameTime gameTime)
        {
            Emitters[0].pMinAngle.X = (float)Math.Cos(Angle);
            Emitters[0].pMinAngle.Y = (float)Math.Sin(Angle);
            Emitters[0].pMaxAngle.X = (float)Math.Cos(Angle);
            Emitters[0].pMaxAngle.Y = (float)Math.Sin(Angle);

            base.Update(gameTime);
        }
    }
}