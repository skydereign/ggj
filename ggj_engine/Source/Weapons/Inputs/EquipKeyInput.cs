using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons.Inputs
{
    class EquipKeyInput : EquipInput
    {
        public enum Types { Pressed, Held, Released, Standby};
        public Keys key;

        private Types type;

        public EquipKeyInput(Types type, Keys key, Trigger trigger)
        {
            this.type = type;
            this.key = key;
            this.trigger = trigger;
        }

        public override void Update(GameTime gameTime)
        {
            switch (type)
            {
                case Types.Pressed:
                    if (InputControl.GetKeyboardKeyPressed(key))
                    {
                        trigger();
                    }
                    break;

                case Types.Held:
                    if (InputControl.GetKeyboardKeyHeld(key))
                    {
                        trigger();
                    }
                    break;

                case Types.Released:
                    if (InputControl.GetKeyboardKeyReleased(key))
                    {
                        trigger();
                    }
                    break;

                case Types.Standby:
                    if (!InputControl.GetKeyboardKeyPressed(key) && !InputControl.GetKeyboardKeyReleased(key) && !InputControl.GetKeyboardKeyHeld(key))
                    {
                        trigger();
                    }
                    break;
            }
        }
    }
}
