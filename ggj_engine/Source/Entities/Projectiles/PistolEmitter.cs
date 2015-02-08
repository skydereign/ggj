using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.Projectiles
{
    class PistolEmitter : ProjectileEmitter
    {
        public PistolEmitter(int offset, int accuracy, int maxFirstProjectiles, float fireTimer, float fireIncrement, Vector2 positionOffset, Weapon owner) :
            base(offset, accuracy, maxFirstProjectiles, fireTimer, fireIncrement, positionOffset, owner)
        {
            //
        }


        public override void FireHeld(float angle)
        {
            throw new NotImplementedException();
        }

        public override void FireNone(float angle)
        {
            throw new NotImplementedException();
        }

        public override void FirePressed(float angle)
        {
            timer += FireTimerInc;
            while(timer > FireTimerMax)
            {
                Projectile p = new Bullet(Position, Globals.Right, owner.Owner);
                p.Parent = this;
                MyScreen.AddEntity(p);
                timer -= FireTimerMax;
            }
        }

        public override void FireReleased(float angle)
        {
            throw new NotImplementedException();
        }
    }
}
