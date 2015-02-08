using ggj_engine.Source.Weapons.Trajectories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.Projectiles
{
    public abstract class ProjectileEmitter : Entity
    {
        public int AngleOffset;
        public int Accuracy;
        public int MaxFirstProjectiles; // max number of projectiles that can exist in the first state
        public float FireTimerMax; // count that the timer must reach to fire
        public float FireTimerInc; // increases FireTimer by this amount on each call to fire
        protected float timer;

        // public list of TrajectoryStates
        public List<TrajectoryState> States;
        public List<Projectile> Projectiles;
        private List<Projectile> addedProjectiles;
        private List<Projectile> removedProjectiles;
        protected Weapon owner;
        private Vector2 positionOffset;

        public ProjectileEmitter(int offset, int accuracy, int maxFirstProjectiles, float fireTimer, float fireIncrement, Vector2 positionOffset, Weapon owner)
        {
            AngleOffset = offset;
            Accuracy = accuracy;
            MaxFirstProjectiles = maxFirstProjectiles;
            FireTimerMax = fireTimer;
            FireTimerInc = fireIncrement;

            States = new List<TrajectoryState>();
            Projectiles = new List<Projectile>();
            addedProjectiles = new List<Projectile>();
            removedProjectiles = new List<Projectile>();

            this.positionOffset = positionOffset;
            this.owner = owner;
        }

        // Note pairs FireNone/FireHeld and FirePressed/FireReleased can be switched
        abstract public void FireNone(float angle);
        abstract public void FireHeld(float angle);
        abstract public void FirePressed(float angle);
        abstract public void FireReleased(float angle);

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Position = owner.Position + positionOffset;
            base.Update(gameTime);
        }


        public void AddProjectile(Projectile p)
        {
            addedProjectiles.Add(p);
        }

        public void RemoveProjectile(Projectile p)
        {
            removedProjectiles.Add(p);
        }
    }
}
