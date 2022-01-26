using System;
using System.Collections.Generic;

namespace SpaceInvaders.Core
{
    public class GameManagerMemento : IGameManagerMemento
    {
        private readonly DateTime _creationDate;
        private readonly int _currentLevel;
        private readonly List<ScoreData> _scoreData;

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