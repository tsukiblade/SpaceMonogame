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

        // return a random explosion sound
        public static SoundEffect Explosion => explosions[rand.Next(explosions.Length)];
        public static SoundEffect Shot => shots[rand.Next(shots.Length)];
        public static SoundEffect Spawn => spawns[rand.Next(spawns.Length)];

        public static void Load(ContentManager content)
        {
            //Music = content.Load<Song>("Audio/Music");

            // These linq expressions are just a fancy way loading all sounds of each category into an array.
            //explosions = Enumerable.Range(1, 8).Select(x => content.Load<SoundEffect>("Audio/explosion-0" + x)).ToArray();
            //shots = Enumerable.Range(1, 4).Select(x => content.Load<SoundEffect>("Audio/shoot-0" + x)).ToArray();
            //spawns = Enumerable.Range(1, 8).Select(x => content.Load<SoundEffect>("Audio/spawn-0" + x)).ToArray();
        }
    }
}