using ggj_engine.Source.Entities;
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
        private Entity entity;
        private float range;

        public IsPlayerInRange(Enemy enemy, Entity entity, float range)
        {
            this.enemy = enemy;
            this.range = range;
            this.entity = entity;
        }

        public bool Test()
        {
            if(Vector2.Distance(enemy.Position, entity.Position) < range)
            {
                return true;
            }
            return false;
        }
    }
}
