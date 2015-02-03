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
    class GenericButtonGUI : GUI
    {
        // delegate for callback
        public delegate void Callback();

        Callback callback;
        private SpriteFont font;
        private Vector2 dimensions;

        public GenericButtonGUI(string label, Vector2 position, Callback callback) : base("")
        {
            Position = position;
            this.callback = callback;
            this.label = label;

            font = ContentLibrary.Fonts["pixelFont"];
            dimensions = font.MeasureString(label);
            dimensions *= Globals.GUIScale;
        }

        public override void Update(GameTime gameTime)
        {
            if (InputControl.GetMouseOnLeftPressed())
            {
                Vector2 mousePos = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());
                
                if (mousePos.X >= Position.X && mousePos.X <= Position.X + dimensions.X && mousePos.Y >= Position.Y && mousePos.Y <= Position.Y + dimensions.Y)
                {
                    // click
                    callback();
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, label, Position, Color.White, 0, Vector2.Zero, Globals.GUIScale, SpriteEffects.None, 0);
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
            return Position.X + font.MeasureString(label).X * Globals.GUIScale;
        }

        public override float Left()
        {
            return Position.X;
        }
    }
}
