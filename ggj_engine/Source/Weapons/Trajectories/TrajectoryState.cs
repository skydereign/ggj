using ggj_engine.Source.Entities.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons.Trajectories
{
    public class TrajectoryState
    {
        public ProjectileEmitter Emitter;
        // update function for projectiles
        public delegate void ProjectileUpdate(Projectile p);

        // transition check for projectiles
        public delegate bool CheckTransition(Projectile p);

        // code for when transition is triggered
        public delegate void Transition(Projectile p);


        public ProjectileUpdate Update;
        public CheckTransition Check;
        public Transition TransitionState;
        public int LoopState; // when finishing, loop back to the given state

        public TrajectoryState(ProjectileEmitter emitter)
        {
            Emitter = emitter;
        }

        public void CheckNextState(Projectile p)
        {
            if(Check != null && Check(p))
            {
                if (TransitionState != null)
                {
                    TransitionState(p);
                }
                p.State = LoopState;
            }
        }
    }
}
