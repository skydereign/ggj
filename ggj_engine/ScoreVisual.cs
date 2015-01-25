using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ggj_engine.Source.Entities
{
    class ScoreVisual : Entity
    {
        private const int START_LIFE = 60;
        private const float ACCELERATION = 0.2f;
        private const float START_VEL = -0.5f;
        private SpriteFont font = ContentLibrary.Fonts["pixelFont"];
        private int life;
        private float Velocity;
        private int amount;
        private Color color;
        private int seizureTick;

        public ScoreVisual(Vector2 position, int amount)
        {
            Position = position;
            this.amount = amount;
            Velocity = START_VEL;
            life = START_LIFE;
            if (amount < 0)
            {
                color = Color.Red;
            }
            else if (amount > 0)
            {
                color = Color.LimeGreen;
            }
        }

        public override void Update(GameTime gameTime)
        {
            //Velocity += ACCELERATION;
            seizureTick++;
            Position.Y += Velocity;
            if (amount < 0)
            {
                if (seizureTick % 8 == 0)
                {
                    Position.X -= 6;
                }
                else if (seizureTick % 8 == 4)
                {
                    Position.X += 6;
                }
            }
            life--;
            if (life <= 0)
            {
                MyScreen.DeleteEntity(this);
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, amount.ToString(), Position, color, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1);
            base.Draw(spriteBatch);
        }
    }
}
