﻿using ggj_engine.Source.AI.DecisionTree;
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
            iterator = 0;
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
            if(enemy.CurrentPath.Count <= 0)
            {
                Console.WriteLine("destination: " + new Vector2(nextSpot.X / 16, nextSpot.Y / 16));
                enemy.CurrentPath = Pathing.Pathing.FindPath(enemy, nextSpot);
                Pathing.Pathing.PrintPath(enemy);
                enemy.CurrentTile = enemy.CurrentPath.Pop();
                enemy.PopOffTop = false;
            }
            iterator++;
            nextSpot = destination[iterator];
            if(iterator >= 3)
            {
                iterator = -1;
            }
        }
    }
}
