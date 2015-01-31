using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons.Inputs
{
    class EventTrigger
    {
        // delegate for callback
        public delegate void Trigger();
        private Trigger trigger;
        public enum Type { Sequential, All, Any };

        Type type;
        List<InputEvent> events;

        public EventTrigger(Trigger trigger, Type type, params InputEvent[] events)
        {
            this.trigger = trigger;
            this.type = type;
            this.events = new List<InputEvent>();

            foreach(InputEvent e in events)
            {
                this.events.Add(e);
            }
        }

        public void Update(GameTime gameTime)
        {
            switch(type)
            {
                case Type.All:
                    // loop through, if all events return true then trigger delegate
                    int count = 0;

                    foreach(InputEvent e in events)
                    {
                        if(e.Check())
                        {
                            count++;
                        }
                    }

                    if (count == events.Count)
                    {
                        trigger();
                    }
                    break;
            }
        }
    }
}
