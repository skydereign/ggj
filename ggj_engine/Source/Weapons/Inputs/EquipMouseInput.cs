using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons.Inputs
{
    class EquipMouseInput : EquipInput
    {
        public enum Types { Pressed, Held, Released, Standby};
        public enum Button { Left, Right };

        private Types type;
        private Button button;

        public EquipMouseInput(Types type, Button button, Trigger trigger)
        {
            this.type = type;
            this.button = button;
            this.trigger = trigger;
        }

        public override void Update(GameTime gameTime)
        {
            switch(type)
            {
                case Types.Pressed:
                    if((button == Button.Left && InputControl.GetMouseOnLeftPressed()) ||
                        button == Button.Right && InputControl.GetMouseOnRightPressed())
                    {
                        trigger();
                    }
                    break;

                case Types.Held:
                    if ((button == Button.Left && InputControl.GetMouseOnLeftHeld()) ||
                        button == Button.Right && InputControl.GetMouseOnRightHeld())
                    {
                        trigger();
                    }
                    break;

                case Types.Released:
                    if ((button == Button.Left && InputControl.GetMouseOnLeftReleased()) ||
                        button == Button.Right && InputControl.GetMouseOnRightReleased())
                    {
                        trigger();
                    }
                    break;

                case Types.Standby:
                    if((button == Button.Left && !InputControl.GetMouseOnLeftReleased() && !InputControl.GetMouseOnLeftHeld() && !InputControl.GetMouseOnLeftReleased()) ||
                        (button == Button.Right && !InputControl.GetMouseOnRightPressed() && !InputControl.GetMouseOnRightHeld() && !InputControl.GetMouseOnRightReleased()))
                    {
                        trigger();
                    }
                    break;
            }
        }
    }
}
