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
            if(TileGrid.Tiles[(int)enemy.Position.X / TileGrid.TileSize, (int)enemy.Position.Y / TileGrid.TileSize] == TileGrid.Tiles[wayPoint.X, wayPoint.Y])
            {
                enemy.PopOffTop = true;
            }
            if(enemy.PopOffTop)
            {
                if(enemy.CurrentPath.Count <= 0)
                {
                    enemy.PerformingAction = false;
                    enemy.Patrolling = false;
                    return Vector2.Zero;
                }
                enemy.CurrentTile = enemy.CurrentPath.Pop();
                enemy.PopOffTop = false;
            }
            float angle = (float)Math.Atan2(enemy.Position.Y - wayPoint.Y, enemy.Position.X - wayPoint.X);
            Vector2 moveVector = new Vector2((float)Math.Cos(angle) * enemy.Speed, (float)Math.Sin(angle) * enemy.Speed);
            return moveVector;
        }
    }
}
