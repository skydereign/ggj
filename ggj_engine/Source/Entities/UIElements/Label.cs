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
    class Label : Entity
    {
        private String label;
        private Color textColor;
        private float scaleSize;

        public Label(String label, Vector2 position, Color textColor, float scaleSize)
        {
            this.label = label;
            Position = position;
            this.textColor = textColor;
            this.scaleSize = scaleSize;
        }

        public override void Init()
        {
            Position = MyScreen.Camera.ScreenToWorld(Position);
            base.Init();
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 mousePosition = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());

            Position.X += Globals.BackgroundAnimation;

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(ContentLibrary.Fonts["pixelFont"], label, Position, textColor, 0, Vector2.Zero, scaleSize, SpriteEffects.None, 0);
        }

        public override void OnCollision(Entity other)
        {            
            base.OnCollision(other);
        }
    }
}
