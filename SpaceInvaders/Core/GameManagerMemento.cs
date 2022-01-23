using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Core
{
    public class GameManagerMemento : IGameManagerMemento
    {
        private int _currentLevel;
        private List<ScoreData> _scoreData;
        private DateTime _creationDate;

        public GameManagerMemento(int currentLevel, List<ScoreData> scoreData)
        {
            _currentLevel = currentLevel;
            _scoreData = scoreData;
            _creationDate = DateTime.Now;
        }

        public DateTime GetDate()
        {
            return _creationDate;
        }

        public int GetLevel()
        {
            return _currentLevel;
        }

        public List<ScoreData> GetScoreData()
        {
            return _scoreData;
        }
    }
}
