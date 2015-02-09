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

        public enum MovementTypes {Standard, Thrusters, Mouse, Count};
        public enum KeyInput { WASD, Arrows, Count };
        private enum KeyMovementMotions { Standard, TowardMouse, Diagonal, StandardIvert, TowardMouseInvert, DiagonalInvert, Count};
        private enum MouseMovementMotions { ClickToMove, RunAway, FollowMouse, Count};

        private List<MovementDelegate> movementInputs;

        private float moveSpeed;
        private Vector2 velocityVector;
        private Vector2 locationVector;

        private MovementTypes movementType;
        private MovementDelegate.Types inputType;

        public MovementManager()
        {
            movementInputs = new List<MovementDelegate>();

            // default values
            moveSpeed = defaultSpeed;
            movementType = MovementTypes.Standard;
            inputType = MovementDelegate.Types.Held;
            velocityVector = Vector2.Zero;

            // generate standard movement inputs
            generateStandardMovementInputs();
        }

        public void GenerateNewMovement()
        {
            movementInputs.Clear();
            moveSpeed = defaultSpeed;
            velocityVector = Vector2.Zero;
            locationVector = Vector2.Zero;
            generateMovementInputs();
        }

        public MovementTypes GetMoveType()
        {
            return movementType;
        }

        private void generateStandardMovementInputs()
        {
            movementInputs.Add(new MovementKeyInput(inputType, Keys.W, moveUp));
            movementInputs.Add(new MovementKeyInput(inputType, Keys.A, moveLeft));
            movementInputs.Add(new MovementKeyInput(inputType, Keys.S, moveDown));
            movementInputs.Add(new MovementKeyInput(inputType, Keys.D, moveRight));
        }

        private void generateMovementInputs() {
            
            movementType = (MovementTypes)RandomUtil.Next((double)MovementTypes.Count);

            // add either keyvboard or mouse input
            KeyInput keyInput = KeyInput.Count;
            MovementMouseInput.Button buttonInput = MovementMouseInput.Button.Count;
            switch(movementType) 
            {
                case MovementTypes.Standard:
                case MovementTypes.Thrusters:
                    keyInput = (KeyInput)RandomUtil.Next((double)KeyInput.Count);
                    switch (keyInput)
                    {
                        case KeyInput.WASD:
                            movementInputs.Add(new MovementKeyInput(Keys.W));
                            movementInputs.Add(new MovementKeyInput(Keys.A));
                            movementInputs.Add(new MovementKeyInput(Keys.S));
                            movementInputs.Add(new MovementKeyInput(Keys.D));
                            break;
                        case KeyInput.Arrows:
                            movementInputs.Add(new MovementKeyInput(Keys.Up));
                            movementInputs.Add(new MovementKeyInput(Keys.Left));
                            movementInputs.Add(new MovementKeyInput(Keys.Down));
                            movementInputs.Add(new MovementKeyInput(Keys.Right));
                            break;
                    }
                    break;
                case MovementTypes.Mouse:
                    buttonInput = (MovementMouseInput.Button)RandomUtil.Next((double)MovementMouseInput.Button.Count);
                    movementInputs.Add(new MovementMouseInput(buttonInput));    
                    break;
            }

            // choose which type input
            inputType = (MovementDelegate.Types)RandomUtil.Next((double)MovementDelegate.Types.Count);
            foreach(MovementDelegate movementInput in movementInputs) 
            {
                movementInput.SetType(inputType);
            }

            // choose movement motions
            KeyMovementMotions keyMotion = (KeyMovementMotions)RandomUtil.Next((double)KeyMovementMotions.Count);
            MouseMovementMotions mouseMotion = (MouseMovementMotions)RandomUtil.Next((double)MouseMovementMotions.Count);

            // choose which movements to use, this sh*t gonna be super long
            MovementDelegate[] movementInputsArray = movementInputs.ToArray();
            switch (movementType)
            {
                case MovementTypes.Standard:
                    switch (keyMotion)
                    {
                        case KeyMovementMotions.Standard:
                            movementInputsArray[0].SetMovements(moveUp);
                            movementInputsArray[1].SetMovements(moveLeft);
                            movementInputsArray[2].SetMovements(moveDown);
                            movementInputsArray[3].SetMovements(moveRight);
                            break;
                        case KeyMovementMotions.TowardMouse:
                            movementInputsArray[0].SetMovements(moveTowardsPosition);
                            movementInputsArray[1].SetMovements(moveTowardsPosition);
                            movementInputsArray[2].SetMovements(moveTowardsPosition);
                            movementInputsArray[3].SetMovements(moveTowardsPosition);
                            break;
                        case KeyMovementMotions.Diagonal:
                            movementInputsArray[0].SetMovements(moveUp, moveLeft);
                            movementInputsArray[1].SetMovements(moveLeft, moveDown);
                            movementInputsArray[2].SetMovements(moveDown, moveRight);
                            movementInputsArray[3].SetMovements(moveRight, moveUp);
                            break;
                        case KeyMovementMotions.StandardIvert:
                            movementInputsArray[0].SetMovements(invertMovement, moveUp);
                            movementInputsArray[1].SetMovements(moveLeft);
                            movementInputsArray[2].SetMovements(moveDown);
                            movementInputsArray[3].SetMovements(moveRight);
                            break;
                        case KeyMovementMotions.TowardMouseInvert:
                            movementInputsArray[0].SetMovements(invertMovement, moveTowardsPosition);
                            movementInputsArray[1].SetMovements(moveTowardsPosition);
                            movementInputsArray[2].SetMovements(moveTowardsPosition);
                            movementInputsArray[3].SetMovements(moveTowardsPosition);
                            break;
                        case KeyMovementMotions.DiagonalInvert:
                            movementInputsArray[0].SetMovements(invertMovement, moveUp, moveLeft);
                            movementInputsArray[1].SetMovements(moveLeft, moveDown);
                            movementInputsArray[2].SetMovements(moveDown, moveRight);
                            movementInputsArray[3].SetMovements(moveRight, moveUp);
                            break;

                    }
                     Console.WriteLine("Generated: " + movementType + " motions " + keyMotion + " input " + inputType + " keyInput " + keyInput);
                    break;
                case MovementTypes.Thrusters:
                    switch (keyMotion)
                    {
                        case KeyMovementMotions.Standard:
                            movementInputsArray[0].SetMovements(thrusterUp);
                            movementInputsArray[1].SetMovements(thrusterLeft);
                            movementInputsArray[2].SetMovements(thrusterDown);
                            movementInputsArray[3].SetMovements(thrusterRight);
                            break;
                        case KeyMovementMotions.TowardMouse:
                            movementInputsArray[0].SetMovements(thrustTowardsPosition);
                            movementInputsArray[1].SetMovements(thrustTowardsPosition);
                            movementInputsArray[2].SetMovements(thrustTowardsPosition);
                            movementInputsArray[3].SetMovements(thrustTowardsPosition);
                            break;
                        case KeyMovementMotions.Diagonal:
                            movementInputsArray[0].SetMovements(thrusterUp, thrusterLeft);
                            movementInputsArray[1].SetMovements(thrusterLeft, thrusterDown);
                            movementInputsArray[2].SetMovements(thrusterDown, thrusterRight);
                            movementInputsArray[3].SetMovements(thrusterRight, thrusterUp);
                            break;
                        case KeyMovementMotions.StandardIvert:
                            movementInputsArray[0].SetMovements(invertMovement, thrusterUp);
                            movementInputsArray[1].SetMovements(thrusterLeft);
                            movementInputsArray[2].SetMovements(thrusterDown);
                            movementInputsArray[3].SetMovements(thrusterRight);
                            break;
                        case KeyMovementMotions.TowardMouseInvert:
                            movementInputsArray[0].SetMovements(invertMovement, thrustTowardsPosition);
                            movementInputsArray[1].SetMovements(thrustTowardsPosition);
                            movementInputsArray[2].SetMovements(thrustTowardsPosition);
                            movementInputsArray[3].SetMovements(thrustTowardsPosition);
                            break;
                        case KeyMovementMotions.DiagonalInvert:
                            movementInputsArray[0].SetMovements(invertMovement, thrusterUp, thrusterLeft);
                            movementInputsArray[1].SetMovements(thrusterLeft, thrusterDown);
                            movementInputsArray[2].SetMovements(thrusterDown, thrusterRight);
                            movementInputsArray[3].SetMovements(thrusterRight, thrusterUp);
                            break;
                    }
                     Console.WriteLine("Generated: " + movementType + " motions " + keyMotion + " input " + inputType + " keyInput " + keyInput);
                    break;
                case MovementTypes.Mouse:
                    switch (mouseMotion)
                    {
                        case MouseMovementMotions.ClickToMove:
                        case MouseMovementMotions.FollowMouse:
                            movementInputsArray[0].SetMovements(goToLocation);
                            break;
                        case MouseMovementMotions.RunAway:
                            movementInputsArray[0].SetMovements(runAwayFromLocation);
                            break;
                        

                    }
                     Console.WriteLine("Generated: " + movementType + " motions " + mouseMotion + " input " + inputType + " buttonInput " + buttonInput);
                    break;
            }

            

            // clear old movement inputs and add new ones that have actions
            movementInputs.Clear();

            foreach (MovementDelegate movementInput in movementInputsArray)
            {
                movementInputs.Add(movementInput);
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

        public void StopVelocity()
        {
            velocityVector = Vector2.Zero;
        }

        // types of movement
        private Vector2 invertMovement(Vector2 currPosition, Vector2 mousePosition)
        {
            moveSpeed = -defaultSpeed;
            return Vector2.Zero;
        }

        private Vector2 moveUp(Vector2 currPosition, Vector2 mousePosition)
        {
            if (inputType == MovementDelegate.Types.Pressed || inputType == MovementDelegate.Types.Released)
            {
                return new Vector2(0.0f, -Globals.TileSize);
            }
            return new Vector2(0.0f, -moveSpeed);
        }

        private Vector2 moveDown(Vector2 currPosition, Vector2 mousePosition)
        {
            if (inputType == MovementDelegate.Types.Pressed || inputType == MovementDelegate.Types.Released)
            {
                return new Vector2(0.0f, Globals.TileSize);
            }
            return new Vector2(0.0f, moveSpeed);
        }

        private Vector2 moveLeft(Vector2 currPosition, Vector2 mousePosition)
        {
            if (inputType == MovementDelegate.Types.Pressed || inputType == MovementDelegate.Types.Released)
            {
                return new Vector2(-Globals.TileSize, 0.0f);
            }
            return new Vector2(-moveSpeed, 0.0f);
        }

        private Vector2 moveRight(Vector2 currPosition, Vector2 mousePosition)
        {
            if (inputType == MovementDelegate.Types.Pressed || inputType == MovementDelegate.Types.Released)
            {
                return new Vector2(Globals.TileSize, 0.0f);
            }
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

            if (inputType == MovementDelegate.Types.Pressed || inputType == MovementDelegate.Types.Released)
            {
                velocityVector += new Vector2((float)(moveSpeed*2 * Math.Cos(angle)), (float)(moveSpeed*2 * Math.Sin(angle)));
            }
            else
            {
                velocityVector += new Vector2((float)(moveSpeed/2 * Math.Cos(angle)), (float)(moveSpeed/2 * Math.Sin(angle)));
            }

            if (velocityVector.X > Globals.MaxVelocity)
            {
                velocityVector.X = Globals.MaxVelocity;
            }
            else if (velocityVector.X < -Globals.MaxVelocity)
            {
                velocityVector.X = -Globals.MaxVelocity;
            }

            if (velocityVector.Y > Globals.MaxVelocity)
            {
                velocityVector.Y = Globals.MaxVelocity;
            }
            else if (velocityVector.Y < -Globals.MaxVelocity)
            {
                velocityVector.Y = -Globals.MaxVelocity;
            }

            return currPosition;
        }

        private Vector2 thrusterUp(Vector2 currPosition, Vector2 mousePosition)
        {
            if (velocityVector.Y > -Globals.MaxVelocity)
            {
                if (inputType == MovementDelegate.Types.Pressed || inputType == MovementDelegate.Types.Released)
                {
                    velocityVector.Y -= moveSpeed * 2;
                }
                else
                {
                    velocityVector.Y -= moveSpeed;
                }
            }
            return currPosition;
        }

        private Vector2 thrusterLeft(Vector2 currPosition, Vector2 mousePosition)
        {
            if (velocityVector.X > -Globals.MaxVelocity)
            {
                if (inputType == MovementDelegate.Types.Pressed || inputType == MovementDelegate.Types.Released)
                {
                    velocityVector.X -= moveSpeed * 2;
                }
                else
                {
                    velocityVector.X -= moveSpeed;
                }
            }
            return currPosition;
        }

        private Vector2 thrusterDown(Vector2 currPosition, Vector2 mousePosition)
        {
            if (velocityVector.Y < Globals.MaxVelocity)
            {
                if (inputType == MovementDelegate.Types.Pressed || inputType == MovementDelegate.Types.Released)
                {
                    velocityVector.Y += moveSpeed * 2;
                }
                else
                {
                    velocityVector.Y += moveSpeed;
                }
            }
            return currPosition;
        }

        private Vector2 thrusterRight(Vector2 currPosition, Vector2 mousePosition)
        {
            if (velocityVector.X < Globals.MaxVelocity)
            {
                if (inputType == MovementDelegate.Types.Pressed || inputType == MovementDelegate.Types.Released)
                {
                    velocityVector.X += moveSpeed * 2;
                }
                else
                {
                    velocityVector.X += moveSpeed;
                }
            }
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
            if(Vector2.Distance(currPosition, mousePosition) < 200) {
                locationVector = currPosition - (mousePosition - currPosition);
            }
            return currPosition;
        }

    }
}
