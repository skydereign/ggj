using ggj_engine.Source.AI.DecisionTree;
using ggj_engine.Source.Entities.Enemies;
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
            return this;
        }

        public void DoAction()
        {
            
        }
    }
}
