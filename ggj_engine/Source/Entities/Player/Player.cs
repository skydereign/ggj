using ggj_engine.Source.Collisions;
using ggj_engine.Source.Level;
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
        private static int playerCount = 0;
        public int PlayerID;

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
            Shield = new Shield();
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


            Weapon.Position = Position;

            Vector2 mousePosition = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());
            Position = TileGrid.AdjustedForCollisions(Position, movementManager.Update(gameTime, Position, mousePosition), (CircleRegion)CollisionRegion);

            Shield.Position = Position;
            Shield.Update();
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
            
            base.OnCollision(other);
        }
    }
}
