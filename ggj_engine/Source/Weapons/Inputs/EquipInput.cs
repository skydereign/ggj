using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons.Inputs
{
    abstract class EquipInput
    {
        // delegate for callback
        public delegate void Trigger();

        protected Trigger trigger;

        // update
        public abstract void Update(GameTime gameTime);
    }
}
