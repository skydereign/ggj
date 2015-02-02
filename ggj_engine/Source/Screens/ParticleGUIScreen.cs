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
        ListGUI curSettings;

        public ParticleGUIScreen()
        {
            emitters = new List<Emitter>();
            curEmitter = 0;

            ParticleSystem = new PSystemSparks();
            AddEntity(ParticleSystem);

            curSettings = new ListGUI("", Vector2.Zero, ListGUI.Orientation.Vert);
            curSettings.Add("pStartColor", new ColorGUI("pStartColor", Vector2.Zero));
            curSettings.Add("pEndColor", new ColorGUI("pEndColor", Vector2.Zero));
            curSettings.Add("pStartColorDeviation", new ColorGUI("pStartColorDeviation", Vector2.Zero));
            curSettings.Add("pEndColorDeviation", new ColorGUI("pEndColorDeviation", Vector2.Zero));
            curSettings.Add("pStartBrightnessDeviationFactor", new NumberButton("pStartBrightnessDeviationFactor", Vector2.Zero, 1, 0.05f, 0, 1));
            curSettings.Add("pEndBrightnessDeviationFactor", new NumberButton("pEndBrightnessDeviationFactor", Vector2.Zero, 1, 0.05f, 0, 1));
            curSettings.Add("pStartScale", new NumberButton("pStartScale", Vector2.Zero, 1f, 0.05f, 0.001f, 100f));
            curSettings.Add("pEndScale", new NumberButton("pEndScale", Vector2.Zero, 2f, 0.05f, 0.001f, 100f));
            curSettings.Add("pStartScaleDeviation", new NumberButton("pStartScaleDeviation", Vector2.Zero, 1f, 0.05f, 0f, 100f));
            curSettings.Add("pEndScaleDeviation", new NumberButton("pEndScaleDeviation", Vector2.Zero, 1f, 0.05f, 0f, 100f));
            curSettings.Add("pMinAngle", new Vector2GUI("pMinAngle", -1f, 0.1f, -1f, 1f));
            curSettings.Add("pMaxAngle", new Vector2GUI("pMaxAngle", 1f, 0.1f, -1f, 1f));
            curSettings.Add("pMinVel", new NumberButton("pMinVel", Vector2.Zero, 4f, 0.5f, 0f, 100f));
            curSettings.Add("pMaxVel", new NumberButton("pMaxVel", Vector2.Zero, 20f, 0.5f, 0f, 100f));
            curSettings.Add("pMinAccel", new Vector2GUI("pMinAccel", 0f, 0.1f, -100f, 100f));
            curSettings.Add("pMaxAccel", new Vector2GUI("pMaxAccel", 0f, 0.1f, -100f, 100f));
            curSettings.Add("pMinFriction", new NumberButton("pMinFriction", Vector2.Zero, 0.7f, 0.1f, -100f, 100f));
            curSettings.Add("pMaxFriction", new NumberButton("pMaxFriction", Vector2.Zero, 0.7f, 0.1f, -100, 100f));
            curSettings.Add("pMinLife", new NumberButton("pMinLife", Vector2.Zero, 50f, 1f, 1f, 10000f));
            curSettings.Add("pMaxLife", new NumberButton("pMaxLife", Vector2.Zero, 80f, 1f, 1f, 10000f));
            curSettings.Add("pUpdateFreq", new NumberButton("pUpdateFreq", Vector2.Zero, 1f, 1f, 1f, 120f));
            curSettings.Add("pMinSpawn", new NumberButton("pMinSpawn", Vector2.Zero, 5, 0.2f, 0f, 100f));
            curSettings.Add("pMaxSpawn", new NumberButton("pMaxSpawn", Vector2.Zero, 10, 0.2f, 0f, 100f));
            // NEED BOOL GUI
            AddEntity(curSettings);


            AddEmitter();
            UpdateCurrent();
            ParticleSystem.AddEmitter(emitters[curEmitter]);
        }

        public override void Update(GameTime gameTime)
        {
            UpdateCurrent();
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

        public void UpdateCurrent()
        {
            Emitter e = emitters[curEmitter];
            e.pStartColor = ((ColorGUI)curSettings.Get("pStartColor")).Value;
            e.pEndColor = ((ColorGUI)curSettings.Get("pEndColor")).Value;
            e.pStartColorDeviation = ((ColorGUI)curSettings.Get("pStartColorDeviation")).Value;
            e.pEndColorDeviation = ((ColorGUI)curSettings.Get("pEndColorDeviation")).Value;
            e.pStartBrightnessDeviationFactor = ((NumberButton)curSettings.Get("pStartBrightnessDeviationFactor")).Value;
            e.pEndBrightnessDeviationFactor = ((NumberButton)curSettings.Get("pStartBrightnessDeviationFactor")).Value;
            e.pStartScale = ((NumberButton)curSettings.Get("pStartBrightnessDeviationFactor")).Value;
            e.pEndScale = ((NumberButton)curSettings.Get("pStartBrightnessDeviationFactor")).Value;
            e.pStartScaleDeviation = ((NumberButton)curSettings.Get("pStartScaleDeviation")).Value;
            e.pEndScaleDeviation = ((NumberButton)curSettings.Get("pEndScaleDeviation")).Value;
            e.pMinAccel = ((Vector2GUI)curSettings.Get("pMinAccel")).Value;
            e.pMaxAccel = ((Vector2GUI)curSettings.Get("pMaxAccel")).Value;
            e.pMinAngle = ((Vector2GUI)curSettings.Get("pMinAngle")).Value;
            e.pMaxAngle = ((Vector2GUI)curSettings.Get("pMaxAngle")).Value;
            e.pMinVel = ((NumberButton)curSettings.Get("pMinVel")).Value;
            e.pMaxVel = ((NumberButton)curSettings.Get("pMaxVel")).Value;
            e.pMinFriction = ((NumberButton)curSettings.Get("pMinFriction")).Value;
            e.pMaxFriction = ((NumberButton)curSettings.Get("pMaxFriction")).Value;
            e.pMinLife = ((NumberButton)curSettings.Get("pMinLife")).Value;
            e.pMaxLife = ((NumberButton)curSettings.Get("pMaxLife")).Value;
            e.pMinSpawn = (int)((NumberButton)curSettings.Get("pMinSpawn")).Value;
            e.pMaxSpawn = (int)((NumberButton)curSettings.Get("pMaxSpawn")).Value;
            //emitters[curEmitter] = e;
        }
    }
}
