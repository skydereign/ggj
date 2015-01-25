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
            Position = position;
        }

        public override void Init()
        {
            PerformingAction = false;
            Speed = 1.0f;
            sprite = ContentLibrary.Sprites["test_animation"];
            sprite.Tint = Color.Red;
            sightRange = 4 * 16; // number of tiles * tileSize
            combatRange = 2 * 16;
            playerInSightRange = new BinaryDecision();
            playerInCombatRange = new BinaryDecision();
            CurrentPath = new Stack<Tile>();
            wayPoints = new List<Vector2>();
            LoadWayPoints();
            SetDecisionTree();
            base.Init();
        }

        private void LoadWayPoints()
        {
            Vector2 wayPoint1 = new Vector2(100, 100);
            Vector2 wayPoint2 = new Vector2(40, 40);
            Vector2 wayPoint3 = new Vector2(40, 100);
            Vector2 wayPoint4 = new Vector2(100, 40);
            wayPoints.Add(wayPoint1);
            wayPoints.Add(wayPoint2);
            wayPoints.Add(wayPoint3);
            wayPoints.Add(wayPoint4);
        }

        protected override void SetDecisionTree()
        {
            playerInSightRange.SetCondition(new IsPlayerInRange(this, MyScreen.GetEntity("Player").ElementAt(0), sightRange));
            playerInSightRange.SetFalseBranch(new PatrolAction(this, wayPoints));
            playerInSightRange.SetTrueBranch(playerInCombatRange);

            playerInCombatRange.SetCondition(new IsPlayerInRange(this, MyScreen.GetEntity("Player").ElementAt(0), combatRange));
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
            if(Patrolling || Following)
            {
                Position += EnemyMovement.MoveTowardsTile(this, CurrentTile);
            }
            if(Attacking)
            {

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