using System;
using System.Collections.Generic;

namespace SpaceInvaders.Core
{
    public interface IGameManagerMemento
    {
        int GetLevel();
        List<ScoreData> GetScoreData();
        DateTime GetDate();
    }
}