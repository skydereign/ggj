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

        private List<Trigger> triggers;

        public EquipKeyInput(Types type, Keys key, params Trigger[] triggerList)
        {
            this.type = type;
            this.key = key;
            triggers = new List<Trigger>();
            if (triggerList != null)
            {
                foreach (Trigger myTrigger in triggerList)
                {
                    triggers.Add(myTrigger);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            switch (type)
            {
                case Types.Pressed:
                    if (InputControl.GetKeyboardKeyPressed(key))
                    {
                        foreach (Trigger myTrigger in triggers)
                        {
                            myTrigger();
                        }
                    }
                    break;

                case Types.Held:
                    if (InputControl.GetKeyboardKeyHeld(key))
                    {
                        foreach (Trigger myTrigger in triggers)
                        {
                            myTrigger();
                        }
                    }
                    break;

                case Types.Released:
                    if (InputControl.GetKeyboardKeyReleased(key))
                    {
                        foreach (Trigger myTrigger in triggers)
                        {
                            myTrigger();
                        }
                    }
                    break;

                case Types.Standby:
                    if (!InputControl.GetKeyboardKeyPressed(key) && !InputControl.GetKeyboardKeyReleased(key) && !InputControl.GetKeyboardKeyHeld(key))
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
