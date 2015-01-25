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
    public class Weapon : Entity
    {
        public enum ProjectileType { Bullet, Arrow, Cannonball, Rocket, Count };
        public enum InputType { WASD, Arrows, Space, Mouse, WASDInvert, ArrowsInvert, MouseInvert, SpaceInvert, Count };

        public ProjectileType CurrentProjectile;
        public InputType CurrentInputType;
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
            // standard weapon input
            GenerateDefaultInput();
            

            FireDelay = 3;
        }

        public void GenerateDefaultInput()
        {
            inputs.Clear();
            inputs.Add(new EquipMouseInput(EquipMouseInput.Types.Held, EquipMouseInput.Button.Left, FireMouse));

            CurrentProjectile = ProjectileType.Bullet;
            CurrentInputType = InputType.Mouse;
        }

        public void GenerateWeaponInputs()
        {
            inputs.Clear();

            // choose random input
            CurrentInputType = (InputType)RandomUtil.Next((double)InputType.Count);
            switch (CurrentInputType)
            {
                case InputType.WASD:
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.W, FireUp));
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.A, FireLeft));
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.S, FireDown));
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.D, FireRight));
                    break;
                case InputType.Arrows:
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.Up, FireUp));
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.Left, FireLeft));
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.Down, FireDown));
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.Right, FireRight));
                    break;
                case InputType.Space:
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.Space, FireMouse));
                    break;
                case InputType.SpaceInvert:
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.Space, FireMouseInvert));
                    break;
                case InputType.WASDInvert:
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.W, FireDown));
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.A, FireRight));
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.S, FireUp));
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.D, FireLeft));
                    break;
                case InputType.ArrowsInvert:
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.Up, FireDown));
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.Left, FireRight));
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.Down, FireUp));
                    inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Held, Keys.Right, FireLeft));
                    break;
                case InputType.Mouse:
                    inputs.Add(new EquipMouseInput(EquipMouseInput.Types.Held, EquipMouseInput.Button.Left, FireMouse));
                    break;
                case InputType.MouseInvert:
                    inputs.Add(new EquipMouseInput(EquipMouseInput.Types.Held, EquipMouseInput.Button.Left, FireMouseInvert));
                    break;
            }

            // choose random bullet type
            CurrentProjectile = (ProjectileType)RandomUtil.Next((double)ProjectileType.Count);
            switch(CurrentProjectile) {
                case ProjectileType.Bullet:
                    FireDelay = (int)RandomUtil.Next(minBulletFire, maxBulletFire);
                    break;
                case ProjectileType.Arrow:
                    FireDelay = (int)RandomUtil.Next(minArrowFire, maxArrowFire);
                    break;
                case ProjectileType.Cannonball:
                    FireDelay = (int)RandomUtil.Next(minCannonballFire, maxCannonballFire);
                    if (RandomUtil.Next() > 0.9f)
                    {
                        FireDelay = (int)(minCannonballFire * 0.25f);
                    }
                    break;
                case ProjectileType.Rocket:
                    FireDelay = (int)RandomUtil.Next(minRocketFire, maxRocketFire);
                    break;
            }
        }

        public void FireMouse()
        {
            if (currentFireDelay >= FireDelay)
            {
                Vector2 targetPos = MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - Position;
                Fire(targetPos);
                
                Network.NetworkManager.Instance.BroadcastEvent(",W," + 0 + ',' + (int)CurrentProjectile + ',' + Position.X + ',' + Position.Y + ',' + targetPos.X + ',' + targetPos.Y + ',');

                switch (CurrentProjectile)
                {
                    case ProjectileType.Bullet:
                        MyScreen.AddEntity(new Bullet(Position, targetPos, MyScreen.GetEntity("Player").ElementAt(0)));
                        Game1.SoundController.PlaySFX("bullet", false);
                        break;
                    case ProjectileType.Arrow:
                        MyScreen.AddEntity(new Arrow(Position, targetPos, MyScreen.GetEntity("Player").ElementAt(0)));
                        Game1.SoundController.PlaySFX("bow", false);
                        break;
                    case ProjectileType.Cannonball:
                        MyScreen.AddEntity(new Cannonball(Position, targetPos, MyScreen.GetEntity("Player").ElementAt(0)));
                        Game1.SoundController.PlaySFX("cannon", false);
                        break;
                    case ProjectileType.Rocket:
                        MyScreen.AddEntity(new Rocket(Position, targetPos, MyScreen.GetEntity("Player").ElementAt(0)));
                        Game1.SoundController.PlaySFX("rocket", false);
                        break;
                }
                currentFireDelay = 0;
            }
        }

        public void FireMouseInvert()
        {
            if (currentFireDelay >= FireDelay)
            {
                Vector2 targetPos = Position - MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition());

                Fire(targetPos);
            }
        }

        public void FireUp()
        {
            if (currentFireDelay >= FireDelay)
            {
                Vector2 targetPos = new Vector2(0f, -20f);

                Fire(targetPos);
            }
        }

        public void FireLeft()
        {
            if (currentFireDelay >= FireDelay)
            {
                Vector2 targetPos = new Vector2(-20f, 0f);

                Fire(targetPos);
            }
        }

        public void FireDown()
        {
            if (currentFireDelay >= FireDelay)
            {
                Vector2 targetPos = new Vector2(0f, 20f);

                Fire(targetPos);
            }
        }

        public void FireRight()
        {
            if (currentFireDelay >= FireDelay)
            {
                Vector2 targetPos = new Vector2(20f, 0f);

                Fire(targetPos);
            }
        }

        private void Fire(Vector2 targetPos)
        {
            Network.NetworkManager.Instance.BroadcastEvent(",W," + 0 + ',' + (int)CurrentProjectile + ',' + Position.X + ',' + Position.Y + ',' + targetPos.X + ',' + targetPos.Y + ',');

            switch (CurrentProjectile)
            {
                case ProjectileType.Bullet:
                    MyScreen.AddEntity(new Bullet(Position, targetPos, MyScreen.GetEntity("Player").ElementAt(0)));
                    break;
                case ProjectileType.Arrow:
                    MyScreen.AddEntity(new Arrow(Position, targetPos, MyScreen.GetEntity("Player").ElementAt(0)));
                    break;
                case ProjectileType.Cannonball:
                    MyScreen.AddEntity(new Cannonball(Position, targetPos, MyScreen.GetEntity("Player").ElementAt(0)));
                    break;
                case ProjectileType.Rocket:
                    MyScreen.AddEntity(new Rocket(Position, targetPos, MyScreen.GetEntity("Player").ElementAt(0)));
                    break;
            }
            currentFireDelay = 0;
        }

        public override void Update(GameTime gameTime)
        {

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
