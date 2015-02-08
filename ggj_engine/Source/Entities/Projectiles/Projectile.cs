using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Level;
using ggj_engine.Source.Collisions;

namespace ggj_engine.Source.Entities.Projectiles
{
    public abstract class Projectile : Entity
    {
        public Vector2 Velocity;
        public Entity Owner;
        public float Damage;

        public ProjectileEmitter Parent;
        public int Timer;
        public int State;
        public int InitialAngle;

        public override void Update(GameTime gameTime)
        {
            if(Parent != null)
            {
                Parent.States[State].Update(this);
                Parent.States[State].CheckNextState(this);
            }
            TileGrid.AdjustedForCollisions(this, Position, Position, (CircleRegion)CollisionRegion);
            base.Update(gameTime);
        }

        public override void OnTileCollision()
        {
            MyScreen.DeleteEntity(this);
            base.OnTileCollision();
        }
    }
}
