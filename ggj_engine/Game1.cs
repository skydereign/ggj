#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ggj_engine.Source.Screens;
using ggj_engine.Source.Media;
using ggj_engine.Source.Utility;
#endregion

namespace ggj_engine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Screen> screens;
        private List<Screen> createdScreens;
        private List<Screen> deletedScreens;
        float fpsDFrameCount;
        float fpsDTimePassed;
        float fpsDFrameRate;
        const float fpsSampleLength = 200;
        bool drawFps = true;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ContentLibrary.Init(GraphicsDevice);
            Screen.Game = this;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentLibrary.LoadSprites(Content);
            ContentLibrary.LoadFonts(Content);

            screens = new List<Screen>();
            createdScreens = new List<Screen>();
            deletedScreens = new List<Screen>();

            //screens.Add(new TestScreen());
            screens.Add(new NetworkTestScreen());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // need to add InputControl

            // may need to update this to call update on all screens
            if (screens.Count > 0)
            {
                screens[screens.Count - 1].Update(gameTime);
            }

            foreach(Screen screen in createdScreens)
            {
                screens.Add(screen);
            }
            createdScreens.Clear();


            foreach (Screen screen in deletedScreens)
            {
                screens.Remove(screen);
                screen.Close();
            }
            deletedScreens.Clear();

            InputControl.Update(1);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            foreach(Screen screen in screens)
            {
                screen.Draw(spriteBatch);
            }

            //Calculate draw framerate
            fpsDFrameCount++;
            fpsDTimePassed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (fpsDTimePassed > fpsSampleLength)
            {
                fpsDFrameRate = fpsDFrameCount / fpsDTimePassed * 1000;
                fpsDFrameCount = 0;
                fpsDTimePassed = 0;
            }
            if (drawFps)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(ContentLibrary.Fonts["smallFont"], "Draw fps: " + MathExt.Truncate(fpsDFrameRate,2).ToString(), new Vector2(5, 10), Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// Clear all other screens and set current to newScreen
        /// </summary>
        /// <param name="newScreen"></param>
        public void SetScreen(Screen newScreen)
        {
            foreach(Screen screen in screens)
            {
                screen.Close();
            }

            screens.Clear();
            screens.Add(newScreen);
        }

        /// <summary>
        /// Add a new screen to the top
        /// </summary>
        /// <param name="screen"></param>
        public void PushScreen(Screen screen)
        {
            createdScreens.Add(screen);
        }

        public void RemoveScreen(Screen screen)
        {
            deletedScreens.Add(screen);
        }

        public void PopScreen()
        {
            if (screens.Count > 0)
            {
                deletedScreens.Add(screens[screens.Count - 1]);
            }
        }
    }
}
