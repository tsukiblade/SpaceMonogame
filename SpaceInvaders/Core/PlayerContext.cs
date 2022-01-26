using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Core
{
    public sealed class PlayerContext
    {
        private PlayerContext()
        {
            Reset();
        }

        private static PlayerContext _instance;

        public static PlayerContext Instance
        {
            get
            {
                if (_instance is null) //lazy init
                {
                    _instance = new PlayerContext();
                }

                return _instance;
            }
        }

        public int Lives { get; set; }

        public int Score { get; set; }

        public bool IsGameOver => Lives == 0;

        public void Reset()
        {
            Score = 0;
            Lives = 4;
        }

        public void AddPoints(int points)
        {
            Score += points;
        }

        public void RemoveLife()
        {
            Lives--;
            if (Lives == 0)
            {
                //dead logic here
            }
        }

        private int LoadHighScore()
        {
            return 0;
        }

        private void SaveHighScore()
        {

        }
    }
}
