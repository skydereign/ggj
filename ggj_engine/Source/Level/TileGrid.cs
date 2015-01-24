using ggj_engine.Source.Media;
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
        public static int TileSize;
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
                    spriteBatch.Draw(tileTexture, new Rectangle((int)(Position.X + i*TileSize), (int)(Position.Y + j*TileSize), TileSize, TileSize),
                                                  new Rectangle(tileX*TileSize, tileY*TileSize, TileSize, TileSize),
                                                  Color.White);
                }
            }
        }
    }
}
