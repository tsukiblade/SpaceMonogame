﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Core
{
    internal static class Art
    {
        public static Texture2D Player { get; private set; }
        public static Texture2D Obstacle { get; private set; }
        public static SpriteFont Font { get; private set; }
        public static Texture2D Pixel { get; private set; } // a single white pixel
        public static Texture2D Pointer { get; private set; }
        public static Texture2D Bullet { get; private set; }
        public static Texture2D Alien { get; private set; }
        public static Texture2D Ship { get; private set; }
        public static Texture2D Button { get; private set; }
        public static Texture2D Bomb { get; private set; }
        public static Texture2D Laser { get; private set; }

        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("Art/Player");
            Obstacle = content.Load<Texture2D>("Art/Obstacle");

            Pointer = content.Load<Texture2D>("Art/Pointer");

            Pixel = new Texture2D(Player.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });

            Font = content.Load<SpriteFont>("Font");

            Bullet = content.Load<Texture2D>("Art/Bullet");
            Alien = content.Load<Texture2D>("Art/Alien");
            Ship = content.Load<Texture2D>("Art/Ship");

            Button = content.Load<Texture2D>("Art/Bomb");
            Bomb = content.Load<Texture2D>("Art/Bomb");
            Laser = content.Load<Texture2D>("Art/Laser");
        }
    }
}