using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ggj_engine.Source.Network;
using ggj_engine.Source.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;
using ggj_engine.Source.Entities;

namespace ggj_engine.Source.Screens
{
    public class NetworkTestScreen : Screen
    {
        List<Vector2> vels;
        public NetworkTestScreen()
        {
            Camera = new Camera(Vector2.Zero, new Vector2(1280, 720));
            vels = new List<Vector2>();
            for (int i = 0; i < 10; ++i)
            {
                vels.Add(new Vector2((float)RandomUtil.Next() * 2 + .5f, (float)RandomUtil.Next() * 2 + .5f));
                entities.Add(new TestEntity(new Vector2((float)RandomUtil.Next(-Camera.ScreenDimensions.X / 2, Camera.ScreenDimensions.X / 2), (float)RandomUtil.Next(-Camera.ScreenDimensions.Y / 2, Camera.ScreenDimensions.Y / 2))));
            }

            NetworkManager.Instance.DetermineHost();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //update game world
            //update logic

            //update net man
            NetworkManager.Instance.Solve(gameTime, entities);

            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            //Draw all entities
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp,
                    DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Camera.GetViewMatrix());

            for (int i = 0; i < entities.Count; ++i) 
                entities[i].Draw(spriteBatch);
            
            spriteBatch.End();
            base.Draw(spriteBatch);
        }

        public override void Close()
        {
            base.Close();
        }
    }
}
