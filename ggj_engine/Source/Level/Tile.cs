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
        public int X, Y;
        public Tile Parent;
        public float GScore, HScore, FScore;

        public Tile(int type, bool walkable, int x, int y)
        {
            Type = type;
            Walkable = walkable;
            X = x;
            Y = y;
            Parent = null;
            GScore = 0;
            HScore = 0;
            FScore = 0;
        }
    }
}
