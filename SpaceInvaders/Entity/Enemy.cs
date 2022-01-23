using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Core;

namespace SpaceInvaders.Entity
{
    public abstract class Enemy : Entity
    {
        private int _baseHp = 2;
        private int _timeUntilStart = 60;
        public bool IsActive { get { return _timeUntilStart <= 0; } }
        private IEnemyMovementStrategy _enemyMovementStrategy;

        public int PointValue { get; private set; }

        public Enemy()
        {
        }

        protected Enemy(IEnemyMovementStrategy enemyMovementStrategy, EntityState state = null)
        {
            _enemyMovementStrategy = enemyMovementStrategy;
            if (!(state is null))
            {
                TransitionToState(state);
            }
        }

        public void SetStrategy(IEnemyMovementStrategy enemyMovementStrategy)
        {
            _enemyMovementStrategy = enemyMovementStrategy;
        }

        public override void Update()
        {
            if (_timeUntilStart <= 0)
            {
                _enemyMovementStrategy.Move(this);
            }
            else
            {
                _timeUntilStart--;
                //not active display sprite or sth change color
            }
            
            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, Game1.ScreenSize - (Size / 2));

            Velocity *= 0.8f; //todo change that
        }

        public void WasShot(Bullet bullet)
        {
            if (GetHealthPoints() <= 0)
            {
                IsExpired = true;
                PlayerContext.Instance.AddPoints(PointValue);
                return;
            }
            RemoveHealthPoints(bullet.GetDamage());
        }

        public override void OnCollision(Entity other)
        {
            if (!(other is Bullet bullet))
            {
                return;
            }
            WasShot(bullet);
            bullet.IsExpired = true;
        }

        public abstract int GetHealthPoints();
        public abstract void RemoveHealthPoints(int amount);
    }

    /*concrete enemies*/
    public class EnemyShip : Enemy
    {
        protected int _baseHealthPoints = 2;
        private int _actualHealthPoints;

        public EnemyShip(IEnemyMovementStrategy enemyMovementStrategy, EntityState state = null) : base(enemyMovementStrategy, state)
        {
            _actualHealthPoints = _baseHealthPoints;
        }

        public override int GetHealthPoints()
        {
            return _actualHealthPoints;
        }

        public override void RemoveHealthPoints(int amount)
        {
            --_baseHealthPoints;
        }
    }


    /*decorators*/

    public abstract class EnemyDecorator : Enemy
    {
        protected Enemy _enemy;

        public EnemyDecorator(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void SetEnemy(Enemy enemy)
        {
            _enemy = enemy;
        }
    }
}
