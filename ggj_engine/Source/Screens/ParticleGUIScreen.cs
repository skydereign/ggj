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
        List<Emitter> emitters;
        int curEmitter = 0;
        enum EmitterType { Circle, Point }
        EmitterType curEmitterType = EmitterType.Point;

        public PSystem ParticleSystem;
        public NumberButton ColorR;

        public ParticleGUIScreen()
        {
            emitters = new List<Emitter>();
            curEmitter = 0;

            ParticleSystem = new PSystemSparks();
            AddEntity(ParticleSystem);
            AddEntity(new GenericButton("testing", new Vector2(100, 100), () => { Console.WriteLine("testing"); }));
            ColorR = new NumberButton(new Vector2(200, 100), 127, 1f, 0, 255);
            AddEntity(ColorR);
            AddEntity(new ColorGUI("Testing", new Vector2(10, 10)));
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
            e.pStartColor = new Color(ColorR.Value, ColorR.Value, ColorR.Value);
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
            e.pMinVel = ColorR.Value;
            e.pMaxVel = ColorR.Value;
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
