﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ggj_engine.Source.Utility;

namespace ggj_engine.Source.Particles
{
    public class PSystemTest : PSystem
    {
        public PSystemTest(Vector2 p)
        {
            Position = p;

            Emitter e = new PointEmitter();
            
            e.MPSystem = this;
            e.pStartColor = new Color(240, 190, 40);
            e.pEndColor = new Color(120, 120, 120);
            e.pStartColorDeviation = new Color(30, 20, 0, 10);
            e.pEndColorDeviation = new Color(0, 0, 0, 0);
            e.pStartBrightnessDeviation = 0.1f;
            e.pEndBrightnessDeviation = 0.3f;
            e.pMinAccel = new Vector2(0, 0);
            e.pMaxAccel = new Vector2(0, 0);
            e.pMinAngle = new Vector2(-1, -1);
            e.pMaxAngle = new Vector2(1, 1);
            e.pMinVel = 3f;
            e.pMaxVel = 3f;
            e.pMinFriction = 1;
            e.pMaxFriction = 1;
            e.pMinLife = 50;
            e.pMaxLife = 50;
            e.pMinSpawn = 4;
            e.pMaxSpawn = 100;
            e.Burst = false;

            AddEmitter(e);
        }

    }
}