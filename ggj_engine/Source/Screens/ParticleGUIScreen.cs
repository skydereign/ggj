using ggj_engine.Source.Entities.UIElements;
using ggj_engine.Source.Particles;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        bool showLocations = false;

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
            if (InputControl.GetKeyboardKeyPressed(Keys.Right))
            {
                if (curEmitter < emitters.Count - 1)
                {
                    curEmitter++;
                    updateGui();
                }
            }
            else if (InputControl.GetKeyboardKeyPressed(Keys.Left))
            {
                if (curEmitter > 0)
                {
                    curEmitter--;
                    updateGui();
                }
            }

            UpdateCurrent();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //Draw locations of each emitter and system center
            if (showLocations)
            { 
            SpriteBatchCameraBegin(spriteBatch);
            Debug.DrawCircle(spriteBatch, ParticleSystem.Position, Color.Red, 4);
            for (int i = 0; i < ParticleSystem.Emitters.Count;i++)
            {
                Emitter e = ParticleSystem.Emitters[i];
                Color c = i == curEmitter ? Color.Orange : Color.Lime;
                Debug.DrawCircle(spriteBatch, e.PositionOffset + ParticleSystem.Position, c, 3);
            }
            spriteBatch.End();
                }
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
            curSettings.Add("pStartColor", new ColorGUI("pStartColor", Vector2.Zero, new Vector4(127,127,127,255)));
            curSettings.Add("pEndColor", new ColorGUI("pEndColor", Vector2.Zero, new Vector4(127, 127, 127, 255)));
            curSettings.Add("pStartColorDeviation", new ColorGUI("pStartColorDeviation", Vector2.Zero, new Vector4(127, 127, 127, 0)));
            curSettings.Add("pEndColorDeviation", new ColorGUI("pEndColorDeviation", Vector2.Zero, new Vector4(127, 127, 127, 0)));
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
            menuGui.Add("curEmitter", new GenericButtonGUI("[Display Locations]", Vector2.Zero, () => { showLocations = !showLocations; }));
            menuGui.Add("save", new GenericButtonGUI("[Save]", Vector2.Zero, () => { SaveParticles(); }));
            menuGui.Add("load", new GenericButtonGUI("[Load]", Vector2.Zero, () => { LoadParticles(); }));
            menuGui.Add("addEmitter", new GenericButtonGUI("[Add Emitter]", Vector2.Zero, () => { AddEmitter(); }));
            menuGui.Add("removeEmitter", new GenericButtonGUI("[Remove]", Vector2.Zero, () =>
            {
                if (emitters.Count > 1)
                {
                    Emitter e = emitters[curEmitter];
                    emitters.Remove(e);
                    ParticleSystem.RemoveEmitter(e);
                    if(curEmitter == emitters.Count)
                    {
                        curEmitter = emitters.Count - 1;
                    }
                    updateGui();
                }
            }));
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
            e.pUpdateFreq = (int)((NumberButton)curSettings.Get("pUpdateFreq")).Value;
            e.pMinSpawn = (int)((NumberButton)curSettings.Get("pMinSpawn")).Value;
            e.pMaxSpawn = (int)((NumberButton)curSettings.Get("pMaxSpawn")).Value;
            e.Burst = ((BoolGUI)curSettings.Get("Burst")).Value;
        }

        public void SaveParticles()
        {
            StreamWriter writer = new StreamWriter(((StringGUI)curSettings.Get("filename")).Value);
            writer.WriteLine(emitters.Count);
            foreach(Emitter e in emitters)
            {
                writer.WriteLine(e.PositionOffset.X + "," + e.PositionOffset.Y);
                writer.WriteLine(ColorToString(e.pStartColor));
                writer.WriteLine(ColorToString(e.pEndColor));
                writer.WriteLine(ColorToString(e.pStartColorDeviation));
                writer.WriteLine(ColorToString(e.pEndColorDeviation));
                writer.WriteLine(e.pStartBrightnessDeviationFactor);
                writer.WriteLine(e.pEndBrightnessDeviationFactor);
                writer.WriteLine(e.pStartScale);
                writer.WriteLine(e.pEndScale);
                writer.WriteLine(e.pStartScaleDeviation);
                writer.WriteLine(e.pEndScaleDeviation);
                writer.WriteLine(Vector2ToString(e.pMinAccel));
                writer.WriteLine(Vector2ToString(e.pMaxAccel));
                writer.WriteLine(Vector2ToString(e.pMinAngle));
                writer.WriteLine(Vector2ToString(e.pMaxAngle));
                writer.WriteLine(e.pMinVel);
                writer.WriteLine(e.pMaxVel);
                writer.WriteLine(e.pMinFriction);
                writer.WriteLine(e.pMaxFriction);
                writer.WriteLine(e.pMinLife);
                writer.WriteLine(e.pMaxLife);
                writer.WriteLine(e.pMinSpawn);
                writer.WriteLine(e.pMaxSpawn);
                writer.WriteLine(e.Burst);
            }
            writer.Close();
        }
        public void LoadParticles()
        {
            string filename = ((StringGUI)curSettings.Get("filename")).Value;
            try
            {
                StreamReader reader = new StreamReader(filename);
                int count = Int32.Parse(reader.ReadLine());

                DeleteEntity(ParticleSystem);
                ParticleSystem = new PSystemTest(Vector2.Zero);
                AddEntity(ParticleSystem);
                emitters.Clear();

                for (int i = 0; i < count; i++)
                {
                    curEmitter = i;
                    Emitter e = new PointEmitter();
                    string[] position = reader.ReadLine().Split(',');
                    e.PositionOffset = new Vector2(float.Parse(position[0]), float.Parse(position[1]));
                    e.pStartColor = StringToColor(reader.ReadLine());
                    e.pEndColor = StringToColor(reader.ReadLine());
                    e.pStartColorDeviation = StringToColor(reader.ReadLine());
                    e.pEndColorDeviation = StringToColor(reader.ReadLine());
                    e.pStartBrightnessDeviationFactor = float.Parse(reader.ReadLine());
                    e.pEndBrightnessDeviationFactor = float.Parse(reader.ReadLine());
                    e.pStartScale = float.Parse(reader.ReadLine());
                    e.pEndScale = float.Parse(reader.ReadLine());
                    e.pStartScaleDeviation = float.Parse(reader.ReadLine());
                    e.pEndScaleDeviation = float.Parse(reader.ReadLine());
                    e.pMinAccel = StringToVector2(reader.ReadLine());
                    e.pMaxAccel = StringToVector2(reader.ReadLine());
                    e.pMinAngle = StringToVector2(reader.ReadLine());
                    e.pMaxAngle = StringToVector2(reader.ReadLine());
                    e.pMinVel = float.Parse(reader.ReadLine());
                    e.pMaxVel = float.Parse(reader.ReadLine());
                    e.pMinFriction = float.Parse(reader.ReadLine());
                    e.pMaxFriction = float.Parse(reader.ReadLine());
                    e.pMinLife = float.Parse(reader.ReadLine());
                    e.pMaxLife = float.Parse(reader.ReadLine());
                    e.pMinSpawn = Int32.Parse(reader.ReadLine());
                    e.pMaxSpawn = Int32.Parse(reader.ReadLine());
                    e.Burst = bool.Parse(reader.ReadLine());
                    emitters.Add(e);
                    ParticleSystem.AddEmitter(e);
                }
                reader.Close();

                // update gui
                curEmitter = 0;
                updateGui();
            }
            catch(Exception e)
            {
                Console.WriteLine("File " + filename + " does not exist");
            }
        }

        private void updateGui()
        {
            Emitter cur = emitters[curEmitter];

            ((ColorGUI)curSettings.Get("pStartColor")).Set(cur.pStartColor);
            ((ColorGUI)curSettings.Get("pEndColor")).Set(cur.pEndColor);
            ((ColorGUI)curSettings.Get("pStartColorDeviation")).Set(cur.pStartColorDeviation);
            ((ColorGUI)curSettings.Get("pEndColorDeviation")).Set(cur.pEndColorDeviation);
            ((NumberButton)curSettings.Get("pStartBrightnessDeviationFactor")).Value = cur.pStartBrightnessDeviationFactor;
            ((NumberButton)curSettings.Get("pEndBrightnessDeviationFactor")).Value = cur.pEndBrightnessDeviationFactor;
            ((NumberButton)curSettings.Get("pStartScale")).Value = cur.pStartScale;
            ((NumberButton)curSettings.Get("pEndScale")).Value = cur.pEndScale;
            ((NumberButton)curSettings.Get("pStartScaleDeviation")).Value = cur.pStartScaleDeviation;
            ((NumberButton)curSettings.Get("pEndScaleDeviation")).Value = cur.pEndScaleDeviation;
            ((Vector2GUI)curSettings.Get("pMinAccel")).Set(cur.pMinAccel);
            ((Vector2GUI)curSettings.Get("pMaxAccel")).Set(cur.pMaxAccel);
            ((Vector2GUI)curSettings.Get("pMinAngle")).Set(cur.pMinAngle);
            ((Vector2GUI)curSettings.Get("pMaxAngle")).Set(cur.pMaxAngle);
            ((NumberButton)curSettings.Get("pMinVel")).Value = cur.pMinVel;
            ((NumberButton)curSettings.Get("pMaxVel")).Value = cur.pMaxVel;
            ((NumberButton)curSettings.Get("pMinFriction")).Value = cur.pMinFriction;
            ((NumberButton)curSettings.Get("pMaxFriction")).Value = cur.pMaxFriction;
            ((NumberButton)curSettings.Get("pMinLife")).Value = cur.pMinLife;
            ((NumberButton)curSettings.Get("pMaxLife")).Value = cur.pMaxLife;
            ((NumberButton)curSettings.Get("pMinSpawn")).Value = cur.pMinSpawn;
            ((NumberButton)curSettings.Get("pMaxSpawn")).Value = cur.pMaxSpawn;
            ((BoolGUI)curSettings.Get("Burst")).Value = cur.Burst;
        }
        public string ColorToString(Color color)
        {
            return color.R + "," + color.G + "," + color.B + "," + color.A;
        }

        public Color StringToColor(string color)
        {
            string[] colors = color.Split(',');
            return new Color((int)float.Parse(colors[0]), (int)float.Parse(colors[1]), (int)float.Parse(colors[2]), (int)float.Parse(colors[3]));
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
