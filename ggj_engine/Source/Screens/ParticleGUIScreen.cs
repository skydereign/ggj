using ggj_engine.Source.Entities.UIElements;
using ggj_engine.Source.Particles;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
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

            ParticleSystem = new PSystemTest(Vector2.Zero);
            AddEntity(ParticleSystem);

            InitGui();

            AddEmitter();
            UpdateCurrent();
        }

        public override void Update(GameTime gameTime)
        {
            if(InputControl.GetKeyboardKeyHeld(Keys.LeftControl) && InputControl.GetKeyboardKeyPressed(Keys.S))
            {
                SaveParticles();
            }
            if (InputControl.GetKeyboardKeyHeld(Keys.LeftControl) && InputControl.GetKeyboardKeyPressed(Keys.L))
            {
                LoadParticles();
            }

            if(InputControl.GetMouseOnMiddleHeld())
            {
                emitters[curEmitter].PositionOffset = Camera.ScreenToWorld(InputControl.GetMousePosition());
            }

            // add controls for changing between curEmitter
            if(InputControl.GetKeyboardKeyPressed(Keys.Right))
            {
                if (curEmitter < emitters.Count - 1) { curEmitter++; }
            }
            else if(InputControl.GetKeyboardKeyPressed(Keys.Left))
            {
                if (curEmitter > 0) { curEmitter--; }
            }

            UpdateCurrent();
            base.Update(gameTime);
        }

        public void AddEmitter()
        {
            if (curEmitterType == EmitterType.Point)
            {
                Emitter e = new PointEmitter();
                emitters.Add(e);
                curEmitter = emitters.Count - 1;
                ParticleSystem.AddEmitter(e);
            }
        }

        private void InitGui()
        {
            curSettings = new ListGUI("", Vector2.Zero, ListGUI.Orientation.Vert, GUI.Anchor.None);
            curSettings.Add("filename", new StringGUI("filename", "testparticle", Keys.Enter));
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
            curSettings.Add("Burst", new BoolGUI("Burst", false));
            curSettings.Add("emitterPosition", new DisplayGUI(() => { return "position =  " + emitters[curEmitter].PositionOffset; }));
            curSettings.Add("curEmitter", new DisplayGUI(() => { return "Emitter " + (curEmitter + 1) + "/" + emitters.Count; }));
            AddEntity(curSettings);

            ListGUI menuGui = new ListGUI("", new Vector2(400, 0), ListGUI.Orientation.Horz, GUI.Anchor.Right);
            menuGui.Add("burst", new GenericButtonGUI("[Burst]", Vector2.Zero, () => { emitters[curEmitter].BurstParticles(); }));
            menuGui.Add("save", new GenericButtonGUI("[Save]", Vector2.Zero, () => { SaveParticles(); }));
            menuGui.Add("load", new GenericButtonGUI("[Load]", Vector2.Zero, () => { LoadParticles(); }));
            menuGui.Add("addEmitter", new GenericButtonGUI("[Add Emitter]", Vector2.Zero, () => { AddEmitter(); }));
            AddEntity(menuGui);
        }

        public void UpdateCurrent()
        {
            Emitter e = emitters[curEmitter];
            e.pStartColor = ((ColorGUI)curSettings.Get("pStartColor")).Value;
            e.pEndColor = ((ColorGUI)curSettings.Get("pEndColor")).Value;
            e.pStartColorDeviation = ((ColorGUI)curSettings.Get("pStartColorDeviation")).Value;
            e.pEndColorDeviation = ((ColorGUI)curSettings.Get("pEndColorDeviation")).Value;
            e.pStartBrightnessDeviationFactor = ((NumberButton)curSettings.Get("pStartBrightnessDeviationFactor")).Value;
            e.pEndBrightnessDeviationFactor = ((NumberButton)curSettings.Get("pEndBrightnessDeviationFactor")).Value;
            e.pStartScale = ((NumberButton)curSettings.Get("pStartScale")).Value;
            e.pEndScale = ((NumberButton)curSettings.Get("pEndScale")).Value;
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
            e.Burst = ((BoolGUI)curSettings.Get("Burst")).Value;
        }

        public void SaveParticles()
        {
            StreamWriter writer = new StreamWriter(((StringGUI)curSettings.Get("filename")).Value);
            writer.WriteLine(ColorToString(((ColorGUI)curSettings.Get("pStartColor")).Value));
            writer.WriteLine(ColorToString(((ColorGUI)curSettings.Get("pEndColor")).Value));
            writer.WriteLine(ColorToString(((ColorGUI)curSettings.Get("pStartColorDeviation")).Value));
            writer.WriteLine(ColorToString(((ColorGUI)curSettings.Get("pEndColorDeviation")).Value));
            writer.WriteLine(((NumberButton)curSettings.Get("pStartBrightnessDeviationFactor")).Value);
            writer.WriteLine(((NumberButton)curSettings.Get("pEndBrightnessDeviationFactor")).Value);
            writer.WriteLine(((NumberButton)curSettings.Get("pStartScale")).Value);
            writer.WriteLine(((NumberButton)curSettings.Get("pEndScale")).Value);
            writer.WriteLine(((NumberButton)curSettings.Get("pStartScaleDeviation")).Value);
            writer.WriteLine(((NumberButton)curSettings.Get("pEndScaleDeviation")).Value);
            writer.WriteLine(Vector2ToString(((Vector2GUI)curSettings.Get("pMinAccel")).Value));
            writer.WriteLine(Vector2ToString(((Vector2GUI)curSettings.Get("pMaxAccel")).Value));
            writer.WriteLine(Vector2ToString(((Vector2GUI)curSettings.Get("pMinAngle")).Value));
            writer.WriteLine(Vector2ToString(((Vector2GUI)curSettings.Get("pMaxAngle")).Value));
            writer.WriteLine(((NumberButton)curSettings.Get("pMinVel")).Value);
            writer.WriteLine(((NumberButton)curSettings.Get("pMaxVel")).Value);
            writer.WriteLine(((NumberButton)curSettings.Get("pMinFriction")).Value);
            writer.WriteLine(((NumberButton)curSettings.Get("pMaxFriction")).Value);
            writer.WriteLine(((NumberButton)curSettings.Get("pMinLife")).Value);
            writer.WriteLine(((NumberButton)curSettings.Get("pMaxLife")).Value);
            writer.WriteLine((int)((NumberButton)curSettings.Get("pMinSpawn")).Value);
            writer.WriteLine((int)((NumberButton)curSettings.Get("pMaxSpawn")).Value);
            writer.WriteLine(((BoolGUI)curSettings.Get("Burst")).Value);
            writer.Close();
        }
        public void LoadParticles()
        {
            string filename = ((StringGUI)curSettings.Get("filename")).Value;
            try
            {
                StreamReader reader = new StreamReader(filename);
                ((ColorGUI)curSettings.Get("pStartColor")).Set(StringToColor(reader.ReadLine()));
                ((ColorGUI)curSettings.Get("pEndColor")).Set(StringToColor(reader.ReadLine()));
                ((ColorGUI)curSettings.Get("pStartColorDeviation")).Set(StringToColor(reader.ReadLine()));
                ((ColorGUI)curSettings.Get("pEndColorDeviation")).Set(StringToColor(reader.ReadLine()));
                ((NumberButton)curSettings.Get("pStartBrightnessDeviationFactor")).Value = float.Parse(reader.ReadLine());
                ((NumberButton)curSettings.Get("pEndBrightnessDeviationFactor")).Value = float.Parse(reader.ReadLine());
                ((NumberButton)curSettings.Get("pStartScale")).Value = float.Parse(reader.ReadLine());
                ((NumberButton)curSettings.Get("pEndScale")).Value = float.Parse(reader.ReadLine());
                ((NumberButton)curSettings.Get("pStartScaleDeviation")).Value = float.Parse(reader.ReadLine());
                ((NumberButton)curSettings.Get("pEndScaleDeviation")).Value = float.Parse(reader.ReadLine());
                ((Vector2GUI)curSettings.Get("pMinAccel")).Set(StringToVector2(reader.ReadLine()));
                ((Vector2GUI)curSettings.Get("pMaxAccel")).Set(StringToVector2(reader.ReadLine()));
                ((Vector2GUI)curSettings.Get("pMinAngle")).Set(StringToVector2(reader.ReadLine()));
                ((Vector2GUI)curSettings.Get("pMaxAngle")).Set(StringToVector2(reader.ReadLine()));
                ((NumberButton)curSettings.Get("pMinVel")).Value = float.Parse(reader.ReadLine());
                ((NumberButton)curSettings.Get("pMaxVel")).Value = float.Parse(reader.ReadLine());
                ((NumberButton)curSettings.Get("pMinFriction")).Value = float.Parse(reader.ReadLine());
                ((NumberButton)curSettings.Get("pMaxFriction")).Value = float.Parse(reader.ReadLine());
                ((NumberButton)curSettings.Get("pMinLife")).Value = float.Parse(reader.ReadLine());
                ((NumberButton)curSettings.Get("pMaxLife")).Value = float.Parse(reader.ReadLine());
                ((NumberButton)curSettings.Get("pMinSpawn")).Value = float.Parse(reader.ReadLine());
                ((NumberButton)curSettings.Get("pMaxSpawn")).Value = float.Parse(reader.ReadLine());
                ((BoolGUI)curSettings.Get("Burst")).Value = bool.Parse(reader.ReadLine());
                reader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("File " + filename + " does not exist");
            }
        }

        public string ColorToString(Color color)
        {
            return color.R + "," + color.G + "," + color.B;
        }

        public Color StringToColor(string color)
        {
            string[] colors = color.Split(',');
            return new Color((int)float.Parse(colors[0]), (int)float.Parse(colors[1]), (int)float.Parse(colors[2]));
        }
        public string Vector2ToString(Vector2 vector)
        {
            return vector.X + "," + vector.Y;
        }

        public Vector2 StringToVector2(string value)
        {
            string[] values = value.Split(',');
            Vector2 vector = new Vector2(float.Parse(values[0]), float.Parse(values[1]));
            return vector;
        }
    }
}
