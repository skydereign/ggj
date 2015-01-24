using ggj_engine.Source.AI.DecisionTree;
using ggj_engine.Source.Entities.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI.Actions
{
    class PatrolAction : IAction, IBinaryNode
    {
        private Enemy enemy;
        public PatrolAction(Enemy enemy)
        {

        }

        public IAction MakeDecision()
        {
            return this;
        }

        public void DoAction()
        {

        }
    }
}
