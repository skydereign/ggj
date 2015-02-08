using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.Projectiles
{
    class LaserEmitter : ProjectileEmitter
    {
        Projectile projectile;
        public LaserEmitter(float offset, float accuracy, int maxFirstProjectiles, float fireTimer, float fireIncrement, Vector2 positionOffset, Weapon owner) :
            base(offset, accuracy, maxFirstProjectiles, positionOffset, owner)
        {
            //
        }

        public override void FirePressed(float angle)
        {
            projectile = new Bullet(Position, Globals.Right, owner.Owner);
            projectile.Position = Position + (Globals.Right * 50f).Rotate(angle);
            projectile.Parent = this;
            projectile.InitialAngle = (float)(angle + AngleOffset + RandomUtil.Next(-Accuracy / 2, Accuracy / 2));
            MyScreen.AddEntity(projectile);
            AddProjectile(projectile);
            base.FirePressed(angle);
        }

        public override void FireHeld(float angle)
        {
            projectile.Position = Position + (Globals.Right * 50f).Rotate(angle);
            base.FireHeld(angle);
        }

        public override void FireReleased(float angle)
        {
            RemoveProjectile(projectile);
            base.FireReleased(angle);
        }
    }
}
