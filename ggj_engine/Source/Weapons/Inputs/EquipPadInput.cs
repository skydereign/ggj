using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons.Inputs
{
    class EquipPadInput : EquipInput
    {
        public enum Types { Pressed, Held, Released, Standby };

        private Buttons button;
        private Types type;
        private int controller;
        
        public EquipPadInput(Types type, Buttons button, int controller, Trigger trigger) : base(trigger)
        {
            this.type = type;
            this.button = button;
            this.controller = controller;
        }

        public override void Update(GameTime gameTime)
        {
            switch (type)
            {
                case Types.Pressed:
                    if (InputControl.GetGamePadButtonPressed(controller, button))
                    {
                        trigger();
                    }
                    break;

                case Types.Held:
                    if (InputControl.GetGamePadButtonHeld(controller, button))
                    {
                        trigger();
                    }
                    break;

                case Types.Released:
                    if (InputControl.GetGamePadButtonReleased(controller, button))
                    {
                        trigger();
                    }
                    break;

                case Types.Standby:
                    if (!InputControl.GetGamePadButtonPressed(controller, button) && !InputControl.GetGamePadButtonReleased(controller, button) && !InputControl.GetGamePadButtonHeld(controller, button))
                    {
                        trigger();
                    }
                    break;
            }
        }
    }
}
