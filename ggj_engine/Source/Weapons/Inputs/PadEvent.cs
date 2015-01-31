using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons.Inputs
{
    class PadEvent : InputEvent
    {
        public enum Types { Pressed, Held, Released, Standby };

        private Buttons button;
        private Types type;
        private int controller;

        public PadEvent(Buttons button, Types type, int controller)
        {
            this.button = button;
            this.type = type;
            this.controller = controller;
        }

        public override bool Check()
        {
            switch (type)
            {
                case Types.Pressed:
                    if (InputControl.GetGamePadButtonPressed(controller, button))
                    {
                        return true;
                    }
                    break;

                case Types.Held:
                    if (InputControl.GetGamePadButtonHeld(controller, button))
                    {
                        return true;
                    }
                    break;

                case Types.Released:
                    if (InputControl.GetGamePadButtonReleased(controller, button))
                    {
                        return true;
                    }
                    break;

                case Types.Standby:
                    if (!InputControl.GetGamePadButtonPressed(controller, button) && !InputControl.GetGamePadButtonReleased(controller, button) && !InputControl.GetGamePadButtonHeld(controller, button))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}
