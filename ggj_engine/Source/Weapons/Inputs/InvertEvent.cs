using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons.Inputs
{
    class InvertEvent : InputEvent
    {
        InputEvent self;

        public InvertEvent(InputEvent self)
        {
            this.self = self;
        }

        public override bool Check()
        {
            return !self.Check();
        }
    }
}
