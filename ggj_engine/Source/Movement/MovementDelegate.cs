using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Movement
{
    abstract class MovementDelegate
    {
        public delegate Vector2 Movement(Vector2 currPosition, Vector2 mousePosition);

        protected Movement movement;

        public abstract Vector2 Update(GameTime gameTime, Vector2 currPosition, Vector2 mousePosition);

    }
}
