using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ggj_engine.Source.AI.Pathing;

namespace ggj_engine.Source.Entities.Enemies
{
    public abstract class Enemy : Entity
    {
        protected int health;
        protected int damage;
        protected float speed;
        
        protected virtual void SetDecisionTree()
        {
            //
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            sprite.Position = Position;
            if(health < 0)
            {
                Destroy();
            }
            base.Update(gameTime);
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        protected void DecreaseHealth()
        {
            health--;
        }
    }
}
