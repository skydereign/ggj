using ggj_engine.Source.Collisions;
using ggj_engine.Source.Entities.Projectiles;
using ggj_engine.Source.Level;
using ggj_engine.Source.Media;
using ggj_engine.Source.Movement;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.Player
{
    public class Player : Entity
    {
        public static int DEAD_LENGTH = 180;

        private static int playerCount = 0;
        public int PlayerID;
        public bool Dead;
        private int deadTimer;

        public bool NetPlayer = false;

        public Weapon Weapon;
        public Shield Shield;
        public Particles.PSystemMove MovementParticles;

        private MovementManager movementManager;

        public Player(Vector2 position)
        {
            Position = position;
            sprite = ContentLibrary.Sprites["square_enemy"];
            CollisionRegion = new CircleRegion(14, position);

            PlayerID = playerCount++;
            movementManager = new MovementManager(this);
            Shield = new Shield(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (MovementParticles == null)
            {
                UpdateMovementParticleType();
            }

            if (Weapon == null)
            {
                Weapon = new Weapon(this);
                MyScreen.AddEntity(Weapon);
            }

            if (InputControl.GetKeyboardKeyPressed(Keys.P))
            {
                movementManager.GenerateNewMovement();
                Weapon.GenerateWeaponInputs();
                UpdateMovementParticleType();
            }

            Weapon.Position = Position;

            Vector2 mousePosition = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());
            if (!Dead)
            {
                Position = TileGrid.AdjustedForCollisions(this, Position, movementManager.Update(gameTime, Position, mousePosition), (CircleRegion)CollisionRegion);
                MovementParticles.Position = Position;
            }

            Shield.Position = Position;
            Shield.Update();
            if (Shield.Health <= 0 && deadTimer <= 0)
            {
                Dead = true;
                deadTimer = DEAD_LENGTH;
            }
            if (Dead)
            {
                deadTimer--;
            }
            if (Dead && deadTimer <= 0)
            {
                Dead = false;
                Shield.ResetShield();
                List<Entity> spawnPoints = MyScreen.GetEntity("Spawn");
                Position = spawnPoints[(int)RandomUtil.Next(spawnPoints.Count)].Position;
            }


            //Make camera follow player
            //Lots of smoothing, including inching toward the mouse position to make things seem more dynamic
            MyScreen.Camera.Position += ((Position + (MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - Position) * 0.025f) - MyScreen.Camera.Position) * 0.4f;


            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Shield.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public override void OnCollision(Entity other)
        {
            if (other is Projectile)
            {
                if (((Projectile)other).Owner is Enemies.Enemy || (Projectile)other is Explosion || ((Projectile)other).Owner is TestEntity)
                {
                    Shield.Damage(other, ((Projectile)other).Damage);
                }
                if (((Projectile)other).Owner != this)
                {
                    MyScreen.DeleteEntity(other);
                }
            }
            base.OnCollision(other);
        }

        public override void OnTileCollision()
        {
            base.OnTileCollision();

            movementManager.StopVelocity();
        }

        public void ChangeMovementAndWeapon()
        {
            movementManager.GenerateNewMovement();
            Weapon.GenerateWeaponInputs();
            UpdateMovementParticleType();
        }

        private void UpdateMovementParticleType()
        {
            if (MovementParticles != null)
            {
                MovementParticles.Kill(60);
            }

            MovementManager.MovementTypes mType = movementManager.GetMoveType();
            MovementKeyInput.Types iType = movementManager.GetInputType();

            if (mType == MovementManager.MovementTypes.Standard)
            {
                if (iType == MovementDelegate.Types.Held)
                {
                    MovementParticles = new Particles.PSystemStandardMove();
                }
                if (iType == MovementDelegate.Types.Pressed || iType == MovementDelegate.Types.Released)
                {
                    MovementParticles = new Particles.PSystemBurstMove();
                }
            }
            else if (mType == MovementManager.MovementTypes.Thrusters)
            {
                if (iType == MovementDelegate.Types.Held)
                {
                    MovementParticles = new Particles.PSystemThrustMove();
                }
                if (iType == MovementDelegate.Types.Pressed || iType == MovementDelegate.Types.Released)
                {
                    MovementParticles = new Particles.PSystemBurstMove();
                }
            }
            else if (mType == MovementManager.MovementTypes.Mouse)
            {
                if (iType == MovementDelegate.Types.Held)
                {
                    MovementParticles = new Particles.PSystemStandardMove();
                }
                if (iType == MovementDelegate.Types.Pressed || iType == MovementDelegate.Types.Released)
                {
                    MovementParticles = new Particles.PSystemStandardMove();
                }
            }
            MyScreen.AddEntity(MovementParticles);
        }
    }
}
