using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;

namespace ggj_engine.Source.Particles
{
    public abstract class Emitter
    {
        public Vector2 PositionOffset = Vector2.Zero;
        public PSystem MPSystem;

        //Defaults for particles spawned from this emitter
        public Color pStartMinColor = Color.White;
        public Color pStartMaxColor = Color.White;
        public Color pEndMinColor = Color.White;
        public Color pEndMaxColor = Color.White;
        public Vector2 pMinAngle = new Vector2(-1, -1);
        public Vector2 pMaxAngle = new Vector2(1, 1);
        public float pMinVel = -1;
        public float pMaxVel = 1;
        public Vector2 pMinAccel = new Vector2(0, 0);
        public Vector2 pMaxAccel = new Vector2(0, 0);
        public float pMinFriction = 1;
        public float pMaxFriction = 1;
        public float pMinLife = 20;
        public float pMaxLife = 20;
        public int pUpdateFreq = 1;
        public int pMinSpawn = 1;
        public int pMaxSpawn = 1;
        public bool Burst = false;

        private List<Particle> particles = new List<Particle>();

        public void Update()
        {
            if (!Burst)
            {
                if (pMinSpawn < 1)
                {
                    if (RandomUtil.Next(pMinSpawn) == 0)
                    {
                        SpawnParticle();
                    }
                }
                else
                {
                    int number = (int)RandomUtil.Next(pMinSpawn, pMaxSpawn);
                    for (int i = 0; i < number; i++)
                    {
                        particles.Add(SpawnParticle());
                    }
                }
            }

            foreach(Particle p in particles)
            {
                p.Update();
            }

            //Clean up dead particles
            for(int i=0;i<particles.Count;i++)
            {
                if (particles[i].Dead)
                {
                    particles.Remove(particles[i]);
                    i--;
                }
            }
        }

        /// <summary>
        /// Bursts the particles this emitter has been set up to burst
        /// </summary>
        public void BurstParticles()
        {
            int number = (int)RandomUtil.Next(pMinSpawn, pMaxSpawn);
            for (int i = 0; i < number; i++)
            {
                particles.Add(SpawnParticle());
            }
        }

        public abstract Particle SpawnParticle();

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Particle p in particles)
            {
                p.Draw(spriteBatch);
            }
        }
    }
}

