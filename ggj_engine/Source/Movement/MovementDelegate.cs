using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Movement
{
    abstract class MovementDelegate
    {
        public enum Types { Pressed, Held, Released, Count };

        public delegate Vector2 Movement(Vector2 currPosition, Vector2 mousePosition);

        protected Movement movement;

        public abstract void SetType(Types type);

        public abstract void SetMovements(params Movement[] movements);

        public abstract Vector2 Update(GameTime gameTime, Vector2 currPosition, Vector2 mousePosition);

    }
}
