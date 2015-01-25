using ggj_engine.Source.AI.DecisionTree;
using ggj_engine.Source.Entities.Enemies;
using ggj_engine.Source.Entities.Player;
using ggj_engine.Source.Level;
using ggj_engine.Source.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI.Actions
{
    class FollowPlayerAction : IAction, IBinaryNode
    {
        private Enemy enemy;

        public FollowPlayerAction(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public IAction MakeDecision()
        {
            enemy.PerformingAction = true;
            enemy.Following = true;
            return this;
        }

        public void DoAction()
        {
            if(enemy.CurrentPath.Count <= 0)
            {
                Player player = (Player)enemy.MyScreen.GetEntity("Player").ElementAt(0);
                if(enemy.GetType().Name.Equals("YourMom"))
                {
                    float angle = (float)Math.Atan2(player.Position.Y / 16 - enemy.Position.Y / 16, player.Position.X / 16 - enemy.Position.X / 16);
                    Vector2 target = new Vector2((float)(player.Position.X / 16 - Math.Cos(angle) * 2.0f),
                        (float)(player.Position.Y / 16 + Math.Sin(angle) * 2.0f));

                    enemy.CurrentPath = Pathing.Pathing.FindPath(enemy, target);

                }
                else
                {
                    enemy.CurrentPath = Pathing.Pathing.FindPath(enemy, player.Position);
                }
                enemy.CurrentTile = enemy.CurrentPath.Pop();
                enemy.PopOffTop = false;
            }
        }
    }
}
