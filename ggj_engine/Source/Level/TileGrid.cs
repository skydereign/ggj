﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Level
{
    public static class TileGrid
    {
        List<Texture2D> tileTextures;

        public static int Width;
        public static int Height;
        public static Tile[,] Tiles;
        public static int TileSize;
        public Vector2 Position;

        public TileGrid(int width, int height, Vector2 position)
        {
            Tiles = new Tile[width, height];
            Width = width;
            Height = height;
            Position = position;
            TileSize = 16;

            tileTextures = new List<Texture2D>();
            // need to get textures

            CreateRoom();
        }

        public void CreateRoom()
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

        public void InitTiles()
        {
            //
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int j = 0; j < Height; j++ )
            {
                for(int i=0; i<Width; i++)
                {
                    Tile tile = Tiles[i, j];

                    // valid tile
                    if (tileTextures.Count < tile.Type)
                    {
                        spriteBatch.Draw(tileTextures[tile.Type], Position, Color.White);
                    }
                }
            }
            spriteBatch.End();
        }
    }
}
