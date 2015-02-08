using ggj_engine.Source.Collisions;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.Projectiles
{
    class ChargeEmitter : ProjectileEmitter
    {
        public float Charge;
        public float MaxCharge;

        public ChargeEmitter(float maxCharge, float offset, float accuracy, int maxFirstProjectiles, float fireTimer, float fireIncrement, Vector2 positionOffset, Weapon owner) :
            base(offset, accuracy, maxFirstProjectiles, positionOffset, owner)
        {
            MaxCharge = maxCharge;
        }

        public override void FireHeld(float angle)
        {
            Charge += (Charge < MaxCharge) ? 1 : 0;
            base.FireHeld(angle);
        }

        public override void FireReleased(float angle)
        {
            Projectile p = new Bullet(Position, Globals.Right, owner.Owner);
            p.Parent = this;
            CircleRegion c = (CircleRegion)p.CollisionRegion;
            c.Radius = (int)Charge;
            p.InitialAngle = (float)(angle + AngleOffset + RandomUtil.Next(-Accuracy / 2, Accuracy / 2));
            MyScreen.AddEntity(p);
            AddProjectile(p);
            Charge = 0;
            base.FireReleased(angle);
        }
    }
}
