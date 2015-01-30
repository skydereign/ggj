using ggj_engine.Source.Entities;
using ggj_engine.Source.Entities.UIElements;
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
    class MenuScreen : Screen
    {

        public MenuScreen()
        {
            AddEntity(new StarBackground());
            AddEntity(new Label("Kill Switch: Engage", new Vector2(200, 50), Color.White, 1.0f));
            AddEntity(new Button("Play Game", new Vector2(500, 300), Color.White, 0.5f, new Vector2(180, 50)));

            // play bgm music
            Game1.SoundController.PlayMusic("title_screen", true);
        }

        public override void Update(GameTime gameTime)
        {
            Camera.Position.X += Globals.BackgroundAnimation;


            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteBatchCameraBegin(spriteBatch);
            spriteBatch.End();

            base.Draw(spriteBatch);


            ////Draw HUD after everything else has been drawn
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
                        DepthStencilState.Default, RasterizerState.CullCounterClockwise, null);

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
