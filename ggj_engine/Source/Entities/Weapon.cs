using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Entities.Projectiles;

namespace ggj_engine.Source.Entities
{
    class Weapon : Entity
    {
        public enum ProjectileType { Bullet, Arrow, Cannonball, Rocket };

        public ProjectileType CurrentProjectile;
        public int FireDelay;

        private const float minBulletFire = 3;
        private const float maxBulletFire = 3;
        private const float minArrowFire = 10;
        private const float maxArrowFire =30; 
        private const float minCannonballFire = 40;
        private const float maxCannonballFire = 80; 
        private const float minRocketFire = 80;
        private const float maxRocketFire = 120;

        private int currentFireDelay = 0;

        public Weapon()
        {
            FireDelay = 3;
        }

        public override void Update(GameTime gameTime)
        {
            if (InputControl.GetKeyboardKeyPressed(Microsoft.Xna.Framework.Input.Keys.D1))
            {
                CurrentProjectile = ProjectileType.Bullet;
                FireDelay = (int)RandomUtil.Next(minBulletFire, maxBulletFire);
            }
            if (InputControl.GetKeyboardKeyPressed(Microsoft.Xna.Framework.Input.Keys.D2))
            {
                CurrentProjectile = ProjectileType.Arrow;
                FireDelay = (int)RandomUtil.Next(minArrowFire, maxArrowFire);
            }
            if (InputControl.GetKeyboardKeyPressed(Microsoft.Xna.Framework.Input.Keys.D3))
            {
                CurrentProjectile = ProjectileType.Cannonball;
                FireDelay = (int)RandomUtil.Next(minCannonballFire, maxCannonballFire);
                if (RandomUtil.Next() > 0.9f)
                {
                    FireDelay = (int)(minCannonballFire * 0.25f);
                }
            }
            if (InputControl.GetKeyboardKeyPressed(Microsoft.Xna.Framework.Input.Keys.D4))
            {
                CurrentProjectile = ProjectileType.Rocket;
                FireDelay = (int)RandomUtil.Next(minRocketFire, maxRocketFire);
            }

            currentFireDelay++;
            if (InputControl.GetMouseOnLeftHeld() && currentFireDelay >= FireDelay)
            {
                switch (CurrentProjectile)
                {
                    case ProjectileType.Bullet:
                        MyScreen.AddEntity(new Bullet(Position, MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - Position));
                        break;
                    case ProjectileType.Arrow:
                        MyScreen.AddEntity(new Arrow(Position, MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - Position));
                        break;
                    case ProjectileType.Cannonball:
                        MyScreen.AddEntity(new Cannonball(Position, MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - Position));
                        break;
                    case ProjectileType.Rocket:
                        MyScreen.AddEntity(new Rocket(Position, MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - Position));
                        break;
                }
                currentFireDelay = 0;
            }
            base.Update(gameTime);
        }

    }
}
