using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Media
{
    public class ContentLibrary
    {
        private static SpriteDictionary sprites;
        public static SpriteDictionary Sprites { get { return sprites; } }
        public static Dictionary<string, SpriteFont> Fonts;
        public static Dictionary<string, string> SoundFX;
        public static Dictionary<string, string> BGM;
        static GraphicsDevice GraphicsDevice;


        public static Texture2D Tilesheet;
        public static int NumHorzTiles = 10;

        public static void Init(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
        }

        /// <summary>
        /// Loads all sprites into the content library
        /// </summary>
        public static void LoadSprites (ContentManager content)
        {
            sprites = new SpriteDictionary();

            // add sprites
            Sprites.Add("player", new Sprite(content.Load<Texture2D>("Textures/player.png")));
            Sprites.Add("test_sprite", new Sprite(content.Load<Texture2D>("Textures/test_sprite.png")));
            Sprites.Add("cursor", new Sprite(content.Load<Texture2D>("Textures/cursor.png")));
            Sprites.Add("circle_region", new Sprite(content.Load<Texture2D>("Textures/circle_region.png")));
            Sprites.Add("white_pixel", new Sprite(content.Load<Texture2D>("Textures/white_pixel.png")));

            Sprite animated_example = new Sprite(content.Load<Texture2D>("Textures/test_animation.png"));
            animated_example.AddAnimation("default", 0, 0, 20, 20, 4, 0.1f);
            Sprites.Add("test_animation", animated_example);

            Sprite triangle = new Sprite(content.Load<Texture2D>("Textures/triangle_enemy.png"));
            triangle.AddAnimation("default", 0, 0, 16, 16, 8, 0.1f);
            Sprites.Add("triangle_enemy", triangle);

            Sprite square = new Sprite(content.Load<Texture2D>("Textures/square_enemy.png"));
            square.AddAnimation("default", 0, 0, 16, 16, 3, 0.1f);
            Sprites.Add("square_enemy", square);


            Tilesheet = content.Load<Texture2D>("Textures/tiles.png");
        }

        /// <summary>
        /// Loads all fonts
        /// </summary>
        /// <param name="content"></param>
        public static void LoadFonts(ContentManager content)
        {
            Fonts = new Dictionary<string, SpriteFont>();

            //add fonts
            Fonts.Add("smallFont", content.Load<SpriteFont>("Fonts/smallFont"));
            Fonts.Add("pixelFont", content.Load<SpriteFont>("Fonts/PixelFont"));

        }

        public static void LoadSoundFX()
        {
            SoundFX = new Dictionary<string, string>();

            //add sounds
            SoundFX.Add("laser_shoot", "Content/Audio/SoundEffects/Laser_Shoot10.wav");
            SoundFX.Add("fire", "Content/Audio/SoundEffects/fire-punch.wav");
            SoundFX.Add("bullet", "Content/Audio/SoundEffects/bullet.wav");
            SoundFX.Add("bow", "Content/Audio/SoundEffects/bow.wav");
            SoundFX.Add("cannon", "Content/Audio/SoundEffects/cannon.wav");
            SoundFX.Add("rocket", "Content/Audio/SoundEffects/rocket.wav");
        }

        public static void LoadBGM()
        {
            BGM = new Dictionary<string, string>();
            
            //add music
            BGM.Add("title_screen", "Content/Audio/BGM/title_screen.wav");
        }


        public class SpriteDictionary : Dictionary<String, Sprite>
        {
            new public Sprite this[string index]
            {
                get
                {
                    //we pass back copies instead of original
                    //otherwise we would have multiple Entities trying
                    //to control the same sprite
                    return base[index].Clone();
                }
            }
        }
    }
}
