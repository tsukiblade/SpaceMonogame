using System;
using System.Collections.Generic;
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
}
