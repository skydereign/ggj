using ggj_engine.Source.Entities;
using ggj_engine.Source.Entities.Enemies;
using ggj_engine.Source.Entities.Player;
using ggj_engine.Source.Level;
using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Screens
{
    class TestEnemyScreen : Screen
    {
        public TestEnemyScreen()
        {
            AddEntity(new TestEntity(new Vector2(130, 110)));
            AddEntity(new Player(new Vector2(100, 100)));
            AddEntity(new Follower(new Vector2(50, 50)));

            Camera = new Camera(Vector2.Zero, new Vector2(1280, 720));

            TileGrid.Init(50, 50, new Vector2(0, 0));
        }

        public override void Update(GameTime gameTime)
        {
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
