using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Movement
{
    class MovementManager : MovementDelegate
    {
        public enum MovementInput { Standard, Arrows, Mouse, KeyDash };

        private MovementInput input;

        public MovementManager()
        {
            input = MovementInput.Standard;
        }

        public override Vector2 Update(GameTime gameTime, Vector2 currPosition)
        {
            Vector2 newPosition = currPosition;

            switch (input)
            {
                case MovementInput.Standard:
                    // move up
                    if (InputControl.GetKeyboardKeyHeld(Keys.W))
                    {
                        newPosition.Y -= 1.0f;
                    }

                    // move left
                    if (InputControl.GetKeyboardKeyHeld(Keys.A))
                    {
                        newPosition.X -= 1.0f;
                    }

                    // move down
                    if (InputControl.GetKeyboardKeyHeld(Keys.S))
                    {
                        newPosition.Y += 1.0f;
                    }

                    // move right
                    if (InputControl.GetKeyboardKeyHeld(Keys.D))
                    {
                        newPosition.X += 1.0f;
                    } 
                    break;
            }

            return newPosition;
        }


        
        public void SetMovementInput(MovementInput input)
        {

        }

    }
}
