using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ggj_engine.Source.Entities.Projectiles
{
    class Arrow : Projectile
    {
        private const float SPEED = 4f;

        public Arrow(Vector2 position, Vector2 direction)
        {
            Position = position;
            Velocity = Vector2.Normalize(direction) * SPEED;
            sprite = ContentLibrary.Sprites["white_pixel"];
            sprite.Tint = new Color(160, 150, 25);
            sprite.ScaleX = 14f;
            sprite.ScaleY = 3f;
        }

        public override void Update(GameTime gameTime)
        {
            Position += Velocity;
            sprite.Rotation = (float)Math.Atan2(Velocity.Y, Velocity.X);

            base.Update(gameTime);
        }

    }
}
