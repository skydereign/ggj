using ggj_engine.Source.Collisions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.Projectiles
{
    class Explosion : Projectile
    {
        public int Life = 40;

        public Explosion(Vector2 position, Entity owner)
        {
            Position = position;
            Velocity = Vector2.Zero;
            CollisionRegion = new CircleRegion(75, position);
            Damage = 45;
            Owner = owner;
        }

        public override void Update(GameTime gameTime)
        {
            Life--;
            if (Life <=0)
            {
                MyScreen.DeleteEntity(this);
            }
            base.Update(gameTime);
        }
    }
}
