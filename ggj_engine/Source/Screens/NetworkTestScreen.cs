using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ggj_engine.Source.Network;
using ggj_engine.Source.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;

namespace ggj_engine.Source.Screens
{
    public class NetworkTestScreen : Screen
    {
        public Camera Camera;
        Sprite sprite;
        Vector2 position = Vector2.Zero;
        public NetworkTestScreen()
        {
            sprite = ContentLibrary.Sprites["test_animation"];

            Camera = new Camera(Vector2.Zero, new Vector2(1280, 720));

            Console.WriteLine("Host? yes or no");

            string response = Console.ReadLine();
            
            if (response.CompareTo("yes") == 0)
                NetworkManager.Instance.CreateHost();
            else
                NetworkManager.Instance.ConnectToHost();

            Console.WriteLine();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            NetworkManager.Instance.Solve();

            if (NetworkManager.Instance.IsHost)
            {
                position.X += 1f;
                position.Y += 1f;

                sprite.Position = position;

                NetworkManager.Instance.WriteTCPToClients("Position: " + position.X + " " + position.Y);
            }
            else
            {

            }

            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            //Draw all entities
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp,
                    DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Camera.GetViewMatrix());
            sprite.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }

        public override void Close()
        {
            base.Close();
        }
    }
}
