using ggj_engine.Source.Collisions;
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
    class Player : Entity
    {
        public Weapon Weapon;

        private MovementManager movementManager;

        public Player(Vector2 position)
        {
            sprite = ContentLibrary.Sprites["test_animation"];
            Position = position + new Vector2(12, 12);
            CollisionRegion = new CircleRegion(14, position);
            movementManager = new MovementManager();
        }

        public override void Update(GameTime gameTime)
        {
            if (Weapon == null)
            {
                Weapon = new Weapon();
                MyScreen.AddEntity(Weapon);
            }

            if (InputControl.GetKeyboardKeyPressed(Keys.P))
            {
                movementManager.GenerateNewMovement();
            }

            Weapon.Position = Position;

            Vector2 mousePosition = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - new Vector2(sprite.Width/2, sprite.Height/2);

            Position = movementManager.Update(gameTime, Position, mousePosition);
            
            //Make camera follow player
            MyScreen.Camera.Position = Position;

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
