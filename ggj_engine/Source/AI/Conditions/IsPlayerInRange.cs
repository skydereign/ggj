using ggj_engine.Source.Entities.Enemies;
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
        private float range;

        public IsPlayerInRange(Enemy enemy, float range)
        {
            this.enemy = enemy;
            this.range = range;
        }

        public bool Test()
        {
            // Check for distance between player position and enemy position.
            // Need to wait for Marco.
            return false;
        }
    }
}
