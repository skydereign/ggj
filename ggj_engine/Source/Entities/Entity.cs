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

        // will have Regions which will handle triggers

        protected Sprite sprite;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(sprite != null)
            {
                spriteBatch.Begin();
                sprite.Draw(spriteBatch);
                spriteBatch.End();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (sprite != null)
            {
                sprite.Update(gameTime);
            }
        }

        public virtual void Destroy()
        {
            //
        }
    }
}
