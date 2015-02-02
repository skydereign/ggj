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
    class GenericButton : Entity
    {
        // delegate for callback
        public delegate void Callback();

        Callback callback;
        private string label;
        private SpriteFont font;
        private Vector2 dimensions;

        public GenericButton(string label, Vector2 position, Callback callback)
        {
            Position = position;
            this.label = label;
            this.callback = callback;

            font = ContentLibrary.Fonts["pixelFont"];
            dimensions = font.MeasureString(label);
            dimensions *= Globals.GUIScale;
            Console.WriteLine("dimensions = " + dimensions);
        }

        public override void Init()
        {
            Position = MyScreen.Camera.ScreenToWorld(Position);
            base.Init();
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
    }
}
