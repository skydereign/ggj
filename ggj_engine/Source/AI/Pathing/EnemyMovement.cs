using ggj_engine.Source.Entities.Enemies;
using ggj_engine.Source.Level;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI.Pathing
{
    public static class EnemyMovement
    {
        public static Vector2 MoveTowardsTile(Enemy enemy, Tile wayPoint)
        {
            if (wayPoint != null && TileGrid.Tiles[(int)Math.Round(enemy.Position.X / TileGrid.TileSize), (int)Math.Round(enemy.Position.Y / TileGrid.TileSize)] == TileGrid.Tiles[wayPoint.X, wayPoint.Y])
            {
                enemy.PopOffTop = true;
            }
            if(enemy.PopOffTop)
            {
                if(enemy.CurrentPath.Count <= 0)
                {
                    enemy.PerformingAction = false;
                    enemy.Patrolling = false;
                    enemy.Following = false;
                    enemy.Evading = false;
                    return Vector2.Zero;
                }
                enemy.CurrentTile = enemy.CurrentPath.Pop();
                enemy.PopOffTop = false;
            }
            if (wayPoint != null)
            {
                float angle = (float)Math.Atan2(wayPoint.Y - enemy.Position.Y / 16, wayPoint.X - enemy.Position.X / 16);
                Vector2 moveVector = new Vector2((float)Math.Cos(angle) * enemy.Speed, (float)Math.Sin(angle) * enemy.Speed);
                return moveVector;
            }
            return Vector2.Zero;
        }
    }
}
