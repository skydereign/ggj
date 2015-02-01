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
