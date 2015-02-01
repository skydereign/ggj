using ggj_engine.Source.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Utility
{
    static class Debug
    {
        public static Texture2D pixel;
        public static Texture2D circle;


        public static void LoadContent()
        {
            pixel = ContentLibrary.Sprites["white_pixel"].Texture;
            circle = ContentLibrary.Sprites["circle_filled"].Texture;
        }

        /// <summary>
        /// Draws a line
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="a">Start point</param> 
        /// <param name="b">End point</param> 
        /// <param name="color">Color</param> 
        /// <param name="thickness">Thickness</param> 
        public static void DrawLine(SpriteBatch spriteBatch, Vector2 a, Vector2 b, Color color, float thickness)
        {
            float angle = (float)Math.Atan2(b.Y - a.Y, b.X - a.X);
            spriteBatch.Draw(pixel, a, null, color, (float)(angle - Math.PI / 2), new Vector2(0.5f, 0f), new Vector2(thickness, Vector2.Distance(a, b)), SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draws a triangle
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="a">Vertex A</param> 
        /// <param name="b">Vertex B</param> 
        /// <param name="c">Vertex C</param>
        /// <param name="color">Color</param>
        /// <param name="thickness">Thickness</param>
        /// <param name="highlightVertex">true to highlight verticies with circles</param>
        public static void DrawTriangle(SpriteBatch spriteBatch, Vector2 a, Vector2 b, Vector2 c, Color color, float thickness, bool highlightVertex)
        {
            DrawLine(spriteBatch, a, b, color, thickness);
            DrawLine(spriteBatch, b, c, color, thickness);
            DrawLine(spriteBatch, c, a, color, thickness);
            if (highlightVertex)
            {
                DrawCircle(spriteBatch, a, color, 4);
                DrawCircle(spriteBatch, b, color, 4);
                DrawCircle(spriteBatch, c, color, 4);
            }
        }

        /// <summary>
        /// Draws a circle
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="center">Center of circle</param>
        /// <param name="color">Color</param>
        /// <param name="radius">Radius</param>
        public static void DrawCircle(SpriteBatch spriteBatch, Vector2 center, Color color, float radius)
        {
            spriteBatch.Draw(circle, center, null, color, 0, new Vector2(Globals.DebugCircleSize/2, Globals.DebugCircleSize/2), radius / (Globals.DebugCircleSize/2f), SpriteEffects.None, 0);
        }
    }
}
