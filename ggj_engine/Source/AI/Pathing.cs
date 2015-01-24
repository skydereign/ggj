using ggj_engine.Source.Entities.Enemies;
using ggj_engine.Source.Level;
using ggj_engine.Source.Utility;
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
        private int destX, destY;

        public void InitPathing(Enemy enemy)
        {
            openTileList = new List<Tile>();
            closedTileList = new List<Tile>();
            closedTileList.Add(TileGrid.Tiles[(int)enemy.Position.X / TileGrid.TileSize, (int)enemy.Position.Y / TileGrid.TileSize]);
            destX = (int)RandomUtil.Next(0, TileGrid.Width);
            destY = (int)RandomUtil.Next(0, TileGrid.Height);
            destination = TileGrid.Tiles[destX / TileGrid.TileSize, destY / TileGrid.TileSize];
            PopulateInitialOpenList();
        }

        public List<Tile> FindPath()
        {
            while(openTileList.Count > 0)
            {

            }

            return closedTileList;
        }

        private void PopulateInitialOpenList()
        {
            openTileList.Add(TileGrid.Tiles[closedTileList[0].X - 1, closedTileList[0].Y - 1]);
            openTileList.Add(TileGrid.Tiles[closedTileList[0].X - 1, closedTileList[0].Y + 1]);
            openTileList.Add(TileGrid.Tiles[closedTileList[0].X + 1, closedTileList[0].Y + 1]);
            openTileList.Add(TileGrid.Tiles[closedTileList[0].X + 1, closedTileList[0].Y - 1]);
        }
    }
}
