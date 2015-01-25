using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Movement
{
    class MovementKeyInput : MovementDelegate
    {
        public enum Types { Pressed, Held, Released, Standby, Count };

        public Keys key;

        private Types type;

        List<Movement> movementList;

        public MovementKeyInput(Types type, Keys key, params Movement[] movements)
        {
            this.type = type;
            this.key = key;

            movementList = new List<Movement>();
            if (movements != null)
            {
                foreach (Movement move in movements)
                {
                    movementList.Add(move);
                }
            }
        }

        public override Vector2 Update(GameTime gametime, Vector2 currPosition, Vector2 mousePosition)
        {
            Boolean wasInputCorrect = false;

            switch (type)
            {
                case Types.Pressed:
                    if (InputControl.GetKeyboardKeyPressed(key))
                    {
                        wasInputCorrect = true;
                    }
                    break;

                case Types.Held:
                    if (InputControl.GetKeyboardKeyHeld(key))
                    {
                        wasInputCorrect = true;
                    }
                    break;

                case Types.Released:
                    if (InputControl.GetKeyboardKeyReleased(key))
                    {
                        wasInputCorrect = true;
                    }
                    break;

                case Types.Standby:
                    if (!InputControl.GetKeyboardKeyPressed(key) && !InputControl.GetKeyboardKeyReleased(key) && !InputControl.GetKeyboardKeyHeld(key))
                    {
                        wasInputCorrect = true;
                    }
                    break;
            }

            if (wasInputCorrect)
            {
                Vector2 moveVector = Vector2.Zero;

                foreach (Movement move in movementList)
                {
                    moveVector += move(currPosition, mousePosition);
                }

                return moveVector;
            }

            return Vector2.Zero;
        }
    }
}
