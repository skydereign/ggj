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
        private List<EventTrigger> inputs;
        private Player.Player player;

        public Weapon(Player.Player player)
        {
            this.player = player;
            inputs = new List<EventTrigger>();
            // standard weapon input
            GenerateDefaultInput();
            

            FireDelay = 3;
        }

        public void GenerateDefaultInput()
        {
            EventTrigger.Trigger FireMouse = () => { Fire(MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - Position); };

            inputs.Clear();
            inputs.Add(new EventTrigger(FireMouse, EventTrigger.Type.All, new MouseEvent(MouseEvent.Button.Left, MouseEvent.Types.Held)));

            CurrentProjectile = ProjectileType.Bullet;
            CurrentInputType = InputType.Mouse;
        }

        public void GenerateWeaponInputs()
        {
            inputs.Clear();

            // choose random input
            CurrentInputType = (InputType)RandomUtil.Next((double)InputType.Count);
            EventTrigger.Trigger FireRight = () => { Fire(Globals.Right); };
            EventTrigger.Trigger FireUp =    () => { Fire(Globals.Up);    };
            EventTrigger.Trigger FireLeft =  () => { Fire(Globals.Left);  };
            EventTrigger.Trigger FireDown =  () => { Fire(Globals.Down);  };

            EventTrigger.Trigger FireMouse =       () => { Fire(MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - Position); };
            EventTrigger.Trigger FireMouseInvert = () => { Fire(Position - MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition())); };

            switch (CurrentInputType)
            {
                case InputType.WASD:
                    inputs.Add(new EventTrigger(FireRight, EventTrigger.Type.All, new KeyEvent(Keys.D, KeyEvent.Types.Held)));
                    inputs.Add(new EventTrigger(FireUp, EventTrigger.Type.All, new KeyEvent(Keys.W, KeyEvent.Types.Held)));
                    inputs.Add(new EventTrigger(FireLeft, EventTrigger.Type.All, new KeyEvent(Keys.A, KeyEvent.Types.Held)));
                    inputs.Add(new EventTrigger(FireDown, EventTrigger.Type.All, new KeyEvent(Keys.S, KeyEvent.Types.Held)));
                    break;
                case InputType.Arrows:
                    inputs.Add(new EventTrigger(FireRight, EventTrigger.Type.All, new KeyEvent(Keys.Right, KeyEvent.Types.Held)));
                    inputs.Add(new EventTrigger(FireUp, EventTrigger.Type.All, new KeyEvent(Keys.Up, KeyEvent.Types.Held)));
                    inputs.Add(new EventTrigger(FireLeft, EventTrigger.Type.All, new KeyEvent(Keys.Left, KeyEvent.Types.Held)));
                    inputs.Add(new EventTrigger(FireDown, EventTrigger.Type.All, new KeyEvent(Keys.Down, KeyEvent.Types.Held)));
                    break;
                case InputType.Space:
                    inputs.Add(new EventTrigger(FireMouse, EventTrigger.Type.All, new KeyEvent(Keys.Space, KeyEvent.Types.Held)));
                    break;
                case InputType.SpaceInvert:
                    inputs.Add(new EventTrigger(FireMouseInvert, EventTrigger.Type.All, new KeyEvent(Keys.Space, KeyEvent.Types.Held)));
                    break;
                case InputType.WASDInvert:
                    inputs.Add(new EventTrigger(FireLeft, EventTrigger.Type.All, new KeyEvent(Keys.D, KeyEvent.Types.Held)));
                    inputs.Add(new EventTrigger(FireDown, EventTrigger.Type.All, new KeyEvent(Keys.W, KeyEvent.Types.Held)));
                    inputs.Add(new EventTrigger(FireRight, EventTrigger.Type.All, new KeyEvent(Keys.A, KeyEvent.Types.Held)));
                    inputs.Add(new EventTrigger(FireUp, EventTrigger.Type.All, new KeyEvent(Keys.S, KeyEvent.Types.Held)));
                    break;
                case InputType.ArrowsInvert:
                    inputs.Add(new EventTrigger(FireLeft, EventTrigger.Type.All, new KeyEvent(Keys.Right, KeyEvent.Types.Held)));
                    inputs.Add(new EventTrigger(FireDown, EventTrigger.Type.All, new KeyEvent(Keys.Up, KeyEvent.Types.Held)));
                    inputs.Add(new EventTrigger(FireRight, EventTrigger.Type.All, new KeyEvent(Keys.Left, KeyEvent.Types.Held)));
                    inputs.Add(new EventTrigger(FireUp, EventTrigger.Type.All, new KeyEvent(Keys.Down, KeyEvent.Types.Held)));
                    break;
                case InputType.Mouse:
                    inputs.Add(new EventTrigger(FireMouse, EventTrigger.Type.All, new MouseEvent(MouseEvent.Button.Left, MouseEvent.Types.Held)));
                    break;
                case InputType.MouseInvert:
                    inputs.Add(new EventTrigger(FireMouseInvert, EventTrigger.Type.All, new MouseEvent(MouseEvent.Button.Left, MouseEvent.Types.Held)));
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

        private void Fire(Vector2 targetPos)
        {
            if (currentFireDelay >= FireDelay)
            {
                if (!player.Dead)
                {
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
        }

        public override void Update(GameTime gameTime)
        {

            currentFireDelay++;

            //Update inputs
            foreach (EventTrigger input in inputs)
            {
                input.Update(gameTime);
            }

            base.Update(gameTime);
        }

    }
}
