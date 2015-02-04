using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Media;

namespace ggj_engine.Source.Particles
{
    public abstract class PSystem : Entities.Entity
    {
        public List<Emitter> Emitters = new List<Emitter>();
        public bool Dead;
        private int deathTimer;

        /// <summary>
        /// Update loop
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (Emitter e in Emitters)
            {
                e.Update();
            }
            if (Dead)
            {
                deathTimer--;
                if (deathTimer <= 0)
                {
                    MyScreen.DeleteEntity(this);
                }
            }
            base.Update(gameTime);
        }

        public void Kill(int timeToDeath)
        {
            deathTimer = timeToDeath;
            Dead = true;
        }

        /// <summary>
        /// Adds a new emitter to the system
        /// </summary>
        /// <param name="e"></param>
        public void AddEmitter(Emitter e)
        {
            e.MPSystem = this;
            Emitters.Add(e);
        }

        /// <summary>
        /// Removes emitter from the system
        /// </summary>
        /// <param name="e"></param>
        public void RemoveEmitter(Emitter e)
        {
            Emitters.Remove(e);
        }

        /// <summary>
        /// Bursts particels from this system
        /// </summary>
        public void BurstParticles()
        {
            foreach(Emitter e in Emitters)
            {
                e.BurstParticles();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(Emitter e in Emitters)
            {
                e.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }
    }
}
