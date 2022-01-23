using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Entity;

namespace SpaceInvaders.Core
{
    public interface ILevelBuilder
    {
        void BuildEnemyAlien(Vector2 position);
        void BuildEnemyShip(Vector2 position);

        void BuildObstacle(Vector2 position);

        GameLevel GetCompleteLevel();
    }
    public class LevelBuilder : ILevelBuilder
    {
        private List<Entity.Entity> _entities;
        private readonly IEnemyFactory _alienEnemyFactory;
        private readonly IEnemyFactory _enemyShipFactory;
        private readonly IObstacleFactory _obstacleFactory;

        public LevelBuilder()
        {
            _entities = new List<Entity.Entity>();
            _alienEnemyFactory = new EnemyAlienFactory();
            _enemyShipFactory = new EnemyShipFactory();
            _obstacleFactory = new ObstacleFactory();
        }

        public void BuildEnemyAlien(Vector2 position)
        {
            var alien = _alienEnemyFactory.CreateEnemy(position);
            _entities.Add(alien);
        }

        public void BuildEnemyShip(Vector2 position)
        {
            var ship = _enemyShipFactory.CreateEnemy(position);
            _entities.Add(ship);
        }

        public void BuildObstacle(Vector2 position)
        {
            var entity = _obstacleFactory.CreateObstacle(position);
            _entities.Add(entity);
        }

        public GameLevel GetCompleteLevel()
        {
            return new GameLevel
            {
                Entities = _entities,
                LevelName = "test01"
            };
        }
    }
}
