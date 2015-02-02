using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Utility
{
    public static class Globals
    {
        public static int TileSize = 16;
        public static bool DebugEntities = true;
        public static float DebugCircleSize = 32f;
        public static float WallPushback = 3f;
        public static float MaxVelocity = 200f;
        public static float BackgroundAnimation = 0.25f;
        public static int MaxControllers = 4;

        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        public static Vector2 Right = new Vector2(1, 0);
        public static Vector2 UpRight = new Vector2(1, -1);
        public static Vector2 Up = new Vector2(0, -1);
        public static Vector2 UpLeft = new Vector2(-1, -1);
        public static Vector2 Left = new Vector2(-1, 0);
        public static Vector2 DownLeft = new Vector2(-1, 1);
        public static Vector2 Down = new Vector2(0, 1);
        public static Vector2 DownRight = new Vector2(1, 1);


        // particle gui
        public static float GUIScale = 0.2f;
    }
}
