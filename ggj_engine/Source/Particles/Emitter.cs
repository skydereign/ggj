using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ggj_engine.Source.Particles
{
    abstract class Emitter
    {
        public Vector2 Position;

        //Defaults for particles spawned from this emitter
        private Color pStartMinColor = Color.White;
        private Color pStartMaxColor = Color.White;
        private Color pEndMinColor = Color.White;
        private Color pEndMaxColor = Color.White;
        private Vector2 pMinVel = new Vector2(-1, -1);
        private Vector2 pMaxVel = new Vector2(1, 1);
        private Vector2 pMinAccel = new Vector2(0, 0);
        private Vector2 pMaxAccel = new Vector2(0, 0);
        private float pMinFriction = 0;
        private float pMaxFriction = 0;
        private float pMinLife = 20;
        private float pMaxLife = 20;
        private int pMinSpawn = 1;
        private int pMaxSpawn = 1;

        public void Update()
        {
            
        }

        public void SpawnParticles()
        {
            
        }
    }
}
