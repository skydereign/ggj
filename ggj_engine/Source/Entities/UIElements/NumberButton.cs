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
    class NumberButton : GUI
    {
        public float Value;

        private SpriteFont font;
        private Vector2 dimensions;
        private float max;
        private float min;
        private float step;
        private bool clicked = false;
        private float valueClick = 0.0f;

        public NumberButton(string label, Vector2 position, float initial, float step, float min, float max) : base(label)
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
            Vector2 numberPos = Position;
            numberPos.X += font.MeasureString(label).X * Globals.GUIScale;
            // Mouse wheel setting of Value for more precise increments
            if (InputControl.GetMouseWheelUp() || InputControl.GetMouseWheelDown())
            {
                Vector2 mousePos = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());

                if (mousePos.X >= numberPos.X && mousePos.X <= numberPos.X + dimensions.X && mousePos.Y >= numberPos.Y && mousePos.Y <= numberPos.Y + dimensions.Y)
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

                if (mousePos.X >= numberPos.X && mousePos.X <= numberPos.X + dimensions.X && mousePos.Y >= numberPos.Y && mousePos.Y <= numberPos.Y + dimensions.Y)
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
            Value = Value.Truncate(3);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, label + Value.ToString(), Position, Color.White, 0, Vector2.Zero, Globals.GUIScale, SpriteEffects.None, 0);
        }

        private void setDimensions ()
        {
            dimensions = font.MeasureString(Value.ToString());
            dimensions *= Globals.GUIScale;
        }

        public override float Top()
        {
            return Position.Y;
        }

        public override float Bot()
        {
            return Position.Y + font.MeasureString(Value.ToString()).Y * Globals.GUIScale;
        }

        public override float Right()
        {
            return Position.X + (font.MeasureString(label).X + font.MeasureString(Value.ToString()).X) * Globals.GUIScale;
        }

        public override float Left()
        {
            return Position.X;
        }
    }
}
