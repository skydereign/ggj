using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.UIElements
{
    class StringGUI : GUI
    {
        public string Value;

        private SpriteFont font;
        private bool selected;
        private string defaultValue;
        private int backspaceTimer = 0;
        private Keys escape;

        public StringGUI(string label, string initial, Keys escape) : base(label)
        {
            font = ContentLibrary.Fonts["pixelFont"];
            Value = initial;
            defaultValue = initial;
            this.escape = escape;
        }

        public override void Update(GameTime gameTime)
        {
            if (InputControl.GetMouseOnLeftPressed())
            {
                Vector2 mousePos = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());
                Vector2 dimensions = font.MeasureString(label + Value.ToString()) * Globals.GUIScale;

                if (mousePos.X >= Position.X && mousePos.X <= Position.X + dimensions.X && mousePos.Y >= Position.Y && mousePos.Y <= Position.Y + dimensions.Y)
                {
                    selected = true;
                }
                else
                {
                    Deselect();
                }
            }

            // reset when right clicked
            if (InputControl.GetMouseOnRightPressed())
            {
                Vector2 mousePos = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());
                Vector2 dimensions = font.MeasureString(label + Value.ToString()) * Globals.GUIScale;

                if (mousePos.X >= Position.X && mousePos.X <= Position.X + dimensions.X && mousePos.Y >= Position.Y && mousePos.Y <= Position.Y + dimensions.Y)
                {
                    if (selected)
                    {
                        Value = "";
                    }
                    else
                    {
                        Value = defaultValue;
                    }
                }
            }

            // loop through all pressed keys
            if (selected)
            {
                foreach (Keys k in Keyboard.GetState().GetPressedKeys())
                {
                    // only type for when the key is pressed
                    if (InputControl.GetKeyboardKeyPressed(k))
                    {
                        if (k >= Keys.A && k <= Keys.Z)
                        {
                            Value += (InputControl.GetKeyboardKeyHeld(Keys.LeftShift) || InputControl.GetKeyboardKeyHeld(Keys.RightShift))
                                ? k.ToString() : k.ToString().ToLower();
                        }
                        else if (k >= Keys.D0 && k <= Keys.D9)
                        {
                            Value += (InputControl.GetKeyboardKeyHeld(Keys.LeftShift) || InputControl.GetKeyboardKeyHeld(Keys.RightShift))
                                ? k.ToString()[1] : k.ToString().ToLower()[1];
                        }
                    }
                }

                if (InputControl.GetKeyboardKeyPressed(Keys.Back)  && Value.Length > 0)
                {
                    Value = Value.Remove(Value.Length - 1);
                    backspaceTimer = 10;
                }
                else if (InputControl.GetKeyboardKeyHeld(Keys.Back))
                {
                    backspaceTimer--;
                    if (backspaceTimer <= 0 && Value.Length > 0)
                    {
                        Value = Value.Remove(Value.Length - 1);
                        backspaceTimer = 6;
                    }
                }

                if (InputControl.GetKeyboardKeyPressed(escape))
                {
                    Deselect();
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Color color = (selected) ? Color.Gray : Color.White;
            spriteBatch.DrawString(font, label + Value.ToString(), Position, color, 0, Vector2.Zero, Globals.GUIScale, SpriteEffects.None, 0);
        }

        public override float Top()
        {
            return Position.Y;
        }

        public override float Bot()
        {
            return Position.Y + font.MeasureString(label).Y * Globals.GUIScale;
        }

        public override float Right()
        {
            return Position.X + (font.MeasureString(label).X + font.MeasureString(Value.ToString()).X) * Globals.GUIScale;
        }

        public override float Left()
        {
            return Position.X;
        }

        private void Deselect()
        {
            selected = false;
            if(Value == "")
            {
                Value = defaultValue;
            }
        }
    }
}
