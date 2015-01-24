using ggj_engine.Source.Entities;
using ggj_engine.Source.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Collisions
{
    public abstract class Region
    {
        public Vector2 Position;
        protected Sprite sprite;
        public abstract void Draw (SpriteBatch spriteBatch);
    }
}
