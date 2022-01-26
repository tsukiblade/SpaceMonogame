using System;

namespace SpaceInvaders.Core
{
    public class ScoreData
    {
        public ScoreData()
        {
            Score = 0;
            Date = DateTime.Now;
        }

        public int Score { get; set; }

        /// <summary>
        ///     When score was entered
        /// </summary>
        public DateTime Date { get; }
    }
}