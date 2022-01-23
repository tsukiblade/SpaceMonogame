using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Entity
{
    public interface IBulletFactory
    {
        Bullet CreateBullet(Vector2 position, Vector2 velocity);
        Bullet CreateDoubleDamageBullet(Vector2 position, Vector2 velocity);
        Bullet CreateBigBullet(Vector2 position, Vector2 velocity);
    }
    public class RocketFactory : IBulletFactory
    {
        public Bullet CreateBullet(Vector2 position, Vector2 velocity)
        {
            return new Rocket(position, velocity);
        }

        public Bullet CreateDoubleDamageBullet(Vector2 position, Vector2 velocity)
        {
            return new DoubleDamageDecorator(new Rocket(position, velocity));
        }

        public Bullet CreateBigBullet(Vector2 position, Vector2 velocity)
        {
            return new RangeBulletDecorator(new Rocket(position, velocity));
        }
    }

    public class BombFactory : IBulletFactory
    {
        public Bullet CreateBullet(Vector2 position, Vector2 velocity)
        {
            return new Bomb(position, velocity);
        }

        public Bullet CreateDoubleDamageBullet(Vector2 position, Vector2 velocity)
        {
            return new DoubleDamageDecorator(new Bomb(position, velocity));
        }

        public Bullet CreateBigBullet(Vector2 position, Vector2 velocity)
        {
            return new RangeBulletDecorator(new Rocket(position, velocity));
        }
    }
}
