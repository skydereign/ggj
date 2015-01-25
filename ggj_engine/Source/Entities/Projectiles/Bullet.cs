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
    class Bullet : Projectile
    {
        private const float SPEED = 10f;

        public Bullet(Vector2 position, Vector2 direction)
        {
            Position = position;
            Velocity = Vector2.Normalize(direction) * SPEED;
            sprite = ContentLibrary.Sprites["white_pixel"];
            sprite.Tint = new Color(255, 255, 130);
            sprite.ScaleX = 2f;
            sprite.ScaleY = 2f;
        }

        public override void Update(GameTime gameTime)
        {
            Position += Velocity;
            sprite.Rotation = (float)Math.Atan2(Velocity.Y, Velocity.X);

            base.Update(gameTime);
        }

    }
}
