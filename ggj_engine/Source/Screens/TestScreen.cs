using ggj_engine.Source.Entities;
using ggj_engine.Source.Level;
using ggj_engine.Source.Utility;
using ggj_engine.Source.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Screens
{
    public class TestScreen : Screen
    {

        public TestScreen()
        {
            AddEntity(new TestEntity(new Vector2(100, 100)));

            Camera = new Camera(Vector2.Zero, new Vector2(1280,720));

            TileGrid.Init(10, 10, new Vector2(0, 0));
        }

        public override void Update(GameTime gameTime)
        {
            //Camera movement
            if (InputControl.GetKeyboardKeyHeld(Microsoft.Xna.Framework.Input.Keys.W))
            {
                Camera.Position.Y -= 1f;
            }
            if (InputControl.GetKeyboardKeyHeld(Microsoft.Xna.Framework.Input.Keys.A))
            {
                Camera.Position.X -= 1f;
            }
            if (InputControl.GetKeyboardKeyHeld(Microsoft.Xna.Framework.Input.Keys.S))
            {
                Camera.Position.Y += 1f;
            }
            if (InputControl.GetKeyboardKeyHeld(Microsoft.Xna.Framework.Input.Keys.D))
            {
                Camera.Position.X += 1f;
            }
            //Camera Zoom
            if (InputControl.GetMouseWheelUp())
            {
                Camera.Zoom -= 0.05f;
            }
            if (InputControl.GetMouseWheelDown())
            {
                Camera.Zoom += 0.05f;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteBatchCameraBegin(spriteBatch);
            TileGrid.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(spriteBatch);

            //Draw cursor after everything else has been drawn
            spriteBatch.Begin();
            if (InputControl.GetMouseOnLeftHeld())
            {
                spriteBatch.Draw(ContentLibrary.Sprites["cursor"].Texture, InputControl.GetMousePosition(), Color.Blue);
            }
            else
            {
                spriteBatch.Draw(ContentLibrary.Sprites["cursor"].Texture, InputControl.GetMousePosition(), Color.White);
            }
            spriteBatch.DrawString(ContentLibrary.Fonts["smallFont"], "Camera pos: [" + Camera.Position.X.ToString() + " | " + Camera.Position.Y.ToString() + "]", new Vector2(5, 30), Color.White);
            spriteBatch.End();
        }
    }
}
