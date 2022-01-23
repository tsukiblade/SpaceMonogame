using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Entity
{
    public interface IEnemyFactory
    {
        Enemy CreateEnemy(Vector2 position);
        Enemy CreateWeakEnemy(Vector2 position);
        Enemy CreateStrongEnemy(Vector2 position);
    }

    public class EnemyShipFactory : IEnemyFactory
    {
        public Enemy CreateEnemy(Vector2 position)
        {
            return new EnemyShip(position);
        }

        public Enemy CreateStrongEnemy(Vector2 position)
        {
            return new EnemyShip(position)
            {
                Statistics = new StrongEnemyStatisticsDecorator(new EnemyShipStatistics())
            };
        }

        public Enemy CreateWeakEnemy(Vector2 position)
        {
            return new EnemyShip(position)
            {
                Statistics = new WeakEnemyStatisticsDecorator(new EnemyShipStatistics())
            };
        }
    }

    public class EnemyAlienFactory : IEnemyFactory
    {
        public Enemy CreateEnemy(Vector2 position)
        {
            return new EnemyAlien(position);
        }

        public Enemy CreateWeakEnemy(Vector2 position)
        {
            return new EnemyAlien(position)
            {
                Statistics = new WeakEnemyStatisticsDecorator(new EnemyAlienStatistics())
            };
        }

        public Enemy CreateStrongEnemy(Vector2 position)
        {
            return new EnemyAlien(position)
            {
                Statistics = new StrongEnemyStatisticsDecorator(new EnemyAlienStatistics())
            };
        }
    }
}
