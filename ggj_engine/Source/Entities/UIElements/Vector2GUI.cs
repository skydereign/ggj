using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.UIElements
{
    class Vector2GUI : GUI
    {
        public Vector2 Value;

        private NumberButton[] values;
        private SpriteFont font;

        public Vector2GUI(string label, float initial, float step, float min, float max) : base(label)
        {
            Value = Vector2.Zero;
            values = new NumberButton[2];
            font = ContentLibrary.Fonts["pixelFont"];

            for (int i = 0; i < 2; i++)
            {
                values[i] = new NumberButton("", Vector2.Zero, initial, step, min, max);
            }

            ResetPositions();
        }

        public override void Init()
        {
            for (int i = 0; i < 2; i++)
            {
                values[i].MyScreen = MyScreen;
                values[i].Init();
            }
            base.Init();
        }
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < 2; i++)
            {
                values[i].Update(gameTime);
            }
            Value.X = values[0].Value;
            Value.Y = values[1].Value;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, label, Position, Color.White, 0, Vector2.Zero, Globals.GUIScale, SpriteEffects.None, 0);
        
            for (int i = 0; i < 2; i++)
            {
                values[i].Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }

        public override void ResetPositions()
        {
            Vector2 offset = Vector2.Zero;
            offset.X = font.MeasureString(label).X * Globals.GUIScale;

            for (int i = 0; i < 2; i++)
            {
                values[i].Position = Position + offset;
                offset.X += font.MeasureString(values[i].Value.ToString() + " ").X * Globals.GUIScale;
            }
        }
        public override float Top()
        {
            return Position.Y;
        }

        public override float Bot()
        {
            string str = values[0].Value.ToString();
            return Position.Y + font.MeasureString(str).Y * Globals.GUIScale;
        }

        public override float Right()
        {
            string str = values[0].Value.ToString() + " " + values[1].Value.ToString();
            return Position.X + font.MeasureString(str).X * Globals.GUIScale + 10f + (font.MeasureString("000 ").X * 3) * Globals.GUIScale;
        }

        public override float Left()
        {
            return Position.X;
        }
    }
}
