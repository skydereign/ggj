using ggj_engine.Source.AI.DecisionTree;
using ggj_engine.Source.Entities.Enemies;
using ggj_engine.Source.Level;
using ggj_engine.Source.Utility;
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
        private List<Vector2> destination;
        private Vector2 nextSpot;
        private int iterator;

        public PatrolAction(Enemy enemy, List<Vector2> destination)
        {
            this.enemy = enemy;
            this.destination = destination;
            iterator = (int)RandomUtil.Next(destination.Count);
            nextSpot = destination[iterator];
        }

        public IAction MakeDecision()
        {
            enemy.Patrolling = true;
            enemy.PerformingAction = true;
            return this;
        }

        public void DoAction()
        {
            if(enemy.CurrentPath == null || enemy.CurrentPath.Count <= 0)
            {
                enemy.CurrentPath = Pathing.Pathing.FindPath(enemy, nextSpot);
                if(enemy.CurrentPath ==null)
                {
                    enemy.Patrolling = false;
                    enemy.PerformingAction = false;
                    return;
                }
                enemy.CurrentTile = enemy.CurrentPath.Pop();
                enemy.PopOffTop = false;
            }
            int prevIterator = iterator;
            iterator = (int)RandomUtil.Next(destination.Count);
            while (iterator == prevIterator)
            {
                iterator = (int)RandomUtil.Next(destination.Count);
            }
            if (iterator >= destination.Count)
            {
                iterator = 0;
            }
            nextSpot = destination[iterator];
        }
    }
}
