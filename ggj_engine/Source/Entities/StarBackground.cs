using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ggj_engine.Source.Collisions;
using ggj_engine.Source.Media;
using ggj_engine.Source.Movement;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ggj_engine.Source.Entities
{
    class StarBackground : Entity
    {
        public const int NUM_STARS = 1000;

        private List<Star> Stars;
        public Texture2D StarTex = ContentLibrary.Sprites["white_pixel"].Texture;

        private Vector2 cameraLastPos;

        public StarBackground()
        {
            Stars = new List<Star>();
            for(int i=0;i<NUM_STARS;i++)
            {
                Star star = new Star();
                star.depth = (float)Math.Max(0.05f,RandomUtil.Next(1));
                star.Position = new Vector2((float)RandomUtil.Next(-800, 800), (float)RandomUtil.Next(-800, 800));
                Stars.Add(star);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(Star star in Stars)
            {
                star.Position += (MyScreen.Camera.Position - cameraLastPos) * (1-star.depth) * 2f;
                spriteBatch.Draw(StarTex, star.Position, null, Color.White, 0, Vector2.Zero, star.depth * 3, SpriteEffects.None, 1f);
            }
            cameraLastPos = MyScreen.Camera.Position;
            base.Draw(spriteBatch);
        }

        private class Star
        {
            public Vector2 Position;
            public float depth;
        }
    }
}
