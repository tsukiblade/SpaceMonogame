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
        private int _timeUntilStart = 60;
        public bool IsActive { get { return _timeUntilStart <= 0; } }
        private IEnemyMovementStrategy _enemyMovementStrategy;

        public int PointValue { get; private set; }

        protected Enemy(IEnemyMovementStrategy enemyMovementStrategy, EntityState state = null)
        {
            _enemyMovementStrategy = enemyMovementStrategy;
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

        public void WasShot()
        {
            IsExpired = true;
            PlayerContext.Instance.AddPoints(PointValue);
        }

        public override void OnCollision(Entity other)
        {
            if (!(other is Bullet bullet))
            {
                return;
            }
            WasShot();
            bullet.IsExpired = true;
        }
    }

    public class EnemyDecorator : Enemy
    {
        public EnemyDecorator(IEnemyMovementStrategy enemyMovementStrategy, EntityState state = null) : base(enemyMovementStrategy, state)
        {
        }
    }
}
