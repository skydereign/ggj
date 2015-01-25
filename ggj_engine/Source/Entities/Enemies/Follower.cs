using ggj_engine.Source.AI.Actions;
using ggj_engine.Source.AI.Conditions;
using ggj_engine.Source.AI.DecisionTree;
using ggj_engine.Source.AI.Pathing;
using ggj_engine.Source.Level;
using ggj_engine.Source.Media;
using ggj_engine.Source.Screens;
using ggj_engine.Source.Utility;
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

        public Follower(Vector2 position)
        {
            PerformingAction = false;
            Position = position;
            Speed = 1.0f;
            sprite = ContentLibrary.Sprites["test_animation"];
            sprite.Tint = Color.Red;
            sightRange = 2 * 16; // number of tiles * tileSize
            combatRange = 1 * 16;
            playerInSightRange = new BinaryDecision();
            playerInCombatRange = new BinaryDecision();
            CurrentPath = new Stack<Tile>();
            SetDecisionTree();
        }

        protected override void SetDecisionTree()
        {
            Vector2 target = new Vector2(100, 100);
            playerInSightRange.SetCondition(new IsPlayerInRange(this, target, sightRange));
            playerInSightRange.SetFalseBranch(new PatrolAction(this, target));
            playerInSightRange.SetTrueBranch(playerInCombatRange);

            playerInCombatRange.SetCondition(new IsPlayerInRange(this, target, combatRange));
            playerInCombatRange.SetFalseBranch(new FollowPlayerAction(this));
            playerInCombatRange.SetTrueBranch(new AttackPlayerAction(this));
            
            base.SetDecisionTree();
        }

        public override void Update(GameTime gameTime)
        {
            if (!PerformingAction)
            {
                playerInSightRange.MakeDecision().DoAction();
            }
            if(Patrolling)
            {
                Position += EnemyMovement.MoveTowardsTile(this, CurrentTile);
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
        }
    }
}