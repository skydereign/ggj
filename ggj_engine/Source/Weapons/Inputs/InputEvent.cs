using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons.Inputs
{
    /// <summary>
    /// Event class that returns true in a given case
    /// </summary>
    abstract class InputEvent
    {
        abstract public bool Check();
    }
}
