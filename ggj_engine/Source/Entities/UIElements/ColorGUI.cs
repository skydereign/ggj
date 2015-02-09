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
    class ColorGUI : GUI
    {
        public Color Value;

        private SpriteFont font;
        private NumberButton[] values;

        public ColorGUI(string label, Vector2 position, Vector4 startColor) : base(label)
        {
            Position = position;
            values = new NumberButton[4];
            font = ContentLibrary.Fonts["pixelFont"];

            values[0] = new NumberButton("", Vector2.Zero, startColor.X, 1, 0, 255);
            values[1] = new NumberButton("", Vector2.Zero, startColor.Y, 1, 0, 255);
            values[2] = new NumberButton("", Vector2.Zero, startColor.Z, 1, 0, 255);
            values[3] = new NumberButton("", Vector2.Zero, startColor.W, 1, 0, 255);

            ResetPositions();
        }

        public override void Init()
        {
            for (int i = 0; i < 4; i++)
            {
                values[i].MyScreen = MyScreen;
                values[i].Init();
            }
            base.Init();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < 4; i++)
            {
                values[i].Update(gameTime);
            }

            Value.R = (byte)values[0].Value;
            Value.G = (byte)values[1].Value;
            Value.B = (byte)values[2].Value;
            Value.A = (byte)values[3].Value;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, label, Position, Value, 0, Vector2.Zero, Globals.GUIScale, SpriteEffects.None, 0);
        
            for (int i = 0; i < 4; i++)
            {
                values[i].Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }

        public void Set(Color color)
        {
            Value = color;
            values[0].Value = Value.R;
            values[1].Value = Value.G;
            values[2].Value = Value.B;
            values[3].Value = Value.A;
        }

        public override void ResetPositions ()
        {
            Vector2 offset = Vector2.Zero;
            offset.X = font.MeasureString(label).X*Globals.GUIScale;
            for (int i = 0; i < 4; i++)
            {
                values[i].Position = Position + offset;
                offset.X += font.MeasureString("000 ").X*Globals.GUIScale;
            }
        }

        public override float Top()
        {
            return Position.Y;
        }

        public override float Bot()
        {
            return Position.Y + font.MeasureString(label).Y * Globals.GUIScale;
        }

        public override float Right()
        {
            // does not use the NumberButton's Right to calculate because of fixed width
            return Position.X + font.MeasureString(label ).X * Globals.GUIScale + 10f + (font.MeasureString("000 ").X * 3) * Globals.GUIScale;
        }

        public override float Left()
        {
            return Position.X;
        }

    }
}
