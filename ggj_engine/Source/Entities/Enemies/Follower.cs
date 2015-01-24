using ggj_engine.Source.AI.Actions;
using ggj_engine.Source.AI.Conditions;
using ggj_engine.Source.AI.DecisionTree;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.Enemies
{
    class Follower : Enemy
    {

        private BinaryDecision playerInSightRange;
        private BinaryDecision playerInCombatRange;
        private float sightRange, combatRange;

        public Follower()
        {
            sightRange = 8 * 16; // number of tiles * tileSize
            combatRange = 4 * 16;
            playerInSightRange = new BinaryDecision();
            playerInCombatRange = new BinaryDecision();
        }

        protected override void SetDecisionTree()
        {
            playerInSightRange.SetCondition(new IsPlayerInRange(this, sightRange));
            playerInSightRange.SetFalseBranch(new PatrolAction(this));
            playerInSightRange.SetTrueBranch(playerInCombatRange);

            playerInCombatRange.SetCondition(new IsPlayerInRange(this, combatRange));
            playerInCombatRange.SetFalseBranch(new FollowPlayerAction(this));
            playerInCombatRange.SetTrueBranch(new AttackPlayerAction(this));
            
            base.SetDecisionTree();
        }

        public override void Update(GameTime gameTime)
        {
            playerInSightRange.MakeDecision().DoAction();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
