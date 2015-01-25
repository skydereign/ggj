using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ggj_engine.Source.AI.Pathing;
using ggj_engine.Source.Level;
using ggj_engine.Source.Entities.Projectiles;

namespace ggj_engine.Source.Entities.Enemies
{
    public abstract class Enemy : Entity
    {
        public float Speed, FireDelay;
        public Stack<Tile> CurrentPath;
        public Tile CurrentTile;
        public bool Patrolling, Following, Attacking, Evading;
        public bool PopOffTop, PerformingAction;
        protected int health;
        protected int damage;
        protected float fireCounter;
        protected List<Vector2> wayPoints;
        
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
            if(health < 0)
            {
                if (this is Follower)
                {
                    MyScreen.GameManager.ScoreManager.GrantEnemyFollowerKill(Position);
                }
                if (this is YourMom)
                {
                    MyScreen.GameManager.ScoreManager.GrantEnemyFollowerKill(Position);
                }
                MyScreen.DeleteEntity(this);
            }
            base.Update(gameTime);
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        protected void DecreaseHealth(int amount)
        {
            health -= amount;
        }
        public override void OnCollision(Entity other)
        {
            if (other is Projectile)
            {
                Projectile temp = (Projectile)other;
                if(temp.Owner is Player.Player)
                {
                    DecreaseHealth(5);
                    MyScreen.DeleteEntity(temp);
                }
            }
            base.OnCollision(other);
        }
    }
}
