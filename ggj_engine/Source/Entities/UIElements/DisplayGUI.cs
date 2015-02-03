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
    class DisplayGUI : GUI
    {
        public string Value;
        public delegate string Text();

        private Text updateFunction;
        private SpriteFont font;

        public DisplayGUI(Text updateFunction) : base("")
        {
            font = ContentLibrary.Fonts["pixelFont"];
            this.updateFunction = updateFunction;
        }

        public override void Update(GameTime gameTime)
        {
            Value = updateFunction();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, label + Value, Position, Color.White, 0, Vector2.Zero, Globals.GUIScale, SpriteEffects.None, 0);
        }

        public override float Top()
        {
            return Position.Y;
        }

        public override float Bot()
        {
            return Position.Y + font.MeasureString(Value).Y * Globals.GUIScale;
        }

        public override float Right()
        {
            return Position.X + font.MeasureString(label + Value).X * Globals.GUIScale;
        }

        public override float Left()
        {
            return Position.X;
        }
    }
}
