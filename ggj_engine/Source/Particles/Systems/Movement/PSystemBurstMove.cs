using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;

namespace ggj_engine.Source.Particles
{
    public class PSystemBurstMove : PSystemMove
    {
        public PSystemBurstMove()
        {
            Emitter e = new PointEmitter();

            e.pStartColor = new Color(127, 255, 255,255);
            e.pEndColor = new Color(127, 255, 255,255);
            e.pStartColorDeviation = new Color(0, 0, 0, 255);
            e.pEndColorDeviation = new Color(0, 0, 0, 255);
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
            e.pMinVel = 10f;
            e.pMaxVel = 11f;
            e.pMinFriction = 0.7f;
            e.pMaxFriction = 0.7f;
            e.pMinLife = 15;
            e.pMaxLife = 25;
            e.pMinSpawn = 60;
            e.pMaxSpawn = 60;
            e.Burst = true;

            AddEmitter(e);

            e = new PointEmitter();
            e.pStartColor = new Color(127, 255, 170, 255);
            e.pEndColor = new Color(127, 255, 170, 255);
            e.pStartColorDeviation = new Color(0, 0, 0, 255);
            e.pEndColorDeviation = new Color(0, 0, 0, 255);
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
            e.pMinVel = 2f;
            e.pMaxVel = 4f;
            e.pMinFriction = 0.8f;
            e.pMaxFriction = 0.8f;
            e.pMinLife = 15;
            e.pMaxLife = 25;
            e.pMinSpawn = 20;
            e.pMaxSpawn = 30;
            e.Burst = true;

            AddEmitter(e);
        }

        public override void SetAngle(float angle)
        {
            Angle = angle;

            Vector2 a1 = new Vector2(1, -1);
            Vector2 a2 = new Vector2(1, 1);

            Emitters[0].pMinAngle.X = (float)(a1.X * Math.Cos(Angle) - a1.Y * Math.Sin(Angle));
            Emitters[0].pMinAngle.Y = (float)(a1.X * Math.Sin(Angle) + a1.Y * Math.Cos(Angle));
            Emitters[0].pMaxAngle.X = (float)(a2.X * Math.Cos(Angle) - a2.Y * Math.Sin(Angle));
            Emitters[0].pMaxAngle.Y = (float)(a2.X * Math.Sin(Angle) + a2.Y * Math.Cos(Angle));

            a1 = new Vector2(0.3f, -0.2f);
            a2 = new Vector2(0.3f, 0.2f);

            Emitters[1].pMinAccel.X = (float)(a1.X * Math.Cos(Angle) - a1.Y * Math.Sin(Angle));
            Emitters[1].pMinAccel.Y = (float)(a1.X * Math.Sin(Angle) + a1.Y * Math.Cos(Angle));
            Emitters[1].pMaxAccel.X = (float)(a2.X * Math.Cos(Angle) - a2.Y * Math.Sin(Angle));
            Emitters[1].pMaxAccel.Y = (float)(a2.X * Math.Sin(Angle) + a2.Y * Math.Cos(Angle));
        }
    }
}
