using ggj_engine.Source.Entities;
using ggj_engine.Source.Entities.Enemies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.AI.Conditions
{
    class IsInCombatRange : ICondition
    {
        private Enemy enemy;
        private Entity entity;
        private float farRange, closeRange;

        public IsInCombatRange(Enemy enemy, Entity entity, float farRange, float closeRange)
        {
            this.enemy = enemy;
            this.entity = entity;
            this.farRange = farRange;
            this.closeRange = closeRange;
        }
        public bool Test()
        {
            if(Vector2.Distance(enemy.Position, entity.Position) < farRange &&
                Vector2.Distance(enemy.Position, entity.Position) > closeRange)
            {
                return true;
            }
            return false;
        }
    }
}
