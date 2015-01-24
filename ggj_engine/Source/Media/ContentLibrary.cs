﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Media
{
    class ContentLibrary
    {
        public static Dictionary<string, Sprite> Sprites;
        static GraphicsDevice GraphicsDevice;

        public static void Init(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
        }

        /// <summary>
        /// Loads all sprites into the content library
        /// </summary>
        public static void LoadSprites (ContentManager content)
        {
            Sprites = new Dictionary<string, Sprite>();

            // add sprites
            Sprites.Add("test_sprite", new Sprite(content.Load<Texture2D>("Textures/test_sprite.png")));

            Sprite animated_example = new Sprite(content.Load<Texture2D>("Textures/test_animation.png"));
            animated_example.AddAnimation("default", 0, 0, 20, 20, 4, 0.1f);
            Sprites.Add("test_animation", animated_example);
        }
    }
}