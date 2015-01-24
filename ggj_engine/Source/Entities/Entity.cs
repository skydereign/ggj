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

        protected Sprite sprite;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //
        }

        public virtual void Update(GameTime gameTime)
        {
            //
        }

        public virtual void Destroy()
        {
            //
        }
    }
}
