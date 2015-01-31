using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons.Inputs
{
    class KeyEvent : InputEvent
    {
        public enum Types { Pressed, Held, Released, Standby };

        private Keys key;
        private Types type;

        public KeyEvent(Keys key, Types type)
        {
            this.key = key;
            this.type = type;
        }

        public override bool Check()
        {
            switch (type)
            {
                case Types.Pressed:
                    if (InputControl.GetKeyboardKeyPressed(key))
                    {
                        return true;
                    }
                    break;

                case Types.Held:
                    if (InputControl.GetKeyboardKeyHeld(key))
                    {
                        return true;
                    }
                    break;

                case Types.Released:
                    if (InputControl.GetKeyboardKeyReleased(key))
                    {
                        return true;
                    }
                    break;

                case Types.Standby:
                    if (!InputControl.GetKeyboardKeyPressed(key) && !InputControl.GetKeyboardKeyReleased(key) && !InputControl.GetKeyboardKeyHeld(key))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}
