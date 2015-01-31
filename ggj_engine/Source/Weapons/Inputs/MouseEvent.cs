using ggj_engine.Source.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons.Inputs
{
    class MouseEvent : InputEvent
    {
        public enum Types { Pressed, Held, Released, Standby };
        public enum Button { Left, Right };

        private Button button;
        private Types type;

        public MouseEvent(Button button, Types type)
        {
            this.button = button;
            this.type = type;
        }

        public override bool Check()
        {
            switch (type)
            {
                case Types.Pressed:
                    if ((button == Button.Left && InputControl.GetMouseOnLeftPressed()) ||
                        button == Button.Right && InputControl.GetMouseOnRightPressed())
                    {
                        return true;
                    }
                    break;

                case Types.Held:
                    if ((button == Button.Left && InputControl.GetMouseOnLeftHeld()) ||
                        button == Button.Right && InputControl.GetMouseOnRightHeld())
                    {
                        return true;
                    }
                    break;

                case Types.Released:
                    if ((button == Button.Left && InputControl.GetMouseOnLeftReleased()) ||
                        button == Button.Right && InputControl.GetMouseOnRightReleased())
                    {
                        return true;
                    }
                    break;

                case Types.Standby:
                    if ((button == Button.Left && !InputControl.GetMouseOnLeftReleased() && !InputControl.GetMouseOnLeftHeld() && !InputControl.GetMouseOnLeftReleased()) ||
                        (button == Button.Right && !InputControl.GetMouseOnRightPressed() && !InputControl.GetMouseOnRightHeld() && !InputControl.GetMouseOnRightReleased()))
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}
