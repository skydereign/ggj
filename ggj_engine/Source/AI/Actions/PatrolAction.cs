using ggj_engine.Source.AI.DecisionTree;
using ggj_engine.Source.Entities.Enemies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI.Actions
{
    class PatrolAction : IAction, IBinaryNode
    {
        private Enemy enemy;
        private Vector2 destination;

        public PatrolAction(Enemy enemy, Vector2 destination)
        {
            this.enemy = enemy;
            this.destination = destination;
        }

        public IAction MakeDecision()
        {
            enemy.Patrolling = true;
            return this;
        }

        public void DoAction()
        {
            if(enemy.CurrentPath.Count <= 0)
            {
                enemy.CurrentPath = Pathing.Pathing.FindPath(enemy, destination);
            }
        }
    }
}
