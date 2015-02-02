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

        private const int CIRCLE_DEFAULT_SEGMENTS = 16;

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
            DrawCircle(spriteBatch, center, color, radius, 0, Color.Black);
        }

        /// <summary>
        /// Draws a circle with colored outline
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="center"></param>
        /// <param name="color"></param>
        /// <param name="radius"></param>
        /// <param name="outlineThickness"></param>
        /// <param name="outlineColor"></param>
        public static void DrawCircle(SpriteBatch spriteBatch, Vector2 center, Color color, float radius, float outlineThickness, Color outlineColor)
        {
            spriteBatch.Draw(circle, center, null, color, 0, new Vector2(Globals.DebugCircleSize / 2, Globals.DebugCircleSize / 2), radius / (Globals.DebugCircleSize / 2f), SpriteEffects.None, 0);

            if (outlineThickness != 0)
            {
                float angle1, angle2;
                for (int i = 0; i < CIRCLE_DEFAULT_SEGMENTS; i++)
                {
                    angle1 = (float)(Math.PI * 2.0f / CIRCLE_DEFAULT_SEGMENTS) * i;
                    angle2 = (float)(Math.PI * 2.0f / CIRCLE_DEFAULT_SEGMENTS) * (i + 1);
                    DrawLine(spriteBatch, new Vector2((float)Math.Cos(angle1), (float)Math.Sin(angle1)) * radius + center, new Vector2((float)Math.Cos(angle2), (float)Math.Sin(angle2)) * radius + center,
                        outlineColor, outlineThickness);
                }
            }
        }

        /// <summary>
        /// Draws the outline of a circle
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="center"></param>
        /// <param name="color"></param>
        /// <param name="radius"></param>
        /// <param name="outlineThickness"></param>
        /// <param name="segments"></param>
        public static void DrawOutlineCircle(SpriteBatch spriteBatch, Vector2 center, Color color, float radius, float outlineThickness, int segments)
        {
            float angle1, angle2;
            for (int i = 0; i < segments; i++)
            {
                angle1 = (float)(Math.PI * 2.0f / segments) * i;
                angle2 = (float)(Math.PI * 2.0f / segments) * (i + 1);
                DrawLine(spriteBatch, new Vector2((float)Math.Cos(angle1), (float)Math.Sin(angle1)) * radius + center, new Vector2((float)Math.Cos(angle2), (float)Math.Sin(angle2)) * radius + center,
                    color, outlineThickness);
            }
        }

        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="fillColor"></param>
        public static void DrawRectangle(SpriteBatch spriteBatch, float top, float left, float bottom, float right, Color fillColor)
        {
            DrawRectangle(spriteBatch, top, left, bottom, right, fillColor, 0, Color.Black);
        }

        /// <summary>
        /// Draws a rectangle with outline
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="fillColor"></param>
        /// <param name="outlineThickness"></param>
        /// <param name="outlineColor"></param>
        public static void DrawRectangle(SpriteBatch spriteBatch, float top, float left, float bottom, float right, Color fillColor, float outlineThickness, Color outlineColor)
        {
            spriteBatch.Draw(pixel, new Vector2((right + left) / 2, (top + bottom) / 2), null, fillColor, 0, new Vector2(0.5f, 0.5f), new Vector2(right - left, bottom - top), SpriteEffects.None, 0);

            DrawLine(spriteBatch, new Vector2(left - outlineThickness * 0.5f, top), new Vector2(right + outlineThickness * 0.5f, top), outlineColor, outlineThickness);
            DrawLine(spriteBatch, new Vector2(right, top - outlineThickness * 0.5f), new Vector2(right, bottom + outlineThickness * 0.5f), outlineColor, outlineThickness);
            DrawLine(spriteBatch, new Vector2(right + outlineThickness * 0.5f, bottom), new Vector2(left - outlineThickness * 0.5f, bottom), outlineColor, outlineThickness);
            DrawLine(spriteBatch, new Vector2(left, bottom + outlineThickness * 0.5f), new Vector2(left, top - outlineThickness * 0.5f), outlineColor, outlineThickness);
        }
    }
}
