using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Level
{
    public class Tile
    {
        public int Type;
        public bool Walkable;

        public Tile(int type, bool walkable)
        {
            Type = type;
            Walkable = walkable;
        }
    }
}
