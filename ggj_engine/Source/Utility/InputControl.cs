using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ggj_engine.Source.Utility
{
    static class InputControl
    {
        static bool leftOnce = false;
        static bool rightOnce = false;
        static bool middleOnce = false;
        static bool leftLast = false;
        static bool rightLast = false;
        static bool middleLast = false;
        static bool leftHeld = false;
        static bool rightHeld = false;
        static bool middleHeld = false;
        static bool leftReleased = false;
        static bool middleReleased = false;
        static bool rightReleased = false;
        static int wheelCurrent;
        static int wheelLast;
        static public KeyboardState KeyboardStateLast;

        static Vector2 mousePosition;
        static Vector2 mousePrevPosition;
        static Vector2 mouseLeftClickPosition;

        /// <summary>
        /// true when the left mouse button is first clicked (will not continue to return true when held)
        /// </summary>
        /// <returns></returns>
        public static bool GetMouseOnLeftPressed()
        {
            return (leftOnce);
        }

        /// <summary>
        /// true when the right mouse button is first clicked (will not continue to return true when held)
        /// </summary>
        /// <returns></returns>
        public static bool GetMouseOnRightPressed()
        {
            return (rightOnce);
        }

        /// <summary>
        /// true when the middle mouse button is first clicked (will not continue to return true when held)
        /// </summary>
        /// <returns></returns>
        public static bool GetMouseOnMiddlePressed()
        {
            return (middleOnce);
        }

        /// <summary>
        /// true when the left mouse button is held down
        /// </summary>
        /// <returns></returns>
        public static bool GetMouseOnLeftHeld()
        {
            return (leftHeld);
        }

        /// <summary>
        /// true when the right mouse button is held down
        /// </summary>
        /// <returns></returns>
        public static bool GetMouseOnRightHeld()
        {
            return (rightHeld);
        }

        /// <summary>
        /// true when the middle mouse button is held down
        /// </summary>
        /// <returns></returns>
        public static bool GetMouseOnMiddleHeld()
        {
            return (middleHeld);
        }

        /// <summary>
        /// true when the left mouse button is released (single frame)
        /// </summary>
        /// <returns></returns>
        public static bool GetMouseOnLeftReleased()
        {
            return (leftReleased);
        }

        /// <summary>
        /// true when the middle mouse button is released (single frame)
        /// </summary>
        /// <returns></returns>
        public static bool GetMouseOnMiddleReleased()
        {
            return (middleReleased);
        }

        /// <summary>
        /// true when the right mouse button is released (single frame)
        /// </summary>
        /// <returns></returns>
        public static bool GetMouseOnRightReleased()
        {
            return (rightReleased);
        }

        /// <summary>
        /// true when mouse wheel has scrolled up
        /// </summary>
        /// <returns></returns>
        public static bool GetMouseWheelUp()
        {
            return (wheelLast > wheelCurrent);
        }

        /// <summary>
        /// true when mouse wheel has scrolled down
        /// </summary>
        /// <returns></returns>
        public static bool GetMouseWheelDown()
        {
            return (wheelLast < wheelCurrent);
        }

        /// <summary>
        /// Returns the current mouse position
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetMousePosition()
        {
            //Viewport offset is from forsakens camera code. This might need to be changed 
            return mousePosition/* - Game1.ViewportOffset*/;
        }

        /// <summary>
        /// Returns the last frames mouse position
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetMousePrevPosition()
        {
            return mousePrevPosition;
        }

        /// <summary>
        /// Returns the mouse position of the last click
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetMouseLeftClickPosition()
        {
            return mouseLeftClickPosition;
        }

        /// <summary>
        /// true if Key is pressed the first time (does not continue to return true when held)
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static bool GetKeyboardKeyPressed(Keys k)
        {
            if (KeyboardStateLast.IsKeyDown(k) == true)
                return false;
            else if (Keyboard.GetState().IsKeyDown(k))
                return true;
            else
                return false;
        }

        /// <summary>
        /// true if Key is held
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static bool GetKeyboardKeyHeld(Keys k)
        {
            if (Keyboard.GetState().IsKeyDown(k))
                return true;
            else
                return false;
        }

        /// <summary>
        /// True if key was released this tick
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static bool GetKeyboardKeyReleased(Keys k)
        {
            return (KeyboardStateLast.IsKeyDown(k) && Keyboard.GetState().IsKeyUp(k));
        }

        /// <summary>
        /// Updates the InputController
        /// </summary>
        /// <param name="screenPositionFactor">Internal render size to window size ratio</param>
        public static void Update(float screenPositionFactor)
        {
            mousePrevPosition = mousePosition;
            mousePosition.X = Mouse.GetState().X * screenPositionFactor;
            mousePosition.Y = Mouse.GetState().Y * screenPositionFactor;

            leftOnce = false;
            rightOnce = false;
            middleOnce = false;

            leftReleased = false;
            rightReleased = false;
            middleReleased = false;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                leftHeld = true;
                if (leftLast == false)
                {
                    mouseLeftClickPosition = mousePosition;
                    leftOnce = true;
                }
            }
            else
            {
                if (leftHeld == true)
                {
                    leftReleased = true;
                }
                leftHeld = false;
                leftOnce = false;
            }
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                rightHeld = true;
                if (rightLast == false)
                    rightOnce = true;
            }
            else
            {
                if (rightHeld == true)
                {
                    rightReleased = true;
                }
                rightHeld = false;
                rightOnce = false;
            }
            if (Mouse.GetState().MiddleButton == ButtonState.Pressed)
            {
                middleHeld = true;
                if (middleLast == false)
                    middleOnce = true;
            }
            else
            {
                if (middleHeld == true)
                {
                    middleReleased = true;
                }
                middleHeld = false;
                middleOnce = false;
            }

            leftLast = leftHeld;
            rightLast = rightHeld;
            middleLast = middleHeld;
            KeyboardStateLast = Keyboard.GetState();

            wheelLast = wheelCurrent;
            wheelCurrent = Mouse.GetState().ScrollWheelValue;
        }
    }
}

