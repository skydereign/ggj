﻿using ggj_engine.Source.Collisions;
using ggj_engine.Source.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities
{
    class TestEntity : Entity
    {
        public TestEntity(Vector2 position)
        {
            Position = position;
            sprite = ContentLibrary.Sprites["test_animation"];
            CollisionRegion = new CircleRegion(24, position);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void OnCollision(Entity other)
        {
            Console.WriteLine("Colliding");
            Position.X += 0.1f;
            base.OnCollision(other);
        }
    }
}
