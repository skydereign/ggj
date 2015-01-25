﻿using ggj_engine.Source.Entities;
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
    class TestPlayerScreen : Screen
    {
        public TestPlayerScreen()
        {
            // AddEntity(new TestEntity(new Vector2(100, 100)));
            AddEntity(new TestEntity(new Vector2(130, 110)));
            AddEntity(new Player(new Vector2(100, 100)));
            AddEntity(new StarBackground());

            Camera = new Camera(Vector2.Zero, new Vector2(1280,720));
            GameManager = new GameManagement.GameManager();

            TileGrid.Init(10, 10, new Vector2(0, 0));
        }

        public override void Update(GameTime gameTime)
        {
            //Camera movement
            //if (InputControl.GetKeyboardKeyHeld(Microsoft.Xna.Framework.Input.Keys.W))
            //{
            //    Camera.Position.Y -= 1f;
            //}
            //if (InputControl.GetKeyboardKeyHeld(Microsoft.Xna.Framework.Input.Keys.A))
            //{
            //    Camera.Position.X -= 1f;
            //}
            //if (InputControl.GetKeyboardKeyHeld(Microsoft.Xna.Framework.Input.Keys.S))
            //{
            //    Camera.Position.Y += 1f;
            //}
            //if (InputControl.GetKeyboardKeyHeld(Microsoft.Xna.Framework.Input.Keys.D))
            //{
            //    Camera.Position.X += 1f;
            //}

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

            //Draw HUD after everything else has been drawn
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
                        DepthStencilState.Default, RasterizerState.CullCounterClockwise, null);

            GameManager.Draw(spriteBatch);

            if (InputControl.GetMouseOnLeftHeld())
            {
                spriteBatch.Draw(ContentLibrary.Sprites["cursor"].Texture, InputControl.GetMousePosition(), Color.Blue);
            }
            else
            {
                spriteBatch.Draw(ContentLibrary.Sprites["cursor"].Texture, InputControl.GetMousePosition(), Color.White);
            }
            spriteBatch.DrawString(ContentLibrary.Fonts["smallFont"], "Camera pos: [" + Camera.Position.X.ToString() + " | " + Camera.Position.Y.ToString() + "]", new Vector2(5, 700), Color.White);
            spriteBatch.End();
        }
    }
}
