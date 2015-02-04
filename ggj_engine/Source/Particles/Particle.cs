using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;
using ggj_engine.Source.Media;

namespace ggj_engine.Source.Particles
{
    public class Particle
    {
        public Vector2 Position;
        public Color PStartColor;
        public Color PEndColor;
        public float PStartScale;
        public float PEndScale;
        public Vector2 PVel;
        public Vector2 PAccel;
        public float PFriction;
        public float PKillLife;
        public float PCurrentLife;
        public int PUpdateFreq = 0;
        public bool Dead;

        private Color pCurrentColor;
        private float pCurrentScale;
        private Sprite sprite = ContentLibrary.Sprites["white_pixel"];
        private int pUpdateCur = 0;

        public Particle(Vector2 spawnPosition, Emitter e)
        {
            //Pick colors with deviations
            PStartColor = new Color(e.pStartColor.ToVector4() + RandomUtil.Next(-e.pStartColorDeviation.ToVector4(), e.pStartColorDeviation.ToVector4()));
            PEndColor = new Color(e.pEndColor.ToVector4() + RandomUtil.Next(-e.pEndColorDeviation.ToVector4(), e.pEndColorDeviation.ToVector4()));

            //Adjust colors by brightness deviations
            float brightness = (float)RandomUtil.Next(-e.pStartBrightnessDeviationFactor, e.pStartBrightnessDeviationFactor);
            brightness += 1;
            //Clamp brightness so colors do not exceed 255
            if (brightness * (float)PStartColor.R > 255 && PStartColor.R >= PStartColor.G && PStartColor.R >= PStartColor.B)
            {
                brightness = (255 - PStartColor.R) / PStartColor.R + 1;
            }
            if (brightness * (float)PStartColor.G > 255 && PStartColor.G >= PStartColor.R && PStartColor.G >= PStartColor.B)
            {
                brightness = (255 - PStartColor.G) / PStartColor.G + 1;
            }
            if (brightness * (float)PStartColor.B > 255 && PStartColor.B >= PStartColor.G && PStartColor.B >= PStartColor.R)
            {
                brightness = (255 - PStartColor.B) / PStartColor.B + 1;
            }
            PStartColor = new Color(PStartColor.ToVector4() * brightness);

            brightness = (float)RandomUtil.Next(-e.pEndBrightnessDeviationFactor, e.pEndBrightnessDeviationFactor);
            brightness += 1;
            //Clamp brightness so colors do not exceed 255
            if (brightness * (float)PEndColor.R > 255 && PEndColor.R >= PEndColor.G && PEndColor.R >= PEndColor.B)
            {
                brightness = (255 - PEndColor.R) / PEndColor.R + 1;
            }
            if (brightness * (float)PEndColor.G > 255 && PEndColor.G >= PEndColor.R && PEndColor.G >= PEndColor.B)
            {
                brightness = (255 - PEndColor.G) / PEndColor.G + 1;
            }
            if (brightness * (float)PEndColor.B > 255 && PEndColor.B >= PEndColor.G && PEndColor.B >= PEndColor.R)
            {
                brightness = (255 - PEndColor.B) / PEndColor.B + 1;
            }
            PEndColor = new Color(PEndColor.ToVector4() * brightness);

            PStartScale = e.pStartScale + (float)RandomUtil.Next(-e.pStartScaleDeviation, e.pStartScaleDeviation);
            PEndScale = e.pEndScale + (float)RandomUtil.Next(-e.pEndScaleDeviation, e.pEndScaleDeviation);

            PVel = Vector2.Normalize(RandomUtil.Next(e.pMinAngle, e.pMaxAngle)) * (float)RandomUtil.Next(e.pMinVel, e.pMaxVel);
            PAccel = RandomUtil.Next(e.pMinAccel, e.pMaxAccel);
            PFriction = (float)RandomUtil.Next(e.pMinFriction, e.pMaxFriction);
            PKillLife = (float)RandomUtil.Next(e.pMinLife, e.pMaxLife);
            PUpdateFreq = e.pUpdateFreq;

            Position = spawnPosition;
            sprite.Center = new Vector2(0.5f, 0.5f);
        }

        public void Update()
        {
            PVel += PAccel;
            PVel *= PFriction;
            Position += PVel;
            if (pUpdateCur == 0)
            {
                pCurrentScale = (PEndScale - PStartScale) * (((float)PCurrentLife) / ((float)PKillLife)) + PStartScale;
                pCurrentColor = new Color(Vector4.Lerp(PStartColor.ToVector4(), PEndColor.ToVector4(), ((float)PCurrentLife) / ((float)PKillLife)));

            }
            pUpdateCur++;
            if (pUpdateCur >= PUpdateFreq)
            {
                pUpdateCur = 0;
            }

            PCurrentLife++;
            if (PCurrentLife >= PKillLife)
            {
                Dead = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Tint = pCurrentColor;
            sprite.Position = Position;
            sprite.ScaleX = pCurrentScale;
            sprite.ScaleY = pCurrentScale;

            sprite.Draw(spriteBatch);
        }
    }
}
