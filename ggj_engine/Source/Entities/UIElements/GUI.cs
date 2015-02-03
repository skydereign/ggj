using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.UIElements
{
    abstract class GUI : Entity
    {
        public enum Anchor { None, Right, Left }
        protected Anchor anchor;
        protected string label;

        public GUI(string label)
        {
            this.label = label;

            if(label.Count() > 0)
            {
                this.label += ": ";
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(anchor == Anchor.Right)
            {
                Position.X = MyScreen.Camera.ScreenToWorld(new Vector2(Globals.ScreenWidth - Right(), 0)).X;
            }
            base.Update(gameTime);
        }

        public abstract float Top();
        public abstract float Bot();
        public abstract float Right();
        public abstract float Left();

        public virtual void ResetPositions()
        {
            //
        }
    }
}
