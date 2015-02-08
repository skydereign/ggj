using ggj_engine.Source.Entities.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Screens
{
    class TestWeaponScreen : Screen
    {
        public TestWeaponScreen()
        {
            AddEntity(new Player(new Vector2(150, 100)));
            //TileGrid.Init(50, 50, new Vector2(0, 0));
        }
    }
}
