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
        public Keys key;

        private Types type;

        List<Movement> movements;

        public MovementKeyInput(Keys key)
        {
            this.type = Types.Held;
            this.key = key;

            movements = new List<Movement>();
        }

        public MovementKeyInput(Types type, Keys key, params Movement[] movementList)
        {
            this.type = type;
            this.key = key;

            movements = new List<Movement>();
            if (movementList != null)
            {
                foreach (Movement move in movementList)
                {
                    movements.Add(move);
                }
            }
        }

        public override void SetType(Types type)
        {
            this.type = type;
        }

        public override void SetMovements(params Movement[] movementList)
        {

            if (movementList != null)
            {
                foreach (Movement move in movementList)
                {
                    movements.Add(move);
                }
            }
        }

        public override Vector2 Update(GameTime gametime, Vector2 currPosition, Vector2 mousePosition)
        {
            bool wasInputCorrect = false;

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
            }

            if (wasInputCorrect)
            {
                Vector2 moveVector = Vector2.Zero;

                foreach (Movement move in movements)
                {
                    moveVector += move(currPosition, mousePosition);
                }

                return moveVector;
            }

            return Vector2.Zero;
        }
    }

    
}
