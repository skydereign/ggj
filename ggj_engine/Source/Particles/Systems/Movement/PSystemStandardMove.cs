using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;

namespace ggj_engine.Source.Particles
{
    public class PSystemStandardMove : PSystemMove
    {
        public PSystemStandardMove()
        {
            Emitter e = new CircleEmitter();

            e.pStartColor = new Color(127, 255, 157,255);
            e.pEndColor = new Color(127, 255, 157,255);
            e.pStartColorDeviation = new Color(0, 0, 60, 255);
            e.pEndColorDeviation = new Color(0, 0, 60, 255);
            e.pStartBrightnessDeviationFactor = 1f;
            e.pEndBrightnessDeviationFactor = 1f;
            e.pStartScale = 1.5f;
            e.pEndScale = 4;
            e.pStartScaleDeviation = 0.5f;
            e.pEndScaleDeviation = 1f;
            e.pMinAccel = new Vector2(0, 0);
            e.pMaxAccel = new Vector2(0, 0);
            e.pMinAngle = new Vector2(-1, -1);
            e.pMaxAngle = new Vector2(1, 1);
            e.pMinVel = 3f;
            e.pMaxVel = 10f;
            e.pMinFriction = 0.7f;
            e.pMaxFriction = 0.7f;
            e.pMinLife = 30;
            e.pMaxLife = 50;
            e.pMinSpawn = 1;
            e.pMaxSpawn = 4;
            e.Burst = true;
            ((CircleEmitter)e).Radius = 7;

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
