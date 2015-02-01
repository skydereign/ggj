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
            Particle p = new Particle();

            //Pick colors with deviations
            p.PStartColor = new Color(pStartColor.ToVector4() + RandomUtil.Next(-pStartColorDeviation.ToVector4(), pStartColorDeviation.ToVector4()));
            p.PEndColor = new Color(pEndColor.ToVector4() + RandomUtil.Next(-pEndColorDeviation.ToVector4(), pEndColorDeviation.ToVector4()));

            //Adjust colors by brightness deviations
            float brightness = (float)RandomUtil.Next(-pStartBrightnessDeviation, pStartBrightnessDeviation);
            brightness += 1;
            //Clamp brightness so colors do not exceed 255
            if (brightness * (float)p.PStartColor.R > 255 && p.PStartColor.R >= p.PStartColor.G && p.PStartColor.R >= p.PStartColor.B)
            {
                brightness = (255 - p.PStartColor.R) / p.PStartColor.R + 1;
            }
            if (brightness * (float)p.PStartColor.G > 255 && p.PStartColor.G >= p.PStartColor.R && p.PStartColor.G >= p.PStartColor.B)
            {
                brightness = (255 - p.PStartColor.G) / p.PStartColor.G + 1;
            }
            if (brightness * (float)p.PStartColor.B > 255 && p.PStartColor.B >= p.PStartColor.G && p.PStartColor.B >= p.PStartColor.R)
            {
                brightness = (255 - p.PStartColor.B) / p.PStartColor.B + 1;
            }
            p.PStartColor = new Color(p.PStartColor.R * brightness, p.PStartColor.G * brightness, p.PStartColor.B * brightness);

            brightness = (float)RandomUtil.Next(-pEndBrightnessDeviation, pEndBrightnessDeviation);
            brightness += 1;
            //Clamp brightness so colors do not exceed 255
            if (brightness * (float)p.PEndColor.R > 255 && p.PEndColor.R >= p.PEndColor.G && p.PEndColor.R >= p.PEndColor.B)
            {
                brightness = (255 - p.PEndColor.R) / p.PEndColor.R + 1;
            }
            if (brightness * (float)p.PEndColor.G > 255 && p.PEndColor.G >= p.PEndColor.R && p.PEndColor.G >= p.PEndColor.B)
            {
                brightness = (255 - p.PEndColor.G) / p.PEndColor.G + 1;
            }
            if (brightness * (float)p.PEndColor.B > 255 && p.PEndColor.B >= p.PEndColor.G && p.PEndColor.B >= p.PEndColor.R)
            {
                brightness = (255 - p.PEndColor.B) / p.PEndColor.B + 1;
            }
            p.PEndColor = new Color(p.PEndColor.R * brightness, p.PEndColor.G * brightness, p.PEndColor.B * brightness);


            p.PVel = Vector2.Normalize(RandomUtil.Next(pMinAngle, pMaxAngle)) *  (float)RandomUtil.Next(pMinVel, pMaxVel);
            p.PAccel = RandomUtil.Next(pMinAccel,pMaxAccel);
            p.PFriction = (float)RandomUtil.Next(pMinFriction,pMaxFriction);
            p.PKillLife = (float)RandomUtil.Next(pMinLife, pMaxLife);
            p.PUpdateFreq = pUpdateFreq;

            Vector2 v = Vector2.Normalize(RandomUtil.Next(new Vector2(-1,-1), new Vector2(1,1))) * (float)RandomUtil.Next(Radius);

            p.Position = MPSystem.Position + PositionOffset + v;

            return p;
        }
    }
}
