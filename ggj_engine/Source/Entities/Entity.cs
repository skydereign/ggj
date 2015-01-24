using ggj_engine.Source.Collisions;
using ggj_engine.Source.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities
{
    public class Entity
    {
        public Vector2 Position;
        public bool Active;
        public Region CollisionRegion;
        public Screens.Screen MyScreen;

        // will have Regions which will handle triggers

        protected Sprite sprite;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(sprite != null)
            {
                sprite.Draw(spriteBatch);
            }
        }

        public virtual void DrawDebug(SpriteBatch spriteBatch)
        {
            if(CollisionRegion != null)
            {
                CollisionRegion.Draw(spriteBatch);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (sprite != null)
            {
                sprite.Position = Position;
                sprite.Update(gameTime);
            }

            if(CollisionRegion != null)
            {
                CollisionRegion.Position = Position;
                if(sprite != null)
                {
                    CollisionRegion.Position += new Vector2(sprite.Width / 2, sprite.Height / 2);
                }
            }
        }

        public virtual void Destroy()
        {
            //
        }

        public virtual bool Colliding (Entity other)
        {
            if(other.CollisionRegion != null && CollisionRegion != null)
            {
                return CollisionRegion.Colliding((CircleRegion)other.CollisionRegion);
            }
            return false;
        }

        public virtual void OnCollision (Entity other)
        {
            //
        }
    }
}
