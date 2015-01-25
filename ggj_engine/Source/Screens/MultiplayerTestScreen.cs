using ggj_engine.Source.Entities.Player;
using ggj_engine.Source.Level;
using ggj_engine.Source.Media;
using ggj_engine.Source.Network;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Screens
{
    public class MultiplayerTestScreen : Screen
    {
        public MultiplayerTestScreen()
        {
            NetworkManager.Instance.MyScreen = this;

            Camera = new Camera(Vector2.Zero, new Vector2(1280, 720));

            entities = new List<Entities.Entity>();

            TileGrid.Init(50, 50, new Vector2(0, 0));

            

            //string newGameData = "";

            //newGameData += NetworkManager.IPSOFBlock;

            //for(int i = 0; i < entities.Count; ++i)
            //{
            //    Vector2 pos = entities[i].Position;
            //    newGameData += ",C,E," + pos.X + "," + pos.Y + ",";
            //}

            //newGameData += NetworkManager.IPEOFBlock;

            //NetworkManager.Instance.SetNewGameData(newGameData);

            NetworkManager.Instance.DetermineHost();

            if (NetworkManager.Instance.IsHost)
            {
                Player hostPlayer = new Player(new Vector2(100, 100));
                hostPlayer.NetPlayer = false;

                entities.Add(hostPlayer);
                entities[entities.Count - 1].MyScreen = this;

                

                for (int i = 0; i < 50; ++i)
                {
                    Vector2 pos = new Vector2((float)RandomUtil.Next() * 200, (float)RandomUtil.Next() * 200);
                    Entities.TestEntity e = new Entities.TestEntity(pos);

                    e.MyScreen = this;

                    entities.Add(e);
                }

                string newGameData = "";

                newGameData += NetworkManager.IPSOFBlock;

                newGameData += ",CP," + hostPlayer.PlayerID + "," + hostPlayer.Position.X + "," + hostPlayer.Position.Y + ",";

                for (int i = 1; i < entities.Count; ++i)
                {
                    Vector2 pos = entities[i].Position;
                    newGameData += ",CE," + pos.X + "," + pos.Y + ",";
                }

                newGameData += NetworkManager.IPEOFBlock;

                NetworkManager.Instance.SetNewGameData(newGameData);
            }
            else
            {
                ////create net player
                //Player clientPlayer = new Player(new Vector2(200, 200));
                //clientPlayer.NetPlayer = true;

                ////get entity data from host


                ////add net player to game through host
                //entities.Add(clientPlayer);
            }
        }

        public override void Update(GameTime gameTime)
        {
            //Camera Zoom
            if (InputControl.GetMouseWheelUp())
            {
                Camera.Zoom -= 0.05f;
            }
            if (InputControl.GetMouseWheelDown())
            {
                Camera.Zoom += 0.05f;
            }

            base.Update(gameTime);

            NetworkManager.Instance.Solve(gameTime, entities);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            SpriteBatchCameraBegin(spriteBatch);
            TileGrid.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(spriteBatch);

            //Draw cursor after everything else has been drawn
            spriteBatch.Begin();
            if (InputControl.GetMouseOnLeftHeld())
            {
                spriteBatch.Draw(ContentLibrary.Sprites["cursor"].Texture, InputControl.GetMousePosition(), Color.Blue);
            }
            else
            {
                spriteBatch.Draw(ContentLibrary.Sprites["cursor"].Texture, InputControl.GetMousePosition(), Color.White);
            }
            spriteBatch.DrawString(ContentLibrary.Fonts["smallFont"], "Camera pos: [" + Camera.Position.X.ToString() + " | " + Camera.Position.Y.ToString() + "]", new Vector2(5, 30), Color.White);
            spriteBatch.End();
        }
    }
}
