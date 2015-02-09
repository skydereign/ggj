using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;

namespace ggj_engine.Source.Particles
{
    public class PSystemThrustMove : PSystemMove
    {
        public PSystemThrustMove()
        {
            Emitter e = new CircleEmitter();

            e.pStartColor = new Color(127, 255, 200,255);
            e.pEndColor = new Color(127, 255, 160,255);
            e.pStartColorDeviation = new Color(0, 0, 0, 255);
            e.pEndColorDeviation = new Color(0, 0, 0, 255);
            e.pStartBrightnessDeviationFactor = 1f;
            e.pEndBrightnessDeviationFactor = 1f;
            e.pStartScale = 5f;
            e.pEndScale = 3f;
            e.pStartScaleDeviation = 1f;
            e.pEndScaleDeviation = 0.5f;
            e.pMinAccel = new Vector2(0, 0);
            e.pMaxAccel = new Vector2(0, 0);
            e.pMinAngle = new Vector2(-1, -1);
            e.pMaxAngle = new Vector2(1, 1);
            e.pMinVel = 0f;
            e.pMaxVel = 0f;
            e.pMinFriction = 1f;
            e.pMaxFriction = 1f;
            e.pMinLife = 10;
            e.pMaxLife = 15;
            e.pMinSpawn = 20;
            e.pMaxSpawn = 30;
            e.Burst = true;
            ((CircleEmitter)e).Radius = 12;

            AddEmitter(e);
        }

        public override void SetAngle(float angle)
        {
            Angle = angle;
            Emitters[0].pMinAccel.X = (float)Math.Cos(Angle);
            Emitters[0].pMinAccel.Y = (float)Math.Sin(Angle);
            Emitters[0].pMaxAccel.X = (float)Math.Cos(Angle);
            Emitters[0].pMaxAccel.Y = (float)Math.Sin(Angle);

            Emitters[0].pMinAccel *= 0.5f;
            Emitters[0].pMaxAccel *= 0.5f;
        }
    }
}
