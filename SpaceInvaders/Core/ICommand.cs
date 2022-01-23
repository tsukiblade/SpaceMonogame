using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Entity;

namespace SpaceInvaders.Core
{
    public interface ICommand
    {
        void Execute();
    }

    public class FireCommand : ICommand
    {
        private readonly EntityManager _entityManager;

        public FireCommand(EntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public void Execute()
        {
            var bullet = PlayerShip.Instance.FireBullet();
            _entityManager.Add(bullet);
        }
    }

    //Caretaker/command
    public class SaveGameCommand : ICommand
    {
        List<IGameManagerMemento> gameManagerMementos = new List<IGameManagerMemento>();

        private GameManager _gameManager;

        public SaveGameCommand(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        public void Execute()
        {
            Backup();//other logic?
        }

        public void Backup()
        {
            gameManagerMementos.Add(_gameManager.Save());
        }

        public void Undo()
        {
            if (gameManagerMementos.Count == 0)
            {
                return;
            }

            var memento = gameManagerMementos.Last();
            gameManagerMementos.Remove(memento);

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
            foreach (var memento in gameManagerMementos)
            {
                Console.WriteLine($"Level: {memento.GetLevel()}, Scores amount: {memento.GetScoreData().Count()}, Created {memento.GetDate()}.");
            }
        }
    }
}
