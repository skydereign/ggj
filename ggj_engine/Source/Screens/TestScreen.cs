using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Screens
{
    public class TestScreen : Screen
    {
        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("Update!");
            base.Update(gameTime);
        }
    }
}
