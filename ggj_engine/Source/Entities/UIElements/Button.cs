using ggj_engine.Source.Media;
using ggj_engine.Source.Screens;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.UIElements
{
    class Button : Entity
    {

        private String label;
        private Color textColor;
        private float scaleSize;
        private Vector2 size;
        private Vector2 labelPosition;

        private Vector2 startPos;
        private Vector2 endPos;

        public Button(String label, Vector2 position, Color textColor, float scaleSize, Vector2 size)
        {
            this.label = label;
            Position = position;
            this.textColor = textColor;
            this.scaleSize = scaleSize;
            this.size = size;

            this.startPos = new Vector2(position.X, position.Y);
            this.endPos = new Vector2(position.X + size.X, position.Y);
        }

        public override void Init()
        {
            labelPosition = MyScreen.Camera.ScreenToWorld(Position);
            base.Init();
        }
        public override void Update(GameTime gameTime)
        {
            labelPosition.X += Globals.BackgroundAnimation;

            Vector2 mousePosition = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());
            Console.WriteLine(mousePosition.ToString() + " " + labelPosition.ToString());

            if (mousePosition.X > labelPosition.X && mousePosition.X < (labelPosition.X + size.X)
                && mousePosition.Y > labelPosition.Y && mousePosition.Y < (labelPosition.Y + size.Y))
            {
                if (InputControl.GetMouseOnLeftPressed())
                {
                    Game1.PushScreen(new TestPlayerScreen());
                }
                textColor = Color.LimeGreen;
            }
            else if (!InputControl.GetMouseOnLeftPressed())
            {
                textColor = Color.Gray;
            }

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(ContentLibrary.Fonts["pixelFont"], label, labelPosition, textColor, 0, Vector2.Zero, scaleSize, SpriteEffects.None, 0);
        }

        public override void OnCollision(Entity other)
        {            
            base.OnCollision(other);
        }
    }
}
