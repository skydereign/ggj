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
        private List<Emitter> emitters = new List<Emitter>();

        /// <summary>
        /// Update loop
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (Emitter e in emitters)
            {
                e.Update();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Adds a new emitter to the system
        /// </summary>
        /// <param name="e"></param>
        public void AddEmitter(Emitter e)
        {
            emitters.Add(e);
        }

        /// <summary>
        /// Bursts particels from this system
        /// </summary>
        public void BurstParticles()
        {
            foreach(Emitter e in emitters)
            {
                e.BurstParticles();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(Emitter e in emitters)
            {
                e.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }
    }
}
