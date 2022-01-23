using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Core
{
    static class Art
    {
        public static Texture2D Player { get; private set; }
        public static SpriteFont Font { get; private set; }
        public static Texture2D Pixel { get; private set; }		// a single white pixel
        public static Texture2D Pointer { get; private set; }
        public static Texture2D Bullet { get; private set; }

        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("Art/Player");

            Pointer = content.Load<Texture2D>("Art/Pointer");

            Pixel = new Texture2D(Player.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });

            Font = content.Load<SpriteFont>("Font");

            Bullet = content.Load<Texture2D>("Art/Bullet");
        }
    }
}
