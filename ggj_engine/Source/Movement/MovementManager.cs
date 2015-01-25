using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Movement
{
    class MovementManager
    {
        private readonly float defaultSpeed = 5.0f;

        public enum MovementTypes {Standard, Thrusters, ClickToMove};

        List<MovementDelegate> movementInputs;

        private float moveSpeed;
        private Vector2 velocityVector;

        private MovementTypes movementType;

        public MovementManager()
        {
            movementInputs = new List<MovementDelegate>();

            moveSpeed = defaultSpeed;
            movementType = MovementTypes.Standard;
            velocityVector = Vector2.Zero;

            // standard movement
            movementInputs.Add(new MovementKeyInput(MovementKeyInput.Types.Held, Keys.W, moveUp));
            movementInputs.Add(new MovementKeyInput(MovementKeyInput.Types.Held, Keys.A, moveLeft));
            movementInputs.Add(new MovementKeyInput(MovementKeyInput.Types.Held, Keys.S, moveDown));
            movementInputs.Add(new MovementKeyInput(MovementKeyInput.Types.Held, Keys.D, moveRight));

            movementInputs.Add(new MovementMouseInput(MovementMouseInput.Types.Held, MovementMouseInput.Button.Left, moveTowardsMouse));
            movementInputs.Add(new MovementMouseInput(MovementMouseInput.Types.Pressed, MovementMouseInput.Button.Right, resetPosition));
        }

        public Vector2 Update(GameTime gameTime, Vector2 currPosition, Vector2 mousePosition)
        {
            Vector2 newPosition = currPosition;

            switch (movementType)
            {
                case MovementTypes.Standard:
                    foreach (MovementDelegate movementInput in movementInputs)
                    {
                        newPosition += movementInput.Update(gameTime, currPosition, mousePosition);
                    }
                    break;
                case MovementTypes.Thrusters:
                    newPosition += (velocityVector * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case MovementTypes.ClickToMove:
                    break;
            }

            return newPosition;
        }

        public MovementTypes GetMovementType()
        {
            return movementType;
        }

        // types of movement
        private Vector2 moveUp(Vector2 currPosition, Vector2 mousePosition)
        {
            return new Vector2(0.0f, -moveSpeed);
        }

        private Vector2 moveDown(Vector2 currPosition, Vector2 mousePosition)
        {
            return new Vector2(0.0f, moveSpeed);
        }

        private Vector2 moveLeft(Vector2 currPosition, Vector2 mousePosition)
        {
            return new Vector2(-moveSpeed, 0.0f);
        }

        private Vector2 moveRight(Vector2 currPosition, Vector2 mousePosition)
        {
            return new Vector2(moveSpeed, 0.0f);
        }

        private Vector2 teleport(Vector2 currPostion, Vector2 mousePosition)
        {
            return mousePosition - currPostion;
        }

        private Vector2 moveTowardsMouse(Vector2 currPosition, Vector2 mousePosition)
        {
            float angle = (float)Math.Atan2(mousePosition.Y - currPosition.Y, mousePosition.X - currPosition.X);

            Vector2 moveVector = new Vector2((float)(moveSpeed * Math.Cos(angle)), (float)(moveSpeed * Math.Sin(angle)));

            return moveVector;
        }

        private Vector2 thrusterUp(Vector2 currPosition, Vector2 mousePosition)
        {
            velocityVector = new Vector2(0.0f, -moveSpeed);
            return currPosition;
        }

        private Vector2 thrusterDecel(Vector2 currPosition, Vector2 mousePosition)
        {
            velocityVector = Vector2.Zero;
            return currPosition;
        }

        private Vector2 resetPosition(Vector2 currPosition, Vector2 mousePosition)
        {
            Console.WriteLine("Reset position to 0,0");
            return -currPosition;
        }


    }
}
