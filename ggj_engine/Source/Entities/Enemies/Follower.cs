﻿using ggj_engine.Source.AI.Actions;
using ggj_engine.Source.AI.Conditions;
using ggj_engine.Source.AI.DecisionTree;
using ggj_engine.Source.AI.Pathing;
using ggj_engine.Source.Collisions;
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
            Speed = 3.0f;
            sprite = ContentLibrary.Sprites["triangle_enemy"];
            health = 25;
            sprite.Tint = Color.Red;
            CollisionRegion = new CircleRegion(12, Position);
            sightRange = 12 * 16;
            combatRange = 6 * 16;
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
            Vector2 wayPoint1 = new Vector2(40, TileGrid.Height * 16 - 40);
            Vector2 wayPoint2 = new Vector2(TileGrid.Width * 16 - 40, TileGrid.Height * 16 - 40);
            Vector2 wayPoint3 = new Vector2(TileGrid.Width * 16 - 40, 40);
            Vector2 wayPoint4 = new Vector2(40, 40);
            Vector2 wayPoint5 = new Vector2(((TileGrid.Width / 2) + (TileGrid.Width / 4)) * 16, ((TileGrid.Width / 2) + (TileGrid.Width / 4)) * 16); 
            Vector2 wayPoint6 = new Vector2((TileGrid.Width / 3) * 16, (TileGrid.Height / 3) * 16);
            Vector2 wayPoint7 = new Vector2((TileGrid.Width / 2) * 16, (TileGrid.Height / 2) * 16);
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
            else if(Patrolling || Following)
            {
                Position += EnemyMovement.MoveTowardsTile(this, CurrentTile);
            }
            else if(Attacking)
            {
                ShootAtPlayer(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void ShootAtPlayer(GameTime gameTime)
        {
            fireCounter += gameTime.ElapsedGameTime.Milliseconds;
            if(fireCounter >= FireDelay)
            {
                Projectiles.Projectile projectile;
                double randomWeaponNumber = RandomUtil.Next(0, 8);
                if(randomWeaponNumber > 0 && randomWeaponNumber < 2)
                {
                    projectile = new Projectiles.Bullet(Position, MyScreen.GetEntity("Player").ElementAt(0).Position - Position, this);
                }
                else if(randomWeaponNumber > 2 && randomWeaponNumber < 4)
                {
                    projectile = new Projectiles.Arrow(Position, MyScreen.GetEntity("Player").ElementAt(0).Position - Position, this);
                }
                else if(randomWeaponNumber > 4 && randomWeaponNumber < 6)
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
                MyScreen.AddEntity(projectile);
            }
        }
    }
}