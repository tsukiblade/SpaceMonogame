using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Core
{
    public class ScoreData
    {
        public int Score { get; set; }

        /// <summary>
        /// When score was entered
        /// </summary>
        public DateTime Date { get; }

        public ScoreData()
        {
            Score = 0;
            Date = DateTime.Now;
        }
    }
}
