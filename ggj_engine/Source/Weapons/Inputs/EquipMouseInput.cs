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

        private List<Trigger> triggers;

        public EquipMouseInput(Types type, Button button, params Trigger[] triggerList)
        {
            this.type = type;
            this.button = button;
            triggers = new List<Trigger>();
            if (triggerList != null)
            {
                foreach (Trigger myTrigger in triggerList)
                {
                    Console.WriteLine("Added trigger " + myTrigger);
                    triggers.Add(myTrigger);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            switch(type)
            {
                case Types.Pressed:
                    if((button == Button.Left && InputControl.GetMouseOnLeftPressed()) ||
                        button == Button.Right && InputControl.GetMouseOnRightPressed())
                    {
                        foreach (Trigger myTrigger in triggers)
                        {
                            myTrigger();
                        }
                    }
                    break;

                case Types.Held:
                    if ((button == Button.Left && InputControl.GetMouseOnLeftHeld()) ||
                        button == Button.Right && InputControl.GetMouseOnRightHeld())
                    {
                        foreach (Trigger myTrigger in triggers)
                        {
                            myTrigger();
                        }
                    }
                    break;

                case Types.Released:
                    if ((button == Button.Left && InputControl.GetMouseOnLeftReleased()) ||
                        button == Button.Right && InputControl.GetMouseOnRightReleased())
                    {
                        foreach (Trigger myTrigger in triggers)
                        {
                            myTrigger();
                        }
                    }
                    break;

                case Types.Standby:
                    if((button == Button.Left && !InputControl.GetMouseOnLeftReleased() && !InputControl.GetMouseOnLeftHeld() && !InputControl.GetMouseOnLeftReleased()) ||
                        (button == Button.Right && !InputControl.GetMouseOnRightPressed() && !InputControl.GetMouseOnRightHeld() && !InputControl.GetMouseOnRightReleased()))
                    {
                        foreach (Trigger myTrigger in triggers)
                        {
                            myTrigger();
                        }
                    }
                    break;
            }
        }
    }
}
