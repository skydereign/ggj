using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Movement
{
    abstract class MovementDelegate
    {
        public abstract Vector2 Update(GameTime gameTime, Vector2 currPosition);

    }
}
