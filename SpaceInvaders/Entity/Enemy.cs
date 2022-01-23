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
        protected readonly int BaseHealthPoints = 2;
        protected int DamageTaken;
        private int _timeUntilStart = 60;
        public bool IsActive => _timeUntilStart <= 0;
        private IEnemyMovementStrategy _enemyMovementStrategy;

        public int PointValue { get; private set; }

        public Enemy()
        {
        }

        protected Enemy(Texture2D image, Vector2 position)
        {
            Image = image;
            Position = position;

            _enemyMovementStrategy = new StandardEnemyMovementStrategy();
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
        public EnemyShip(Vector2 position) : base(Art.Bullet, position)
        {
        }

        public override int GetHealthPoints()
        {
            return BaseHealthPoints - DamageTaken;
        }

        public override void RemoveHealthPoints(int amount)
        {
            DamageTaken++;
        }
    }

    public class EnemyAlien : Enemy
    {
        public EnemyAlien(Vector2 position) : base(Art.Bullet, position)
        {
        }

        public override int GetHealthPoints()
        {
            return BaseHealthPoints - DamageTaken;
        }

        public override void RemoveHealthPoints(int amount)
        {
            DamageTaken++;
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

        public override int GetHealthPoints()
        {
            if (_enemy is null)
            {
                throw new NullReferenceException(nameof(_enemy));
            }

            return _enemy.GetHealthPoints();
        }

        public override void RemoveHealthPoints(int amount)
        {
            if (_enemy is null)
            {
                throw new NullReferenceException(nameof(_enemy));
            }

            _enemy.RemoveHealthPoints(amount);
        }
    }

    public class WeakEnemyDecorator : EnemyDecorator
    {
        public WeakEnemyDecorator(Enemy enemy) : base(enemy)
        {
        }

        public override int GetHealthPoints()
        {
            return BaseHealthPoints - 1 - DamageTaken;
        }

        public override void RemoveHealthPoints(int amount)
        {
            base.RemoveHealthPoints(amount);
        }
    }

    public class StrongEnemyDecorator : EnemyDecorator
    {
        public StrongEnemyDecorator(Enemy enemy) : base(enemy)
        {
        }

        public override int GetHealthPoints()
        {
            return BaseHealthPoints + 5 - DamageTaken;
        }

        public override void RemoveHealthPoints(int amount)
        {
            base.RemoveHealthPoints(amount);
        }
    }
}
