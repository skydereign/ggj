using ggj_engine.Source.Collisions;
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
    class Player : Entity
    {
        private static int playerCount = 0;
        public int PlayerID;

        public bool NetPlayer = false;

        public Weapon Weapon;

        private MovementManager movementManager;

        public Player(Vector2 position)
        {
            sprite = ContentLibrary.Sprites["test_animation"];
            Position = position + new Vector2(12, 12);
            CollisionRegion = new CircleRegion(14, position);
            movementManager = new MovementManager();

            PlayerID = playerCount++;
        }

        public override void Update(GameTime gameTime)
       { 
            if (Weapon == null)
            {
                Weapon = new Weapon();
                MyScreen.AddEntity(Weapon);
            }
            Vector2 a = Position;
            Vector2 b = MyScreen.GetEntity("TestEntity").ElementAt(0).Position;
            //Vector2 a = new Vector2(185, 45);
            //Vector2 b = new Vector2(130, 110);
            Console.WriteLine("player = " + a);
            Console.WriteLine("test entity = " + b);
            Console.WriteLine(MathExt.Direction(a, b)*180/Math.PI);
            Console.WriteLine("");

            if (InputControl.GetKeyboardKeyPressed(Keys.P))
            {
                movementManager.GenerateNewMovement();
            }

            Weapon.Position = Position;

            Vector2 mousePosition = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());
            Position = TileGrid.AdjustedForCollisions(Position, movementManager.Update(gameTime, Position, mousePosition), (CircleRegion)CollisionRegion);
            
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
