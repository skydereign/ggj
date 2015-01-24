using ggj_engine.Source.Entities;
using ggj_engine.Source.Level;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Screens
{
    public class TestScreen : Screen
    {
        public TileGrid level;

        public TestScreen()
        {
            AddEntity(new TestEntity(new Vector2(100, 100)));

            level = new TileGrid(10, 10, new Vector2(0, 0));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
