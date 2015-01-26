using ggj_engine.Source.Entities;
using ggj_engine.Source.Entities.Enemies;
using ggj_engine.Source.Entities.Player;
using ggj_engine.Source.Level;
using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.GameManagement
{
    public class GameManager
    {
        public class PlayerInfo
        {
            public int Score = 0;
            public int Kills = 0;
            public string Name = "null";
        }

        public const float GAME_LENGTH = 300000; //5 minutes

        public Screens.Screen MyScreen;
        public ScoreManager ScoreManager;
        public PlayerInfo MainPlayer;
        public List<PlayerInfo> AllPlayers;
        public float MillisecondsRemaining;
        public float MovementAndWeaponTimer;
        public float EnemySpawnTimer;
        private int enemyCount, maxEnemyCount;
        public int RuleChangeTextDisplay;

        public GameManager(Screens.Screen myScreen)
        {
            MyScreen = myScreen;
            ScoreManager = new GameManagement.ScoreManager();
            ScoreManager.MyScreen = MyScreen;
            MainPlayer = new PlayerInfo();
            MainPlayer.Name = "Me";
            AllPlayers = new List<PlayerInfo>();
            AllPlayers.Add(MainPlayer);
            MillisecondsRemaining = GAME_LENGTH;
            enemyCount = 0;
            maxEnemyCount = enemyCount;
        }

        public void Update(GameTime gameTime)
        {
            if (InputControl.GetKeyboardKeyPressed(Keys.O))
            {
                ScoreManager.ChangeGameGoals();
                RuleChangeTextDisplay = 45;
            }
            MillisecondsRemaining -= gameTime.ElapsedGameTime.Milliseconds;
            EnemySpawnTimer += gameTime.ElapsedGameTime.Milliseconds;
            MovementAndWeaponTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (MovementAndWeaponTimer > 30000f)
            {
                ScoreManager.ChangeGameGoals();
                RuleChangeTextDisplay = 45;
                ((Player)MyScreen.GetEntity("Player").ElementAt(0)).ChangeMovementAndWeapon();

                MovementAndWeaponTimer = 0;
            }

            if (EnemySpawnTimer > 5000f)
            {
                while (enemyCount < maxEnemyCount + 5)
                {
                    
                    int randX = (int)RandomUtil.Next(TileGrid.Width);
                    int randY = (int)RandomUtil.Next(TileGrid.Height);

                    if (TileGrid.Tiles[randX, randY].Walkable)
                    {
                        float chooseEnemy = (float)RandomUtil.Next(1);
                        if (chooseEnemy > 0.5f)
                        {
                            MyScreen.AddEntity(new YourMom(new Vector2(randX * Globals.TileSize, randY * Globals.TileSize)));
                        }
                        else
                        {
                            MyScreen.AddEntity(new Follower(new Vector2(randX * Globals.TileSize, randY * Globals.TileSize)));
                        }
                        enemyCount++;
                    }
                }
                maxEnemyCount = enemyCount;
                EnemySpawnTimer = 0.0f;
            }
            RuleChangeTextDisplay--;

        }

        public void AddToScore(Vector2 sourcePos, int amount)
        {
            if (amount != 0)
            {
                MyScreen.AddEntity(new ScoreVisual(sourcePos + RandomUtil.Next(new Vector2(-10,-10),new Vector2(10,10)), amount));
                MainPlayer.Score += amount;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float scale = 1;
            //Draw rule change
            if (RuleChangeTextDisplay > 0)
            {
                if (RuleChangeTextDisplay > 0)
                {
                    scale = 1f;
                }
                if (RuleChangeTextDisplay > 39)
                {
                    scale = 0.75f;
                }
                if (RuleChangeTextDisplay > 42)
                {
                    scale = 1.5f;
                }
                spriteBatch.DrawString(ContentLibrary.Fonts["pixelFont"], "RULE CHANGE", new Vector2(640, 150), Color.White, 0, new Vector2(ContentLibrary.Fonts["pixelFont"].MeasureString("RULE CHANGE").X / 2, 15), scale, SpriteEffects.None, 0);
            }

            //Draw Score
            spriteBatch.DrawString(ContentLibrary.Fonts["pixelFont"], "SCORE", new Vector2(10, 10), Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(ContentLibrary.Fonts["pixelFont"], MainPlayer.Score.ToString(), new Vector2(15, 40), Color.White, 0, Vector2.Zero, 0.75f, SpriteEffects.None, 0);

            //Draw time
            float minutesLeft = (float)Math.Floor(MillisecondsRemaining / 60000f);
            int secondsLeft = (int)(((MillisecondsRemaining - minutesLeft * 60000) / 60000f) * 60);
            string time = (minutesLeft).ToString() + ":" + secondsLeft.ToString("D2");
            spriteBatch.DrawString(ContentLibrary.Fonts["pixelFont"], "TIME", new Vector2(640, 10), Color.White, 0, new Vector2(ContentLibrary.Fonts["pixelFont"].MeasureString("TIME").X/2, 0), 0.2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(ContentLibrary.Fonts["pixelFont"], time, new Vector2(640, 30), Color.White, 0, new Vector2(ContentLibrary.Fonts["pixelFont"].MeasureString(time).X/2, 0), 0.5f, SpriteEffects.None, 0);

            //Draw kills
            spriteBatch.DrawString(ContentLibrary.Fonts["pixelFont"], "KILLS", new Vector2(1270, 10), Color.White, 0, new Vector2(ContentLibrary.Fonts["pixelFont"].MeasureString("KILLS").X,0), 0.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(ContentLibrary.Fonts["pixelFont"], MainPlayer.Kills.ToString(), new Vector2(1265, 40), Color.White, 0, new Vector2(ContentLibrary.Fonts["pixelFont"].MeasureString(MainPlayer.Kills.ToString()).X, 0), 0.75f, SpriteEffects.None, 0);
        }
    }
}
