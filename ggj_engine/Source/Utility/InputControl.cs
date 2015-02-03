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
        static public GamePadState[] GamePadStateLast = new GamePadState[Globals.MaxControllers+1];

        static Vector2 mousePosition;
        static Vector2 mousePrevPosition;
        static Vector2 mouseLeftClickPosition;
        static Vector2 mouseRightClickPosition;

        #region Mouse state functions
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
        /// Returns the mouse position of the last left click
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetMouseLeftClickPosition()
        {
            return mouseLeftClickPosition;
        }

        /// <summary>
        /// Returns the mouse position of the last right click
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetMouseRightClickPosition()
        {
            return mouseRightClickPosition;
        }
        #endregion

        #region Keyboard state functions
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
        #endregion

        #region GamePad state functions
        public static bool GetGamePadButtonPressed(int index, Buttons b)
        {
            if (GamePadStateLast[index].IsButtonDown(b) == true)
                return false;
            else if (GamePad.GetState(GetCIndex(index)).IsButtonDown(b))
                return true;
            else
                return false;
        }

        public static bool GetGamePadButtonHeld(int index, Buttons b)
        {
            if (GamePad.GetState(GetCIndex(index)).IsButtonDown(b))
                return true;
            else
                return false;
        }

        public static bool GetGamePadButtonReleased(int index, Buttons b)
        {
            return (GamePadStateLast[index].IsButtonDown(b) && GamePad.GetState(GetCIndex(index)).IsButtonUp(b));
        }


        /// <summary>
        /// Gets the value of the axis
        /// </summary>
        /// <param name="index">Controller index</param>
        /// <param name="b">Button for LeftStick or RightStick</param>
        /// <returns></returns>
        public static Vector2 GetGamePadStick(int index, Buttons b)
        {
            // note this is a bit of a hack, Buttons.LeftStick is actually presssing the left stick
            // but this is a uniform way of handling them
            if(b == Buttons.LeftStick)
            {
                return GamePad.GetState(GetCIndex(index)).ThumbSticks.Left;
            }
            else if(b == Buttons.RightStick)
            {
                return GamePad.GetState(GetCIndex(index)).ThumbSticks.Right;
            }

            return Vector2.Zero;
        }


        public static bool GetGamePadStickPressed(int index, Buttons b)
        {
            GamePadState current = GamePad.GetState(GetCIndex(index));
            GamePadState last = GamePadStateLast[index];

            if (b == Buttons.LeftStick)
            {
                if (last.ThumbSticks.Left != Vector2.Zero)
                    return false;
                else if (current.ThumbSticks.Left != Vector2.Zero)
                    return true;
                else
                    return false;
            }
            else if(b == Buttons.RightStick)
            {
                if (last.ThumbSticks.Right != Vector2.Zero)
                    return false;
                else if (current.ThumbSticks.Right != Vector2.Zero)
                    return true;
                else
                    return false;
            }

            return false;
        }

        public static bool GetGamePadStickHeld(int index, Buttons b)
        {
            GamePadState current = GamePad.GetState(GetCIndex(index));
            GamePadState last = GamePadStateLast[index];

            if (b == Buttons.LeftStick)
            {
                if (current.ThumbSticks.Left != Vector2.Zero)
                    return true;
                else
                    return false;
            }
            else if(b == Buttons.RightStick)
            {
                if (current.ThumbSticks.Right != Vector2.Zero)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public static bool GetGamePadStickReleased(int index, Buttons b)
        {
            GamePadState current = GamePad.GetState(GetCIndex(index));
            GamePadState last = GamePadStateLast[index];

            if (b == Buttons.LeftStick)
            {
                return last.ThumbSticks.Left != Vector2.Zero && current.ThumbSticks.Left == Vector2.Zero;
            }
            else if(b == Buttons.RightStick)
            {
                return last.ThumbSticks.Right != Vector2.Zero && current.ThumbSticks.Right == Vector2.Zero;
            }
            return false;
        }

        public static float GetGamePadTrigger(int index, Buttons b)
        {
            if(b == Buttons.LeftTrigger)
            {
                return GamePad.GetState(GetCIndex(index)).Triggers.Left;
            }
            else if(b == Buttons.RightTrigger)
            {
                return GamePad.GetState(GetCIndex(index)).Triggers.Right;
            }

            return 0f;
        }

        public static bool GetGamePadTriggerPressed(int index, Buttons b)
        {
            GamePadState current = GamePad.GetState(GetCIndex(index));
            GamePadState last = GamePadStateLast[index];

            if (b == Buttons.LeftTrigger)
            {
                if (last.Triggers.Left != 0f)
                    return false;
                else if (current.Triggers.Left != 0f)
                    return true;
                else
                    return false;
            }
            else if (b == Buttons.RightTrigger)
            {
                if (last.Triggers.Right != 0f)
                    return false;
                else if (current.Triggers.Right != 0f)
                    return true;
                else
                    return false;
            }

            return false;
        }

        public static bool GetGamePadTriggerHeld(int index, Buttons b)
        {
            GamePadState current = GamePad.GetState(GetCIndex(index));
            GamePadState last = GamePadStateLast[index];

            if (b == Buttons.LeftTrigger)
            {
                if (current.Triggers.Left != 0f)
                    return true;
                else
                    return false;
            }
            if (b == Buttons.RightTrigger)
            {
                if (current.Triggers.Right != 0f)
                    return true;
                else
                    return false;
            }

            return false;
        }
        public static bool GetGamePadTriggerReleased(int index, Buttons b)
        {
            GamePadState current = GamePad.GetState(GetCIndex(index));
            GamePadState last = GamePadStateLast[index];

            if (b == Buttons.LeftTrigger)
            {
                return last.Triggers.Left != 0f && current.Triggers.Left == 0f;
            }
            if (b == Buttons.RightTrigger)
            {
                return last.Triggers.Right != 0f && current.Triggers.Right == 0f;
            }

            return false;
        }

        #endregion

        /// <summary>
        /// Updates the InputController
        /// </summary>
        /// <param name="screenPositionFactor">Internal render size to window size ratio</param>
        public static void Update(float screenPositionFactor)
        {
            #region Mouse Update
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
                {
                    mouseRightClickPosition = mousePosition;
                    rightOnce = true;
                }
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

            wheelLast = wheelCurrent;
            wheelCurrent = Mouse.GetState().ScrollWheelValue;
            #endregion

            KeyboardStateLast = Keyboard.GetState();


            #region GamePad Update
            for (int i=1; i<=Globals.MaxControllers; i++)
            {
                GamePadStateLast[i] = GamePad.GetState(GetCIndex(i));
            }
            #endregion

        }

        static private PlayerIndex GetCIndex(int index)
        {
            switch (index)
            {
                case 1:
                    return PlayerIndex.One;

                case 2:
                    return PlayerIndex.Two;

                case 3:
                    return PlayerIndex.Three;

                case 4:
                    return PlayerIndex.Four;
            }

            Console.WriteLine("Error - invalid controller index");
            return PlayerIndex.One;
        }
    }
}

