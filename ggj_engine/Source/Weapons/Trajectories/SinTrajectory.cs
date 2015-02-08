using ggj_engine.Source.Entities.Projectiles;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons.Trajectories
{
    class SinTrajectory : TrajectoryState
    {
        public SinTrajectory(ProjectileEmitter e) : base(e)
        {
            Update = pUpdate;
        }

        private void pUpdate(Projectile p)
        {
            float offset = (float)((p.Timer / 30f)*Math.PI*2f);
            Vector2 sinOffset = new Vector2(0, (float)Math.Sin(offset) * 40);
            Vector2 distance = new Vector2(p.Timer*5, 0);
            p.Position = p.StartStatePosition + distance.Rotate(p.InitialAngle) + sinOffset.Rotate(p.InitialAngle);
        }
    }
}
