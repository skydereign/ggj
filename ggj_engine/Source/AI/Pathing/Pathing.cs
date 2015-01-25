using ggj_engine.Source.Entities.Enemies;
using ggj_engine.Source.Level;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI.Pathing
{
    public static class Pathing
    {
        #region A* Algorithm
        private static List<Tile> openTileList;
        private static List<Tile> closedTileList;
        private static Stack<Tile> path;
        private static Tile start, destination, current;

        public static void PrintPath(Enemy enemy)
        {
            foreach(Tile t in enemy.CurrentPath)
            {
                Console.WriteLine("Tile t: " + new Vector2(t.X, t.Y));
            }
        }

        public static Stack<Tile> FindPath(Enemy enemy, Vector2 target)
        {
            openTileList = new List<Tile>();
            closedTileList = new List<Tile>();
            path = new Stack<Tile>();
            start = TileGrid.Tiles[(int)enemy.Position.X / TileGrid.TileSize, (int)enemy.Position.Y / TileGrid.TileSize];
            destination = TileGrid.Tiles[(int)target.X / TileGrid.TileSize, (int)target.Y / TileGrid.TileSize];
            Console.WriteLine("start: " + new Vector2(start.X, start.Y));
            Console.WriteLine("destination: " + new Vector2(destination.X, destination.Y));
            openTileList.Add(start);
            while(openTileList.Count > 0)
            {
                current = getBestTile();
                if(current == null)
                {
                    return null;
                }
                if(TileGrid.Tiles[(int)current.X, (int)current.Y] == 
                    TileGrid.Tiles[(int)destination.X,(int)destination.Y])
                {
                    return getPath(current);
                }
                openTileList.Remove(current);
                closedTileList.Add(current);
                List<Tile> neighbors = getNeighbors(current);
                foreach(Tile t in neighbors)
                {
                    float gScore = t.GScore + 1;
                    float hScore = (float)distance(t.X, t.Y, destination.X, destination.Y);
                    float fScore = gScore + hScore;
                    if(isValueInList(t, closedTileList) && fScore >= t.FScore)
                    {
                        continue;
                    }
                    else if(!isValueInList(t, openTileList) || fScore < t.FScore)
                    {
                        t.Parent = current;
                        t.GScore = gScore;
                        t.HScore = hScore;
                        t.FScore = fScore;
                        if(t.Walkable)
                        {
                            openTileList.Add(t);
                        }
                    }
                }
            }
            return null;
        }

        private static double distance(float startX, float startY, float destX, float destY)
        {
            double x = (double)destX - (double)startX;
            double y = (double)destY - (double)startY;
            return Math.Sqrt(x * x + y * y);
        }

        private static bool isValueInList(Tile t, List<Tile> list)
        {
            if(list.Contains(t))
            {
                return true;
            }
            return false;
        }

        private static Stack<Tile> getPath(Tile current)
        {
            if(current.Parent != null)
            {
                path.Push(current);
                getPath(current.Parent);
            }
            return path;
        }

        private static Tile getBestTile()
        {
            Tile bestTile = null;
            float bestFScore = 10000000;
            for(int i = 0; i < openTileList.Count; i++)
            {
                float fScore = openTileList.ElementAt(i).FScore;
                if (fScore < bestFScore)
                {
                    bestFScore = fScore;
                    bestTile = openTileList.ElementAt(i);
                }
            }
            return bestTile;
        }

        private static List<Tile> getNeighbors(Tile tile)
        {
            List<Tile> neighbors = new List<Tile>();
            neighbors.Add(TileGrid.Tiles[tile.X + 1, tile.Y + 1]);
            neighbors.Add(TileGrid.Tiles[tile.X - 1, tile.Y + 1]);
            neighbors.Add(TileGrid.Tiles[tile.X + 1, tile.Y - 1]);
            neighbors.Add(TileGrid.Tiles[tile.X - 1, tile.Y - 1]);
            return neighbors;
        }
        #endregion
    }
}
