using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ggj_engine.Source.Network;

namespace ggj_engine.Source.Screens
{
    public class NetworkTestScreen : Screen
    {
        public NetworkTestScreen()
        {
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
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Close()
        {
            base.Close();
        }
    }
}
