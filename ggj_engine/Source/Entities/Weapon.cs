using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Entities.Projectiles;
using ggj_engine.Source.Weapons.Inputs;
using Microsoft.Xna.Framework.Input;

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
        private const float minRocketFire = 20;
        private const float maxRocketFire = 60;

        private int currentFireDelay = 0;
        private List<EquipInput> inputs;

        public Weapon()
        {
            inputs = new List<EquipInput>();
            inputs.Add(new EquipMouseInput(EquipMouseInput.Types.Held, EquipMouseInput.Button.Left, Fire));
            inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Pressed, Keys.Space, Fire));

            FireDelay = 3;
        }

        public void Fire()
        {
            if (currentFireDelay >= FireDelay)
            {
                Vector2 targetPos = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - Position;
                
                Network.NetworkManager.Instance.BroadcastEvent(",W," + 0 + ',' + (int)CurrentProjectile + ',' + Position.X + ',' + Position.Y + ',' + targetPos.X + ',' + targetPos.Y + ',');

                switch (CurrentProjectile)
                {
                    case ProjectileType.Bullet:
                        MyScreen.AddEntity(new Bullet(Position, targetPos));
                        Game1.SoundController.PlaySFX("bullet", false);
                        break;
                    case ProjectileType.Arrow:
                        MyScreen.AddEntity(new Arrow(Position, targetPos));
                        Game1.SoundController.PlaySFX("bow", false);
                        break;
                    case ProjectileType.Cannonball:
                        MyScreen.AddEntity(new Cannonball(Position, targetPos));
                        Game1.SoundController.PlaySFX("cannon", false);
                        break;
                    case ProjectileType.Rocket:
                        MyScreen.AddEntity(new Rocket(Position, targetPos));
                        Game1.SoundController.PlaySFX("rocket", false);
                        break;
                }
                currentFireDelay = 0;
            }
        }

        public override void Update(GameTime gameTime)
        {
            //TEMP weapon switching
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

            //Update inputs
            foreach (EquipInput input in inputs)
            {
                input.Update(gameTime);
            }

            base.Update(gameTime);
        }

    }
}
