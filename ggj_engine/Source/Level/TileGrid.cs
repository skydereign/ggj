using ggj_engine.Source.Collisions;
using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Level
{
    public static class TileGrid
    {
        public static Texture2D tileTexture;

        public static int Width;
        public static int Height;
        public static Tile[,] Tiles;
        public static int TileSize = Globals.TileSize;
        public static Vector2 Position;

        public static void Init(int width, int height, Vector2 position)
        {
            Tiles = new Tile[width, height];
            Width = width;
            Height = height;
            Position = position;
            TileSize = 16;

            tileTexture = ContentLibrary.Tilesheet;
            TileSize = 16;

            CreateRoom();
        }

        public static void CreateRoom()
        {
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    if(i==0 || j==0 || i==Width-1 || j==Height-1)
                    {
                        Tiles[i, j] = new Tile(1, false, i, j);
                    }
                    else
                    {
                        Tiles[i, j] = new Tile(0, true, i, j);
                    }
                }
            }
            Tiles[20, 20] = new Tile(1, false, 20, 20);
            Tiles[20, 21] = new Tile(1, false, 20, 21);
            Tiles[21, 20] = new Tile(1, false, 21, 20);
            Tiles[21, 21] = new Tile(1, false, 21, 21);
        }

        public enum Direction {Right, Up, Left, Down, Count}

        /// <summary>
        /// Returns false when the tile is not walkable
        /// </summary>
        public static bool CheckCollision (Vector2 newPosition, CircleRegion region, Direction direction)
        {
            float x = newPosition.X;
            float y = newPosition.Y;

            int tileX = (int)x/TileSize;
            int tileY = (int)y/TileSize;

            return (Within(tileX, tileY) && Tiles[tileX, tileY].Walkable == false);
        }

        public static Vector2 AdjustedForCollisions (Vector2 oldPosition, Vector2 newPosition, CircleRegion region)
        {
            int tileX = (int)newPosition.X / Globals.TileSize;
            int tileY = (int)newPosition.Y / Globals.TileSize;
            Vector2 pos1 = new Vector2();
            Vector2 pos2 = new Vector2();
            Vector2 pos3 = new Vector2();

            // right check
            pos1.X = pos2.X = pos3.X = newPosition.X + region.Radius;
            pos1.Y = oldPosition.Y;
            pos2.Y = oldPosition.Y - region.Radius / 2;
            pos3.Y = oldPosition.Y + region.Radius / 2;
            if (CheckCollision(pos1, region, Direction.Right) || CheckCollision(pos2, region, Direction.Right) || CheckCollision(pos3, region, Direction.Right))
            {
                newPosition.X = oldPosition.X;
            }
            
            // left check
            pos1.X = pos2.X = pos3.X = newPosition.X - region.Radius / 2;
            pos1.Y = oldPosition.Y;
            pos2.Y = oldPosition.Y - region.Radius / 2;
            pos3.Y = oldPosition.Y + region.Radius / 2;
            if (CheckCollision(pos1, region, Direction.Left) || CheckCollision(pos2, region, Direction.Left) || CheckCollision(pos3, region, Direction.Left))
            {
                newPosition.X = oldPosition.X;
            }

            
            // down check
            pos1.Y = pos2.Y = pos3.Y = newPosition.Y + region.Radius * 1;
            pos1.X = oldPosition.X;
            pos2.X = oldPosition.X - region.Radius / 2;
            pos3.X = oldPosition.X + region.Radius / 2;
            if (CheckCollision(pos1, region, Direction.Down) || CheckCollision(pos2, region, Direction.Down) || CheckCollision(pos3, region, Direction.Down))
            {
                newPosition.Y = oldPosition.Y;
            }

            // up check
            pos1.Y = pos2.Y = pos3.Y = newPosition.Y - region.Radius / 2;
            pos1.X = oldPosition.X;
            pos2.X = oldPosition.X - region.Radius / 2;
            pos3.X = oldPosition.X + region.Radius / 2;
            if (CheckCollision(pos1, region, Direction.Up) || CheckCollision(pos2, region, Direction.Up) || CheckCollision(pos3, region, Direction.Up))
            {
                newPosition.Y = oldPosition.Y;
            }

            return newPosition;
        }

        public static bool Within (int x, int y)
        {
            return (x >= 0 && y >= 0 && x < Width && y < Height);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int j = 0; j < Height; j++ )
            {
                for(int i=0; i<Width; i++)
                {
                    Tile tile = Tiles[i, j];

                    // should check if the tile is valid
                    int tileX = tile.Type % ContentLibrary.NumHorzTiles;
                    int tileY = tile.Type / ContentLibrary.NumHorzTiles;
                    spriteBatch.Draw(tileTexture, new Rectangle((int)(Position.X + i*TileSize + TileSize/4), (int)(Position.Y + j*TileSize + TileSize/4), TileSize, TileSize),
                                                  new Rectangle(tileX*TileSize, tileY*TileSize, TileSize, TileSize),
                                                  Color.White);
                }
            }
        }
    }
}
