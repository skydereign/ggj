using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Movement
{
    class MovementMouseInput : MovementDelegate
    {
        public enum Button { Left, Right, Count };

        private Types type;
        private Button button;

        List<Movement> movements;

        public MovementMouseInput(Button button)
        {
            this.type = Types.Held;
            this.button = button;

            movements = new List<Movement>();
        }

        public MovementMouseInput(Types type, Button button, params Movement[] movementList)
        {
            this.type = type;
            this.button = button;

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

        public override Vector2 Update(GameTime gameTime, Vector2 currPosition, Vector2 mousePosition)
        {
            Boolean wasInputCorrect = false;

            switch (type)
            {
                case Types.Pressed:
                    if ((button == Button.Left && InputControl.GetMouseOnLeftPressed()) ||
                        button == Button.Right && InputControl.GetMouseOnRightPressed())
                    {
                        wasInputCorrect = true;
                    }
                    break;

                case Types.Held:
                    if ((button == Button.Left && InputControl.GetMouseOnLeftHeld()) ||
                        button == Button.Right && InputControl.GetMouseOnRightHeld())
                    {
                        wasInputCorrect = true;
                    }
                    break;

                case Types.Released:
                    if ((button == Button.Left && InputControl.GetMouseOnLeftReleased()) ||
                        button == Button.Right && InputControl.GetMouseOnRightReleased())
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
