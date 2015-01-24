using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ggj_engine.Source.Particles
{
    abstract class PSystem
    {
        public Vector2 Position;
        private List<Emitter> emitters;

        private void updateEmitters()
        {
            foreach (Emitter e in emitters)
            {
                e.Update();
            }
        }
    }
}
