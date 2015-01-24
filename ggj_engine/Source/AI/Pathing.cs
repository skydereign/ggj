using ggj_engine.Source.Entities.Enemies;
using ggj_engine.Source.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI
{
    public static class Pathing
    {
        private List<Tile> openTileList;
        private List<Tile> closedTileList;
        private Tile destination;
        

        public void InitPathing(Enemy enemy)
        {
            openTileList = new List<Tile>();
            closedTileList = new List<Tile>();
            closedTileList.Add(TileGrid.Tiles[(int)enemy.Position.X / TileGrid.TileSize, (int)enemy.Position.Y / TileGrid.TileSize]);
        }
    }
}
