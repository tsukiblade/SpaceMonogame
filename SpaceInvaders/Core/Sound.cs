using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace SpaceInvaders.Core
{
    internal class Sound
    {
        private static readonly Random rand = new Random();

        private static SoundEffect[] explosions;

        private static SoundEffect[] shots;

        private static SoundEffect[] spawns;

        public static Song Music { get; private set; }

        public static SoundEffect Explosion => explosions[rand.Next(explosions.Length)];
        public static SoundEffect Shot => shots[rand.Next(shots.Length)];
        public static SoundEffect Spawn => spawns[rand.Next(spawns.Length)];

        public static void Load(ContentManager content)
        {
            //todo
        }
    }
}