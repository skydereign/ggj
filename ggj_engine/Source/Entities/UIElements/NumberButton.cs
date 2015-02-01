using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.UIElements
{
    class NumberButton : Entity
    {
        public float Value;

        private SpriteFont font;
        private Vector2 dimensions;
        private float max;
        private float min;
        private float step;
        private bool clicked = false;
        private float valueClick = 0.0f;

        public NumberButton(Vector2 position, float initial, float step, float min, float max)
        {
            Position = position;
            Value = initial;
            this.step = step;
            this.max = max;
            this.min = min;

            font = ContentLibrary.Fonts["pixelFont"];
            setDimensions();
        }


        public override void Update(GameTime gameTime)
        {
            // Mouse wheel setting of Value for more precise increments
            if (InputControl.GetMouseWheelUp() || InputControl.GetMouseWheelDown())
            {
                Vector2 mousePos = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());

                if (mousePos.X >= Position.X && mousePos.X <= Position.X + dimensions.X && mousePos.Y >= Position.Y && mousePos.Y <= Position.Y + dimensions.Y)
                {
                    if(InputControl.GetMouseWheelDown())
                    {
                        Value += step;
                    }
                    else if(InputControl.GetMouseWheelUp())
                    {
                        Value -= step;
                    }
                    Value = Math.Max(min, Math.Min(max, Value));
                    setDimensions();
                }
            }

            // Click/Drag setting of Value
            if(InputControl.GetMouseOnLeftPressed())
            {
                Vector2 mousePos = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());

                if (mousePos.X >= Position.X && mousePos.X <= Position.X + dimensions.X && mousePos.Y >= Position.Y && mousePos.Y <= Position.Y + dimensions.Y)
                {
                    valueClick = Value;
                    clicked = true;
                }
            }
            else if(InputControl.GetMouseOnLeftReleased())
            {
                clicked = false;
            }
            else if(clicked && InputControl.GetMouseOnLeftHeld())
            {
                float offset = InputControl.GetMouseLeftClickPosition().Y - InputControl.GetMousePosition().Y;
                Value = valueClick + offset;
                Value = Math.Max(min, Math.Min(max, Value));
                setDimensions();
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, Value.ToString(), Position, Color.White, 0, Vector2.Zero, Globals.GUIScale, SpriteEffects.None, 0);
        }

        private void setDimensions ()
        {
            dimensions = font.MeasureString(Value.ToString());
            dimensions *= Globals.GUIScale;
        }
    }
}
