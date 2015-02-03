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
    class BoolGUI : GUI
    {
        public bool Value;
        private SpriteFont font;
        public BoolGUI(string label, bool initial) : base(label)
        {
            font = ContentLibrary.Fonts["pixelFont"];
            Value = initial;
        }
        public override void Update(GameTime gameTime)
        {
            if (InputControl.GetMouseOnLeftPressed())
            {
                Vector2 mousePos = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());
                Vector2 dimensions = font.MeasureString(label + Value.ToString()) * Globals.GUIScale;

                if (mousePos.X >= Position.X && mousePos.X <= Position.X + dimensions.X && mousePos.Y >= Position.Y && mousePos.Y <= Position.Y + dimensions.Y)
                {
                    Value = !Value;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, label + Value.ToString(), Position, Color.White, 0, Vector2.Zero, Globals.GUIScale, SpriteEffects.None, 0);
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
            return Position.X + (font.MeasureString(label).X + font.MeasureString(Value.ToString()).X) * Globals.GUIScale;
        }

        public override float Left()
        {
            return Position.X;
        }
    }
}
