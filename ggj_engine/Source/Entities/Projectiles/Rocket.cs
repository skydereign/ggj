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
    class Rocket : Projectile
    {
        private const float SPEED = 25f;
        private Particles.PSystemRocketSmoke pSystemSmoke;

        public Rocket(Vector2 position, Vector2 direction)
        {
            Position = position;
            Velocity = Vector2.Normalize(direction) * SPEED;
            sprite = ContentLibrary.Sprites["white_pixel"];
            sprite.Tint = new Color(150, 255, 150);
            sprite.ScaleX = 17f;
            sprite.ScaleY = 5f;

        }

        public override void Update(GameTime gameTime)
        {
            if (pSystemSmoke == null)
            {
                pSystemSmoke = new Particles.PSystemRocketSmoke();
                MyScreen.AddEntity(pSystemSmoke);
            }

            Position += Velocity;
            sprite.Rotation = (float)Math.Atan2(Velocity.Y, Velocity.X);
            pSystemSmoke.Angle = sprite.Rotation;
            pSystemSmoke.Position = Position;

            base.Update(gameTime);
        }

    }
}
