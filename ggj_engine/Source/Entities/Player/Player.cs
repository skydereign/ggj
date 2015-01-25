using ggj_engine.Source.Collisions;
using ggj_engine.Source.Media;
using ggj_engine.Source.Movement;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.Player
{
    class Player : Entity
    {
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
            Shield = new Shield();

        }

        public override void Update(GameTime gameTime)
        {
            if (Weapon == null)
            {
                Weapon = new Weapon();
                MyScreen.AddEntity(Weapon);
            }


            Weapon.Position = Position;

            Vector2 mousePosition = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());
            Position = movementManager.Update(gameTime, Position, mousePosition);

            Shield.Position = Position;
            Shield.Update();

            //Make camera follow player
            MyScreen.Camera.Position = Position;

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Shield.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public override void OnCollision(Entity other)
        {
            
            base.OnCollision(other);
        }
    }
}
