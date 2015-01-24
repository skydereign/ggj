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
        public int Radius;
        
        public CircleRegion (int radius, Vector2 position)
        {
            Radius = radius;
            Position = position;
            sprite = ContentLibrary.Sprites["circle_region"];
            sprite.ScaleX = radius / Globals.DebugCircleSize;
            sprite.ScaleY = radius / Globals.DebugCircleSize;
            Console.WriteLine("scalex = " + sprite.ScaleX);
        }

        public bool Colliding(CircleRegion other)
        {
            float distance = (Position - other.Position).Length();
            return distance < Radius + other.Radius;
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            sprite.Position = Position -= new Vector2(Radius / 2, Radius / 2); ;
            sprite.Draw(spriteBatch);
        }
    }
}
