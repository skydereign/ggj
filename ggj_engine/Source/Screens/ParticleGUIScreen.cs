using ggj_engine.Source.Entities.UIElements;
using ggj_engine.Source.Particles;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Screens
{
    class ParticleGUIScreen : Screen
    {
        public PSystem ParticleSystem;

        List<Emitter> emitters;
        int curEmitter = 0;
        enum EmitterType { Circle, Point }
        EmitterType curEmitterType = EmitterType.Point;

        public ParticleGUIScreen()
        {
            emitters = new List<Emitter>();
            curEmitter = 0;

            ParticleSystem = new PSystemSparks();
            AddEntity(ParticleSystem);
            
            ListGUI lGui = new ListGUI("", Vector2.Zero, ListGUI.Orientation.Vert);
            lGui.Add("pStartColor", new ColorGUI("pStartColor", Vector2.Zero));
            lGui.Add("pEndColor", new ColorGUI("pEndColor", Vector2.Zero));
            lGui.Add("pStartColorDeviation", new ColorGUI("pStartColorDeviation", Vector2.Zero));
            lGui.Add("pEndColorDeviation", new ColorGUI("pEndColorDeviation", Vector2.Zero));
            lGui.Add("pStartBrightnessDeviationFactor", new NumberButton("pStartBrightnessDeviationFactor", Vector2.Zero, 0, 0.05f, 0, 1));
            lGui.Add("pEndBrightnessDeviationFactor", new NumberButton("pEndBrightnessDeviationFactor", Vector2.Zero, 0, 0.05f, 0, 1));
            lGui.Add("pStartScale", new NumberButton("pStartScale", Vector2.Zero, 1f, 0.05f, 0.001f, 100f));
            lGui.Add("pEndScale", new NumberButton("pEndScale", Vector2.Zero, 1f, 0.05f, 0.001f, 100f));
            lGui.Add("pStartScaleDeviation", new NumberButton("pStartScaleDeviation", Vector2.Zero, 0f, 0.05f, 0f, 100f));
            lGui.Add("pEndScaleDeviation", new NumberButton("pEndScaleDeviation", Vector2.Zero, 0f, 0.05f, 0f, 100f));
            lGui.Add("pMinAngle", new Vector2GUI("pMinAngle", 0f, 0.1f, -1f, 1f));
            lGui.Add("pMaxAngle", new Vector2GUI("pMaxAngle", 0f, 0.1f, -1f, 1f));
            lGui.Add("pMinVel", new NumberButton("pMinVel", Vector2.Zero, 0f, 0.5f, 0f, 100f));
            lGui.Add("pMaxVel", new NumberButton("pMaxVel", Vector2.Zero, 0f, 0.5f, 0f, 100f));
            lGui.Add("pMinAccel", new Vector2GUI("pMinAccel", 0f, 0.1f, -100f, 100f));
            lGui.Add("pMaxAccel", new Vector2GUI("pMaxAccel", 0f, 0.1f, -100f, 100f));
            lGui.Add("pMinFriction", new NumberButton("pMinFriction", Vector2.Zero, 1f, 0.1f, -100f, 100f));
            lGui.Add("pMaxFriction", new NumberButton("pMaxFriction", Vector2.Zero, 1f, 0.1f, -100, 100f));
            lGui.Add("pMinLife", new NumberButton("pMinLife", Vector2.Zero, 20f, 1f, 1f, 10000f));
            lGui.Add("pMaxLife", new NumberButton("pMaxLife", Vector2.Zero, 20f, 1f, 1f, 10000f));
            lGui.Add("pUpdateFreq", new NumberButton("pUpdateFreq", Vector2.Zero, 1f, 1f, 1f, 120f));
            lGui.Add("pMinSpawn", new NumberButton("pMinSpawn", Vector2.Zero, 1f, 0.2f, 0f, 100f));
            lGui.Add("pMaxSpawn", new NumberButton("pMaxSpawn", Vector2.Zero, 2f, 0.2f, 0f, 100f));
            // NEED BOOL GUI
            AddEntity(lGui);
        }

        public override void Update(GameTime gameTime)
        {
            if(InputControl.GetKeyboardKeyPressed(Keys.A))
            {
                AddEmitter();
                Temp();
                ParticleSystem.AddEmitter(emitters[curEmitter]);
            }
            if (InputControl.GetKeyboardKeyPressed(Keys.S))
            {
                Temp();
            }
            base.Update(gameTime);
        }

        public void AddEmitter()
        {
            if (curEmitterType == EmitterType.Point)
            {
                emitters.Add(new PointEmitter());
                curEmitter = emitters.Count - 1;
            }
        }

        public void Temp()
        {
            Emitter e = emitters[curEmitter];
            e.pStartColor = new Color(127, 0, 1);
            e.pEndColor = e.pStartColor;
            e.pStartColorDeviation = new Color(0, 0, 0, 0);
            e.pEndColorDeviation = new Color(0, 0, 0, 0);
            e.pStartBrightnessDeviationFactor = 0.1f;
            e.pEndBrightnessDeviationFactor = 0.3f;
            e.pStartScale = 1;
            e.pEndScale = 5;
            e.pStartScaleDeviation = 0;
            e.pEndScaleDeviation = 0;
            e.pMinAccel = new Vector2(0, 0);
            e.pMaxAccel = new Vector2(0, 0);
            e.pMinAngle = new Vector2(-1, -1);
            e.pMaxAngle = new Vector2(1, 1);
            e.pMinVel = 10f;
            e.pMaxVel = 10f;
            e.pMinFriction = 0.7f;
            e.pMaxFriction = 0.7f;
            e.pMinLife = 75;
            e.pMaxLife = 80;
            e.pMinSpawn = 100;
            e.pMaxSpawn = 100;
            //emitters[curEmitter] = e;
        }
    }
}
