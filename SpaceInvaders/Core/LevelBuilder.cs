using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SpaceInvaders.Entity;

namespace SpaceInvaders.Core
{
    public enum ObjectType
    {
        Ship = 1,
        Alien,
        Obstacle
    }

    public enum DifficultyType
    {
        Standard = 1,
        Weak,
        Strong
    }

    public enum StrategyType
    {
        Follow = 1,
        Chaotic
    }

    public interface ILevelBuilder
    {
        void BuildShip(Vector2 position, DifficultyType difficultyType);
        void BuildAlien(Vector2 position, DifficultyType difficultyType);
        void BuildObstacle(Vector2 position);

        GameLevel GetCompleteLevel();
    }

    public class LevelBuilder : ILevelBuilder
    {
        private readonly IEnemyFactory _alienEnemyFactory;
        private readonly IEnemyFactory _enemyShipFactory;
        private readonly IObstacleFactory _obstacleFactory;
        private readonly List<Entity.Entity> _entities;

        public LevelBuilder()
        {
            _entities = new List<Entity.Entity>();
            _alienEnemyFactory = new EnemyAlienFactory();
            _enemyShipFactory = new EnemyShipFactory();
            _obstacleFactory = new ObstacleFactory();
        }

        public void BuildShip(Vector2 position, DifficultyType difficultyType)
        {
            var entity = difficultyType switch
            {
                DifficultyType.Standard => _enemyShipFactory.CreateEnemy(position),
                DifficultyType.Weak => _enemyShipFactory.CreateWeakEnemy(position),
                DifficultyType.Strong => _enemyShipFactory.CreateStrongEnemy(position),
                _ => throw new ArgumentOutOfRangeException(nameof(difficultyType))
            };
            _entities.Add(entity);
        }

        public void BuildAlien(Vector2 position, DifficultyType difficultyType)
        {
            var entity = difficultyType switch
            {
                DifficultyType.Standard => _alienEnemyFactory.CreateEnemy(position),
                DifficultyType.Weak => _alienEnemyFactory.CreateWeakEnemy(position),
                DifficultyType.Strong => _alienEnemyFactory.CreateStrongEnemy(position),
                _ => throw new ArgumentOutOfRangeException(nameof(difficultyType))
            };
            _entities.Add(entity);
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