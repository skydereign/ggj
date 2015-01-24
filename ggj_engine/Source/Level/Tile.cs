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
        public float g_score, h_score, f_score;

        public Tile(int type, bool walkable, int x, int y)
        {
            Type = type;
            Walkable = walkable;
            X = x;
            Y = y;
            Parent = null;
            g_score = 0;
            h_score = 0;
            f_score = 0;
        }
    }
}
