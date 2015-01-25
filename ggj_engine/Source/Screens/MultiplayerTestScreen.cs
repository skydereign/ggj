using ggj_engine.Source.Entities.Player;
using ggj_engine.Source.Level;
using ggj_engine.Source.Media;
using ggj_engine.Source.Network;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Screens
{
    public class MultiplayerTestScreen : Screen
    {
        public MultiplayerTestScreen()
        {
            Camera = new Camera(Vector2.Zero, new Vector2(1280, 720));

            entities = new List<Entities.Entity>();

            entities.Add(new Player(new Vector2(100,100)));
            entities[entities.Count - 1].MyScreen = this;

            TileGrid.Init(50, 50, new Vector2(0, 0));

            NetworkManager.Instance.DetermineHost();

            
        }

        public override void Update(GameTime gameTime)
        {
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

            NetworkManager.Instance.Solve(gameTime, entities);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
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
