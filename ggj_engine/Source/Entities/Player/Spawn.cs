using ggj_engine.Source.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.Player
{
    class Spawn : Entity
    {
        public Spawn(float x, float y)
        {
            Position = new Vector2(x, y);
            sprite = ContentLibrary.Sprites["circle_region"];
            sprite.Tint = Color.Green;
        }

    }
}
