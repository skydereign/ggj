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
        public float FireTimerInc;
        public float FireTimerMax;
        protected float timer;

        public PistolEmitter(float offset, float accuracy, int maxFirstProjectiles, float fireTimer, float fireIncrement, Vector2 positionOffset, Weapon owner) :
            base(offset, accuracy, maxFirstProjectiles, positionOffset, owner)
        {
            FireTimerInc = fireIncrement;
            FireTimerMax = fireTimer;
        }

        public override void FirePressed(float angle)
        {
            timer += FireTimerInc;
            while(timer > FireTimerMax)
            {
                Projectile p = new Bullet(Position, Globals.Right, owner.Owner);
                p.Parent = this;
                p.InitialAngle = (float)(angle + AngleOffset + RandomUtil.Next(-Accuracy/2, Accuracy/2));
                MyScreen.AddEntity(p);
                AddProjectile(p);
                timer -= FireTimerMax;
            }
        }
    }
}
