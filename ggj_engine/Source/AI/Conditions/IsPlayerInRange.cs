﻿using ggj_engine.Source.Entities.Enemies;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI.Conditions
{
    class IsPlayerInRange : ICondition
    {
        private Enemy enemy;
        private Vector2 target;
        private float range;

        public IsPlayerInRange(Enemy enemy, Vector2 target, float range)
        {
            this.enemy = enemy;
            this.range = range;
            this.target = target;
        }

        public bool Test()
        {
            if(Vector2.Distance(enemy.Position, target) < range)
            {
                return true;
            }
            return false;
        }
    }
}
