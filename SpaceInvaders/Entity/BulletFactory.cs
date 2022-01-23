using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Entity
{
    public interface IBulletFactory
    {
        Bullet GetRocket(Vector2 position, Vector2 velocity);
        Bullet GetDoubleDamageRocket(Vector2 position, Vector2 velocity);
    }
    public class BulletFactory : IBulletFactory
    {
        public Bullet GetRocket(Vector2 position, Vector2 velocity)
        {
            return new Rocket(position, velocity);
        }

        public Bullet GetDoubleDamageRocket(Vector2 position, Vector2 velocity)
        {
            return new DoubleDamageDecorator(new Rocket(position, velocity));
        }
    }
}
