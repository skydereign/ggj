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

        private MovementManager movementManager;

        public Player(Vector2 position)
        {
            Position = position;
            sprite = ContentLibrary.Sprites["test_animation"];
            sprite.CenterOrigin();
            CollisionRegion = new CircleRegion(14, position);
            movementManager = new MovementManager();

            PlayerID = playerCount++;
            movementManager = new MovementManager();

            Shield = new Shield(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (Weapon == null)
            {
                Weapon = new Weapon(this);
                MyScreen.AddEntity(Weapon);
            }

            if (InputControl.GetKeyboardKeyPressed(Keys.P))
            {
                movementManager.GenerateNewMovement();
                Weapon.GenerateWeaponInputs();
            }

            Weapon.Position = Position;

            Vector2 mousePosition = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());
            if (!Dead)
            {
                Position = TileGrid.AdjustedForCollisions(this, Position, movementManager.Update(gameTime, Position, mousePosition), (CircleRegion)CollisionRegion);
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
                Shield.Health = 100;
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
                // hit
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
        }
    }
}
