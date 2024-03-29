﻿using ggj_engine.Source.AI.Actions;
using ggj_engine.Source.AI.Conditions;
using ggj_engine.Source.AI.DecisionTree;
using ggj_engine.Source.AI.Pathing;
using ggj_engine.Source.Collisions;
using ggj_engine.Source.Level;
using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.Enemies
{
    class YourMom : Enemy
    {
        private BinaryDecision playerInSightRange;
        private BinaryDecision playerInCombatRange;
        private BinaryDecision playerTooClose;
        private float sightRange, combatRange, evadeRange;

        public YourMom(Vector2 position)
        {
            Position = position;
        }

        public override void Init()
        {
            PerformingAction = false;
            Speed = 1.5f;
            sprite = ContentLibrary.Sprites["square_enemy"];
            health = 20;
            sprite.Tint = Color.SaddleBrown;
            CollisionRegion = new CircleRegion(12, Position);
            sightRange = 15 * 16;
            combatRange = 10 * 16;
            evadeRange = 8 * 16;
            playerInSightRange = new BinaryDecision();
            playerInCombatRange = new BinaryDecision();
            playerTooClose = new BinaryDecision();
            CurrentPath = new Stack<Tile>();
            wayPoints = new List<Vector2>();
            LoadWayPoints();
            SetDecisionTree();
            base.Init();
        }

        private void LoadWayPoints()
        {
            Vector2 wayPoint1 = new Vector2(TileGrid.Width * 16 - 40, TileGrid.Height * 16 - 40);
            Vector2 wayPoint2 = new Vector2(40, TileGrid.Height * 16 - 40);
            Vector2 wayPoint3 = new Vector2(40, 40);
            Vector2 wayPoint4 = new Vector2(TileGrid.Width * 16 - 40, 40);
            Vector2 wayPoint5 = new Vector2((TileGrid.Width / 2) * 16, (TileGrid.Height / 2) * 16);
            Vector2 wayPoint6 = new Vector2((TileGrid.Width / 3) * 16, (TileGrid.Height / 3) * 16);
            Vector2 wayPoint7 = new Vector2(((TileGrid.Width / 2) + (TileGrid.Width / 4)) * 16, ((TileGrid.Width / 2) + (TileGrid.Width / 4)) * 16);
            wayPoints.Add(wayPoint1);
            wayPoints.Add(wayPoint2);
            wayPoints.Add(wayPoint3);
            wayPoints.Add(wayPoint4);
            wayPoints.Add(wayPoint5);
            wayPoints.Add(wayPoint6);
        }

        protected override void SetDecisionTree()
        {
            playerInSightRange.SetCondition(new IsPlayerInRange(this, MyScreen.GetEntity("Player").ElementAt(0), sightRange));
            playerInSightRange.SetFalseBranch(new PatrolAction(this, wayPoints));
            playerInSightRange.SetTrueBranch(playerInCombatRange);

            playerInCombatRange.SetCondition(new IsInCombatRange(this, MyScreen.GetEntity("Player").ElementAt(0), combatRange, evadeRange));
            playerInCombatRange.SetFalseBranch(playerTooClose);
            playerInCombatRange.SetTrueBranch(new AttackPlayerAction(this));

            playerTooClose.SetCondition(new IsPlayerInRange(this, MyScreen.GetEntity("Player").ElementAt(0), evadeRange));
            playerTooClose.SetFalseBranch(new FollowPlayerAction(this));
            playerTooClose.SetTrueBranch(new RunFromPlayerAction(this));

            base.SetDecisionTree();
        }

        public override void Update(GameTime gameTime)
        {
            if(!PerformingAction)
            {
                playerInSightRange.MakeDecision().DoAction();
            }
            else if(Patrolling || Following || Evading)
            {
                Position += EnemyMovement.MoveTowardsTile(this, CurrentTile);
            }
            else if(Attacking)
            {
                ShootAtPlayer(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void ShootAtPlayer(GameTime gameTime)
        {
            fireCounter += gameTime.ElapsedGameTime.Milliseconds;
            if (fireCounter >= FireDelay)
            {
                Projectiles.Projectile projectile;
                double randomWeaponNumber = RandomUtil.Next(0, 8);
                if (randomWeaponNumber > 0 && randomWeaponNumber < 2)
                {
                    projectile = new Projectiles.Bullet(Position, MyScreen.GetEntity("Player").ElementAt(0).Position - Position, this);
                }
                else if (randomWeaponNumber > 2 && randomWeaponNumber < 4)
                {
                    projectile = new Projectiles.Arrow(Position, MyScreen.GetEntity("Player").ElementAt(0).Position - Position, this);
                }
                else if (randomWeaponNumber > 4 && randomWeaponNumber < 6)
                {
                    projectile = new Projectiles.Cannonball(Position, MyScreen.GetEntity("Player").ElementAt(0).Position - Position, this);
                }
                else
                {
                    projectile = new Projectiles.Rocket(Position, MyScreen.GetEntity("Player").ElementAt(0).Position - Position, this);
                }
                fireCounter = 0;
                PerformingAction = false;
                Attacking = false;
                projectile.Owner = this;
                MyScreen.AddEntity(projectile);
            }
        }
    }
}
