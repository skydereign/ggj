using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Entities.UIElements
{
    class ListGUI : GUI
    {
        public enum Orientation { Horz, Vert };
        Orientation orientation;

        List<GUI> list;
        List<string> keys;

        public ListGUI(string label, Vector2 position, Orientation orientation) : base(label)
        {
            Position = position;

            this.orientation = orientation;
            list = new List<GUI>();
            keys = new List<string>();
        }

        public override void Init()
        {
            Position = MyScreen.Camera.ScreenToWorld(Position);
            foreach(GUI gui in list)
            {
                gui.MyScreen = MyScreen;
                gui.Init();
            }
            base.Init();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GUI gui in list)
            {
                gui.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 offset = Position;

            foreach(GUI gui in list)
            {
                gui.Position = offset;
                gui.ResetPositions();
                gui.Draw(spriteBatch);
                
                if(orientation == Orientation.Horz)
                {
                    offset.X = gui.Right() + 4f;
                }
                else
                {
                    offset.Y = gui.Bot() + 1f;
                }
            }
            base.Draw(spriteBatch);
        }

        public void Add(string key, GUI gui)
        {
            keys.Add(key);
            list.Add(gui);
        }

        public GUI Get(string key)
        {
            return list[keys.IndexOf(key)];
        }

        public override float Top()
        {
            return Position.Y;
        }

        public override float Bot()
        {
            throw new NotImplementedException();
        }

        public override float Right()
        {
            throw new NotImplementedException();
        }

        public override float Left()
        {
            return Position.X;
        }
    }
}
