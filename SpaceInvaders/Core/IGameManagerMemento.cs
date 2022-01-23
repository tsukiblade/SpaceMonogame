using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Core
{
    public interface IGameManagerMemento
    {
        int GetLevel();
        List<ScoreData> GetScoreData();
        DateTime GetDate();
    }
}
