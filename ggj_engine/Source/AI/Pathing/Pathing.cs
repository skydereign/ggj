﻿using ggj_engine.Source.Entities.Enemies;
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
        private static List<Tile> openTileList;
        private static List<Tile> closedTileList;
        private static List<Tile> path;
        private static Tile start, destination, current;

        public static List<Tile> FindPath(Enemy enemy, Vector2 target)
        {
            openTileList = new List<Tile>();
            closedTileList = new List<Tile>();
            path = new List<Tile>();
            start = TileGrid.Tiles[(int)enemy.Position.X / TileGrid.TileSize, (int)enemy.Position.Y / TileGrid.TileSize];
            destination = TileGrid.Tiles[(int)target.X / TileGrid.TileSize, (int)target.Y / TileGrid.TileSize];

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
                    float hScore = (float)Math.Sqrt((destination.X - current.X) ^ 2 + (destination.Y - current.Y) ^ 2);
                    float fScore = gScore + hScore;
                    if(isValueInList(t, closedTileList) && fScore >= t.FScore)
                    {
                        continue;
                    }
                    if(!isValueInList(t, openTileList) || fScore < t.FScore)
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

        private static bool isValueInList(Tile t, List<Tile> list)
        {
            if(list.Contains(t))
            {
                return true;
            }
            return false;
        }

        private static List<Tile> getPath(Tile current)
        {
            if(current.Parent != null)
            {
                path.Add(current);
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
    }
}