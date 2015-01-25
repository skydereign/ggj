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

        public enum InputType { Keyboard, Mouse, Count };
        public enum MovementTypes {Standard, Thrusters, Mouse, Count};
        // public enum 

        List<MovementDelegate> movementInputs;

        private float moveSpeed;
        private Vector2 velocityVector;
        private Vector2 locationVector;

        private MovementTypes movementType;

        public MovementManager()
        {
            movementInputs = new List<MovementDelegate>();

            moveSpeed = defaultSpeed;
            movementType = MovementTypes.Mouse;
            velocityVector = Vector2.Zero;

            // choose what inputs to use and what they will do


            // generate movement inputs
            generateMovementInputs();
        }

        private void generateMovementTypes()
        {
            movementType = (MovementTypes)RandomUtil.Next((double)MovementTypes.Count);

            // choose how input will be made
            MovementKeyInput.Types inputType = (MovementKeyInput.Types)RandomUtil.Next((double)MovementKeyInput.Types.Count);

        }


        private void generateMovementInputs() {
            switch(movementType) 
            {
                case MovementTypes.Standard:
                    movementInputs.Add(new MovementKeyInput(MovementKeyInput.Types.Held, Keys.W, moveUp));
                    movementInputs.Add(new MovementKeyInput(MovementKeyInput.Types.Held, Keys.A, moveLeft));
                    movementInputs.Add(new MovementKeyInput(MovementKeyInput.Types.Held, Keys.S, moveDown));
                    movementInputs.Add(new MovementKeyInput(MovementKeyInput.Types.Held, Keys.D, moveRight));
                    break;
                case MovementTypes.Thrusters:
                    movementInputs.Add(new MovementKeyInput(MovementKeyInput.Types.Held, Keys.Up, invertMovement, thrustTowardsPosition));
                    movementInputs.Add(new MovementKeyInput(MovementKeyInput.Types.Held, Keys.Left, thrusterLeft));
                    movementInputs.Add(new MovementKeyInput(MovementKeyInput.Types.Held, Keys.Down, thrusterDown));
                    movementInputs.Add(new MovementKeyInput(MovementKeyInput.Types.Held, Keys.Right, thrusterRight));
                    break;
                case MovementTypes.Mouse:
                    movementInputs.Add(new MovementMouseInput(MovementMouseInput.Types.Standby, MovementMouseInput.Button.Left, runAwayFromLocation));
                    break;
            }
        }

        public Vector2 Update(GameTime gameTime, Vector2 currPosition, Vector2 mousePosition)
        {
            Vector2 newPosition = currPosition;

            switch (movementType)
            {
                case MovementTypes.Standard:
                    // update player lcoation
                    foreach (MovementDelegate movementInput in movementInputs)
                    {
                        newPosition += movementInput.Update(gameTime, currPosition, mousePosition);
                    }
                    break;
                case MovementTypes.Thrusters:
                    // update velocity vector
                    foreach (MovementDelegate movementInput in movementInputs)
                    {
                        movementInput.Update(gameTime, currPosition, mousePosition);
                    }
                    newPosition += (velocityVector * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case MovementTypes.Mouse:
                    // move player toward saved location
                    foreach (MovementDelegate movementInput in movementInputs)
                    {
                        movementInput.Update(gameTime, currPosition, mousePosition);
                    }

                    float distance = Vector2.Distance(currPosition, locationVector);

                    if (locationVector != Vector2.Zero && distance > 5)
                    {
                        if (distance <= 5 && distance != 0 )
                        {
                            newPosition += (moveTowardsPosition(currPosition, locationVector)) / 5;
                        }
                        else
                        {
                            newPosition += moveTowardsPosition(currPosition, locationVector);
                        }
                        
                    } 
                    break;
            }

            return newPosition;
        }

        public MovementTypes GetMovementType()
        {
            return movementType;
        }

        // types of movement
        private Vector2 invertMovement(Vector2 currPosition, Vector2 mousePosition)
        {
            moveSpeed = -defaultSpeed;
            return currPosition;
        }

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

        private Vector2 moveTowardsPosition(Vector2 currPosition, Vector2 mousePosition)
        {
            float angle = (float)Math.Atan2(mousePosition.Y - currPosition.Y, mousePosition.X - currPosition.X);

            Vector2 moveVector = new Vector2((float)(moveSpeed * Math.Cos(angle)), (float)(moveSpeed * Math.Sin(angle)));

            return moveVector;
        }

        private Vector2 thrustTowardsPosition(Vector2 currPosition, Vector2 mousePosition)
        {
            float angle = (float)Math.Atan2(mousePosition.Y - currPosition.Y, mousePosition.X - currPosition.X);

            velocityVector += new Vector2((float)(moveSpeed/2 * Math.Cos(angle)), (float)(moveSpeed/2 * Math.Sin(angle)));

            return currPosition;
        }

        private Vector2 thrusterUp(Vector2 currPosition, Vector2 mousePosition)
        {
            velocityVector.Y -= moveSpeed / 2;
            return currPosition;
        }

        private Vector2 thrusterLeft(Vector2 currPosition, Vector2 mousePosition)
        {
            velocityVector.X -= moveSpeed / 2;
            return currPosition;
        }

        private Vector2 thrusterDown(Vector2 currPosition, Vector2 mousePosition)
        {
            velocityVector.Y += moveSpeed / 2;
            return currPosition;
        }

        private Vector2 thrusterRight(Vector2 currPosition, Vector2 mousePosition)
        {
            velocityVector.X += moveSpeed / 2;
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

        private Vector2 goToLocation(Vector2 currPosition, Vector2 mousePosition)
        {
            locationVector = mousePosition;
            return currPosition;
        }

        private Vector2 runAwayFromLocation(Vector2 currPosition, Vector2 mousePosition)
        {
            if(Vector2.Distance(currPosition, mousePosition) < 100) {
                locationVector = currPosition - (mousePosition - currPosition);
            }
            return currPosition;
        }


    }
}
