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
        public Color PColor;
        public Vector2 PVel;
        public Vector2 PAccel;
        public float PFriction;
        public float PKillLife;
        public float PCurrentLife;
        public int PUpdateFreq = 0;
        public bool Dead;

        private Sprite sprite = ContentLibrary.Sprites["white_pixel"];
        private int PUpdateCur = 0;

        public void Update()
        {
            PVel += PAccel;
            PVel *= PFriction;
            Position += PVel;
            if (PUpdateCur == 0)
            {
                PColor = new Color(Vector4.Lerp(PStartColor.ToVector4(), PEndColor.ToVector4(), ((float)PCurrentLife) / ((float)PKillLife)));
            }
            PUpdateCur++;
            if (PUpdateCur >= PUpdateFreq)
            {
                PUpdateCur = 0;
            }

            PCurrentLife++;
            if (PCurrentLife >= PKillLife)
            {
                Dead = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Tint = PColor;
            sprite.Position = Position;
            sprite.ScaleX = 4;
            sprite.ScaleY = 4;

            sprite.Draw(spriteBatch);
        }
    }
}
