using ggj_engine.Source.AI.DecisionTree;
using ggj_engine.Source.Entities.Enemies;
using ggj_engine.Source.Screens;
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
                enemy.CurrentPath = Pathing.Pathing.FindPath(enemy, enemy.MyScreen.GetEntity("Player").ElementAt(0).Position);
                enemy.CurrentTile = enemy.CurrentPath.Pop();
                enemy.PopOffTop = false;
            }
        }
    }
}
