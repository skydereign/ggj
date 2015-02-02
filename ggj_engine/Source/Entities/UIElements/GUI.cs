using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.UIElements
{
    abstract class GUI : Entity
    {
        protected string label;

        public GUI(string label)
        {
            this.label = label;

            if(label.Count() > 0)
            {
                this.label += ": ";
            }
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
