using ggj_engine.Source.Entities;
using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Collisions
{
    public class CircleRegion : Region
    {
        private int radius;
        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                sprite.ScaleX = radius * 2 / Globals.DebugCircleSize;
                sprite.ScaleY = radius * 2 / Globals.DebugCircleSize;
                sprite.CenterOrigin();
            }
        }

        
        public CircleRegion (int radius, Vector2 position)
        {
            Position = position;
            sprite = ContentLibrary.Sprites["circle_region"];
            Radius = radius;
            Active = true;
        }

        public override bool Colliding(Region other)
        {
            if (Active && other.Active)
            {
                CircleRegion circleOther = (CircleRegion)other;
                float distance = (Position - other.Position).Length();
                return distance < Radius + circleOther.Radius;
            }
            return false;
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            sprite.Position = Position -= new Vector2(Radius, Radius);
            sprite.Draw(spriteBatch);
        }
    }
}
