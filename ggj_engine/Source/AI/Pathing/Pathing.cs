using ggj_engine.Source.Entities.Enemies;
using ggj_engine.Source.Level;
using ggj_engine.Source.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI
{
    public class Pathing
    {
        private List<Tile> openTileList;
        private List<Tile> closedTileList;
        private List<Tile> path;
        private Tile start, destination, current;
        private int destX, destY;

        public void FindPath(Enemy enemy)
        {
            openTileList = new List<Tile>();
            closedTileList = new List<Tile>();
            path = new List<Tile>();
            start = TileGrid.Tiles[(int)enemy.Position.X / TileGrid.TileSize, (int)enemy.Position.Y / TileGrid.TileSize];
            destX = (int)RandomUtil.Next(0, TileGrid.Width);
            destY = (int)RandomUtil.Next(0, TileGrid.Height);
            destination = TileGrid.Tiles[destX, destY];

            openTileList.Add(start);
            while(openTileList.Count > 0)
            {
                current = getBestTile();
                if(current == null)
                {
                    return;
                }
                if(TileGrid.Tiles[(int)current.X, (int)current.Y] == 
                    TileGrid.Tiles[(int)destination.X,(int)destination.Y])
                {
                    getPath(current);
                }
                openTileList.Remove(current);
                closedTileList.Add(current);
                List<Tile> neighbors = getNeighbors(current);
                foreach(Tile t in neighbors)
                {
                    float g_score = t.g_score + 1;
                    float h_score = (float)Math.Sqrt((destination.X - current.X) ^ 2 + (destination.Y - current.Y) ^ 2);
                    float f_score = g_score + h_score;
                    if(isValueInList(t, closedTileList) && f_score >= t.f_score)
                    {
                        continue;
                    }
                    if(!isValueInList(t, openTileList) || f_score < t.f_score)
                    {
                        t.Parent = current;
                        t.g_score = g_score;
                        t.h_score = h_score;
                        t.f_score = f_score;
                        openTileList.Add(t);
                    }
                }
            }
        }

        private bool isValueInList(Tile t, List<Tile> list)
        {
            if(list.Contains(t))
            {
                return true;
            }
            return false;
        }

        private void getPath(Tile current)
        {
            if(current.Parent != null)
            {
                getPath(current.Parent);
            }
        }

        private Tile getBestTile()
        {
            Tile bestTile = null;
            float bestFScore = 10000000;
            for(int i = 0; i < openTileList.Count; i++)
            {
                float fScore = openTileList.ElementAt(i).f_score;
                if (fScore < bestFScore)
                {
                    bestFScore = fScore;
                    bestTile = openTileList.ElementAt(i);
                }
            }
            return bestTile;
        }

        private List<Tile> getNeighbors(Tile tile)
        {
            List<Tile> neighbors = new List<Tile>();
            neighbors.Add(TileGrid.Tiles[tile.X + 1, tile.Y + 1]);
            neighbors.Add(TileGrid.Tiles[tile.X - 1, tile.Y + 1]);
            neighbors.Add(TileGrid.Tiles[tile.X + 1, tile.Y - 1]);
            neighbors.Add(TileGrid.Tiles[tile.X - 1, tile.Y - 1]);
            return neighbors;
        }
    }
}
