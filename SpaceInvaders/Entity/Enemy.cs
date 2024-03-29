﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Core;

namespace SpaceInvaders.Entity
{
    public abstract class Enemy : Entity
    {
        private IEnemyMovementStrategy _enemyMovementStrategy;
        private int _timeUntilStart = 60;

        public Enemy()
        {
        }

        protected Enemy(Texture2D image, Vector2 position)
        {
            Image = image;
            Position = position;

            _enemyMovementStrategy = new FollowPlayerStrategy();
        }

        public bool IsActive => _timeUntilStart <= 0;

        public int PointValue { get; private set; }
        public EnemyStatistics Statistics { get; set; }

        public void SetStrategy(IEnemyMovementStrategy enemyMovementStrategy)
        {
            _enemyMovementStrategy = enemyMovementStrategy;
        }

        public override void Update()
        {
            if (_timeUntilStart <= 0)
                _enemyMovementStrategy.Move(this);
            else
                _timeUntilStart--;

            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, Game1.ScreenSize - Size / 2);

            Velocity *= 0.8f; 
        }

        public void WasShot(Bullet bullet)
        {
            if (Statistics.GetHealthPoints() <= 0)
            {
                IsExpired = true;
                PlayerContext.Instance.AddPoints(Statistics.GetValuePoints());
                return;
            }

            Statistics.RemoveHealthPoints(bullet.GetDamage());
        }

        public override void OnCollision(Entity other)
        {
            if (other is PlayerShip player) IsExpired = true;

            if (!(other is Bullet bullet)) return;

            WasShot(bullet);
            bullet.IsExpired = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color = Statistics.GetColor();
            base.Draw(spriteBatch);
        }
    }

    /*concrete enemies*/
    public class EnemyShip : Enemy
    {
        public EnemyShip(Vector2 position) : base(Art.Ship, position)
        {
            Statistics = new EnemyShipStatistics();
            Radius = 5;
        }
    }

    public class EnemyAlien : Enemy
    {
        public EnemyAlien(Vector2 position) : base(Art.Alien, position)
        {
            Statistics = new EnemyAlienStatistics();
            Radius = 10;
        }
    }
}