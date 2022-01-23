using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Core
{
    public class GameManagerCaretaker
    {
        readonly List<IGameManagerMemento> _gameManagerMementos = new List<IGameManagerMemento>();

        private readonly GameManager _gameManager;

        public GameManagerCaretaker(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void Backup()
        {
            _gameManagerMementos.Add(_gameManager.Save());
        }

        public void Undo()
        {
            if (_gameManagerMementos.Count == 0)
            {
                return;
            }

            var memento = _gameManagerMementos.Last();
            _gameManagerMementos.Remove(memento);

            try
            {
                _gameManager.Restore(memento);
            }
            catch (Exception)
            {
                Undo();
            }
        }

        public void ShowHistory()
        {
            foreach (var memento in _gameManagerMementos)
            {
                Console.WriteLine($"Level: {memento.GetLevel()}, Scores amount: {memento.GetScoreData().Count()}, Created {memento.GetDate()}.");
            }
        }
    }
}
