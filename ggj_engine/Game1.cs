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

        private static List<Screen> screens;
        private static List<Screen> createdScreens;
        private static List<Screen> deletedScreens;

        public static FMOD.System SoundSystem;
        public static SoundControl SoundController;

        float fpsDFrameCount;
        float fpsDTimePassed;
        float fpsDFrameRate;
        const float fpsSampleLength = 200;
        bool drawFps = true;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            graphics.PreferredBackBufferHeight = Globals.ScreenHeight;

            SoundSystem = new FMOD.System();
            FMOD.Factory.System_Create(ref SoundSystem);
            SoundSystem.init(150, FMOD.INITFLAGS.NORMAL, IntPtr.Zero);
            SoundController = new SoundControl(SoundSystem);
            IsMouseVisible = true;

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
            ContentLibrary.LoadSoundFX();
            ContentLibrary.LoadBGM();
            
            Debug.LoadContent();

            SoundController.LoadAllSounds(ContentLibrary.SoundFX.Keys, ContentLibrary.SoundFX.Values);
            SoundController.LoadAllSounds(ContentLibrary.BGM.Keys, ContentLibrary.BGM.Values);

            screens = new List<Screen>();
            createdScreens = new List<Screen>();
            deletedScreens = new List<Screen>();

            //screens.Add(new TestScreen());
            //screens.Add(new MultiplayerTestScreen());
            //screens.Add(new MenuScreen());
            screens.Add(new ParticleGUIScreen());
            //screens.Add(new TestScreen());
            //screens.Add(new TestParticleScreen());
            //screens.Add(new TestPlayerScreen());
            //screens.Add(new TestEnemyScreen());
            //screens.Add(new NetworkTestScreen());
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

            // only update top screen
            screens[screens.Count - 1].Update(gameTime);

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
            SoundSystem.update();
            SoundController.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // only draw top screen
            screens[screens.Count - 1].Draw(spriteBatch);

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
                spriteBatch.DrawString(ContentLibrary.Fonts["smallFont"], "Draw fps: " + MathExt.Truncate(fpsDFrameRate,2).ToString(), new Vector2(5, 680), Color.White);
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
        public static void PushScreen(Screen screen)
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
