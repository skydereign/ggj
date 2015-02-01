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
    class ColorGUI : Entity
    {
        public Color Value;

        private SpriteFont font;
        private NumberButton[] values;
        private string label;

        public ColorGUI(string label, Vector2 position)
        {
            Position = position;
            this.label = label;
            values = new NumberButton[3];
            font = ContentLibrary.Fonts["pixelFont"];

            Vector2 offset = Vector2.Zero;
            offset.X = font.MeasureString(label).X*Globals.GUIScale + 10f;
            for (int i = 0; i < 3; i++)
            {
                values[i] = new NumberButton(Position + offset, 127, 1, 0, 255);
                offset.X += font.MeasureString("000 ").X*Globals.GUIScale;
            }
        }

        public override void Init()
        {
            for (int i = 0; i < 3; i++)
            {
                values[i].MyScreen = MyScreen;
            }
            base.Init();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < 3; i++)
            {
                values[i].Update(gameTime);
            }

            Value.R = (byte)values[0].Value;
            Value.G = (byte)values[1].Value;
            Value.B = (byte)values[2].Value;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, label, Position, Value, 0, Vector2.Zero, Globals.GUIScale, SpriteEffects.None, 0);
        
            for (int i = 0; i < 3; i++)
            {
                values[i].Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }

    }
}
