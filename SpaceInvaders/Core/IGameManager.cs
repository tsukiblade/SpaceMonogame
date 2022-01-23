using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Core
{
    public interface IGameManager
    {
        IGameManagerMemento Save();
        void Restore(IGameManagerMemento memento);
        void ShowScores();
    }
}
