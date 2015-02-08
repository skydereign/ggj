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
        public enum InputType { WASD, WASD8, Arrows, Space, Mouse, WASDInvert, ArrowsInvert, MouseInvert, SpaceInvert, Count };
        public enum FireType { Pressed, Released, Held, None };

        public ProjectileType CurrentProjectile;
        public InputType CurrentInputType;
        public int FireDelay;
        public List<ProjectileEmitter> Emitters;
        public Entity Owner;

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

        public Weapon(Entity owner)
        {
            Owner = owner;
            inputs = new List<EventTrigger>();
            Emitters = new List<ProjectileEmitter>();

            FireDelay = 3;
        }

        public override void Init()
        {
            GenerateDefaultInput();
            base.Init();
        }

        public void GenerateDefaultInput()
        {
            ProjectileEmitter e = new PistolEmitter(MathExt.DegToRad(0), MathExt.DegToRad(0), 100, 1, 1, Vector2.Zero, this);
            e.States.Add(new Weapons.Trajectories.TrajectoryState(e));
            e.States[0].Update = (Projectile p) => { p.Position.X += (float)Math.Cos(p.InitialAngle)*5;
                                                     p.Position.Y += (float)Math.Sin(p.InitialAngle)*5; };
            Emitters.Add(e);
            MyScreen.AddEntity(e);

            e = new RapidEmitter(0, MathExt.DegToRad(0), 100, 10, 5, Vector2.Zero, this);
            e.States.Add(new Weapons.Trajectories.SinTrajectory(e));
            //e.States[0].Update = (Projectile p) => { p.Position.Y += 0; p.Position.X += 0; };
            Emitters.Add(e);
            MyScreen.AddEntity(e);


            EventTrigger.Trigger FireMousePressed = () => { Fire(MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - Position, FireType.Pressed); };
            EventTrigger.Trigger FireMouseHeld = () => { Fire(MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - Position, FireType.Held); };
            
            inputs.Clear();
            inputs.Add(new EventTrigger(FireMousePressed, EventTrigger.Type.All, new MouseEvent(MouseEvent.Button.Left, MouseEvent.Types.Pressed)));
            inputs.Add(new EventTrigger(FireMouseHeld, EventTrigger.Type.All, new MouseEvent(MouseEvent.Button.Left, MouseEvent.Types.Held)));
            //
            //CurrentProjectile = ProjectileType.Bullet;
            //CurrentInputType = InputType.Mouse;
        }



        private void Fire(Vector2 targetPos, FireType type)
        {
            float angle = (float)Math.Atan2(targetPos.Y, targetPos.X);
            foreach(ProjectileEmitter e in Emitters)
            {
                switch(type)
                {
                    case FireType.Held:
                        e.FireHeld(angle);
                        break;

                    case FireType.None:
                        e.FireNone(angle);
                        break;

                    case FireType.Pressed:
                        e.FirePressed(angle);
                        break;

                    case FireType.Released:
                        e.FireReleased(angle);
                        break;
                }
            }
            //if (currentFireDelay >= FireDelay)
            //{
            //    if (!player.Dead)
            //    {
            //        Network.NetworkManager.Instance.BroadcastEvent(",W," + 0 + ',' + (int)CurrentProjectile + ',' + Position.X + ',' + Position.Y + ',' + targetPos.X + ',' + targetPos.Y + ',');
            //
            //        switch (CurrentProjectile)
            //        {
            //            case ProjectileType.Bullet:
            //                MyScreen.AddEntity(new Bullet(Position, targetPos, MyScreen.GetEntity("Player").ElementAt(0)));
            //                Game1.SoundController.PlaySFX("bullet", false);
            //                break;
            //            case ProjectileType.Arrow:
            //                MyScreen.AddEntity(new Arrow(Position, targetPos, MyScreen.GetEntity("Player").ElementAt(0)));
            //                Game1.SoundController.PlaySFX("bow", false);
            //                break;
            //            case ProjectileType.Cannonball:
            //                MyScreen.AddEntity(new Cannonball(Position, targetPos, MyScreen.GetEntity("Player").ElementAt(0)));
            //                Game1.SoundController.PlaySFX("cannon", false);
            //                break;
            //            case ProjectileType.Rocket:
            //                MyScreen.AddEntity(new Rocket(Position, targetPos, MyScreen.GetEntity("Player").ElementAt(0)));
            //                Game1.SoundController.PlaySFX("rocket", false);
            //                break;
            //        }
            //        currentFireDelay = 0;
            //    }
            //}
        }

        public override void Update(GameTime gameTime)
        {
            Position = Owner.Position;
            if (InputControl.GetKeyboardKeyPressed(Keys.D1))
            {
                CurrentProjectile = ProjectileType.Rocket;
                FireDelay = 30;
            }
            currentFireDelay++;

            //Update inputs
            foreach (EventTrigger input in inputs)
            {
                input.Update(gameTime);
            }

            base.Update(gameTime);
        }



        public void GenerateWeaponInputs()
        {
            //inputs.Clear();
            //
            //// choose random input
            //CurrentInputType = (InputType)RandomUtil.Next((double)InputType.Count);
            //EventTrigger.Trigger FireRight = () => { Fire(Globals.Right); };
            //EventTrigger.Trigger FireUpRight = () => { Fire(Globals.UpRight); };
            //EventTrigger.Trigger FireUp = () => { Fire(Globals.Up); };
            //EventTrigger.Trigger FireUpLeft = () => { Fire(Globals.UpLeft); };
            //EventTrigger.Trigger FireLeft = () => { Fire(Globals.Left); };
            //EventTrigger.Trigger FireDownLeft = () => { Fire(Globals.DownLeft); };
            //EventTrigger.Trigger FireDown = () => { Fire(Globals.Down); };
            //EventTrigger.Trigger FireDownRight = () => { Fire(Globals.DownRight); };
            //
            //EventTrigger.Trigger FireMouse = () => { Fire(MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition()) - Position); };
            //EventTrigger.Trigger FireMouseInvert = () => { Fire(Position - MyScreen.Camera.ScreenToWorld(InputControl.GetMousePosition())); };
            //
            //switch (CurrentInputType)
            //{
            //    case InputType.WASD:
            //        inputs.Add(new EventTrigger(FireRight, EventTrigger.Type.All, new KeyEvent(Keys.D, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireUp, EventTrigger.Type.All, new KeyEvent(Keys.W, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireLeft, EventTrigger.Type.All, new KeyEvent(Keys.A, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireDown, EventTrigger.Type.All, new KeyEvent(Keys.S, KeyEvent.Types.Held)));
            //        break;
            //    case InputType.WASD8:
            //        inputs.Add(new EventTrigger(FireRight, EventTrigger.Type.All, new KeyEvent(Keys.D, KeyEvent.Types.Held), new InvertEvent(new KeyEvent(Keys.W, KeyEvent.Types.Held)), new InvertEvent(new KeyEvent(Keys.S, KeyEvent.Types.Held))));
            //        inputs.Add(new EventTrigger(FireUpRight, EventTrigger.Type.All, new KeyEvent(Keys.D, KeyEvent.Types.Held), new KeyEvent(Keys.W, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireUp, EventTrigger.Type.All, new KeyEvent(Keys.W, KeyEvent.Types.Held), new InvertEvent(new KeyEvent(Keys.D, KeyEvent.Types.Held)), new InvertEvent(new KeyEvent(Keys.A, KeyEvent.Types.Held))));
            //        inputs.Add(new EventTrigger(FireUpLeft, EventTrigger.Type.All, new KeyEvent(Keys.W, KeyEvent.Types.Held), new KeyEvent(Keys.A, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireLeft, EventTrigger.Type.All, new KeyEvent(Keys.A, KeyEvent.Types.Held), new InvertEvent(new KeyEvent(Keys.W, KeyEvent.Types.Held)), new InvertEvent(new KeyEvent(Keys.S, KeyEvent.Types.Held))));
            //        inputs.Add(new EventTrigger(FireDownLeft, EventTrigger.Type.All, new KeyEvent(Keys.A, KeyEvent.Types.Held), new KeyEvent(Keys.S, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireDown, EventTrigger.Type.All, new KeyEvent(Keys.S, KeyEvent.Types.Held), new InvertEvent(new KeyEvent(Keys.A, KeyEvent.Types.Held)), new InvertEvent(new KeyEvent(Keys.D, KeyEvent.Types.Held))));
            //        inputs.Add(new EventTrigger(FireDownRight, EventTrigger.Type.All, new KeyEvent(Keys.S, KeyEvent.Types.Held), new KeyEvent(Keys.D, KeyEvent.Types.Held)));
            //        break;
            //    case InputType.Arrows:
            //        inputs.Add(new EventTrigger(FireRight, EventTrigger.Type.All, new KeyEvent(Keys.Right, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireUp, EventTrigger.Type.All, new KeyEvent(Keys.Up, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireLeft, EventTrigger.Type.All, new KeyEvent(Keys.Left, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireDown, EventTrigger.Type.All, new KeyEvent(Keys.Down, KeyEvent.Types.Held)));
            //        break;
            //    case InputType.Space:
            //        inputs.Add(new EventTrigger(FireMouse, EventTrigger.Type.All, new KeyEvent(Keys.Space, KeyEvent.Types.Held)));
            //        break;
            //    case InputType.SpaceInvert:
            //        inputs.Add(new EventTrigger(FireMouseInvert, EventTrigger.Type.All, new KeyEvent(Keys.Space, KeyEvent.Types.Held)));
            //        break;
            //    case InputType.WASDInvert:
            //        inputs.Add(new EventTrigger(FireLeft, EventTrigger.Type.All, new KeyEvent(Keys.D, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireDown, EventTrigger.Type.All, new KeyEvent(Keys.W, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireRight, EventTrigger.Type.All, new KeyEvent(Keys.A, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireUp, EventTrigger.Type.All, new KeyEvent(Keys.S, KeyEvent.Types.Held)));
            //        break;
            //    case InputType.ArrowsInvert:
            //        inputs.Add(new EventTrigger(FireLeft, EventTrigger.Type.All, new KeyEvent(Keys.Right, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireDown, EventTrigger.Type.All, new KeyEvent(Keys.Up, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireRight, EventTrigger.Type.All, new KeyEvent(Keys.Left, KeyEvent.Types.Held)));
            //        inputs.Add(new EventTrigger(FireUp, EventTrigger.Type.All, new KeyEvent(Keys.Down, KeyEvent.Types.Held)));
            //        break;
            //    case InputType.Mouse:
            //        inputs.Add(new EventTrigger(FireMouse, EventTrigger.Type.All, new MouseEvent(MouseEvent.Button.Left, MouseEvent.Types.Held)));
            //        break;
            //    case InputType.MouseInvert:
            //        inputs.Add(new EventTrigger(FireMouseInvert, EventTrigger.Type.All, new MouseEvent(MouseEvent.Button.Left, MouseEvent.Types.Held)));
            //        break;
            //}
            //
            //// choose random bullet type
            //CurrentProjectile = (ProjectileType)RandomUtil.Next((double)ProjectileType.Count);
            //switch (CurrentProjectile)
            //{
            //    case ProjectileType.Bullet:
            //        FireDelay = (int)RandomUtil.Next(minBulletFire, maxBulletFire);
            //        break;
            //    case ProjectileType.Arrow:
            //        FireDelay = (int)RandomUtil.Next(minArrowFire, maxArrowFire);
            //        break;
            //    case ProjectileType.Cannonball:
            //        FireDelay = (int)RandomUtil.Next(minCannonballFire, maxCannonballFire);
            //        if (RandomUtil.Next() > 0.9f)
            //        {
            //            FireDelay = (int)(minCannonballFire * 0.25f);
            //        }
            //        break;
            //    case ProjectileType.Rocket:
            //        FireDelay = (int)RandomUtil.Next(minRocketFire, maxRocketFire);
            //        break;
            //}
        }
    }
}
