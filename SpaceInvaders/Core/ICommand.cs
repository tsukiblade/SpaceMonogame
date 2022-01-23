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

    public interface ICommand<T>
    where T : Enum
    {
        void Execute(T type);
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

    public class ChangeWeaponCommand : ICommand<WeaponType>
    {
        public void Execute(WeaponType type)
        {
            PlayerShip.Instance.ChangeWeapon(type);
        }
    }

    //Caretaker/command
    public class SaveGameCommand : ICommand
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
