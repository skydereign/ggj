using ggj_engine.Source.AI.DecisionTree;
using ggj_engine.Source.Entities.Enemies;
using ggj_engine.Source.Entities.Player;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI.Actions
{
    class RunFromPlayerAction : IAction, IBinaryNode
    {
        private Enemy enemy;
        public RunFromPlayerAction(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public IAction MakeDecision()
        {
            enemy.Evading = true;
            enemy.PerformingAction = true;
            return this;
        }

        public void DoAction()
        {
            Player player = (Player)enemy.MyScreen.GetEntity("Player").ElementAt(0);
            float angle = MathExt.Direction(enemy.Position, player.Position);
            Vector2 target = new Vector2((float)(player.Position.X - Math.Cos(angle) * 160.0f),
                        (float)(player.Position.Y + Math.Sin(angle) * 160.0f));
            enemy.CurrentPath = Pathing.Pathing.FindPath(enemy, target);
            if(enemy.CurrentPath == null)
            {
                return;
            }
            enemy.CurrentTile = enemy.CurrentPath.Pop();
            enemy.PopOffTop = false;
        }
    }
}
