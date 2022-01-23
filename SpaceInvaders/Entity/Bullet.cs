using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Core;
using SpaceInvaders.Extensions;

namespace SpaceInvaders.Entity
{
    public abstract class Bullet : Entity
    {
        protected int _baseDamage = 1;

        public Bullet()
        {
        }
        public Bullet(Texture2D image, Vector2 position, Vector2 velocity)
        {
            _image = image;
            Position = position;
            Velocity = velocity;
            Orientation = Velocity.ToAngle();
            Radius = 8; //depends
        }

        public abstract int GetDamage();

        public override void Update()
        {
            if (Velocity.LengthSquared() > 0)
            {
                Orientation = Velocity.ToAngle();
            }
            
            Position += Velocity;

            //delete bullet that go off-screen
            if (!Game1.Viewport.Bounds.Contains(Position.ToPoint()))
            {
                IsExpired = true;
            }
        }
    }

    /* concrete classes */
    public class Rocket : Bullet
    {
        private const int rocketDamage = 1;
        public Rocket(Vector2 position, Vector2 velocity) : base(Art.Bullet, position, velocity)
        {
        }

        public override int GetDamage()
        {
            return _baseDamage;
        }
    }

    public class Bomb : Bullet
    {
        private const int bombDamage = 2;
        public Bomb(Vector2 position, Vector2 velocity) : base(Art.Bullet, position, velocity)
        {
        }

        public override int GetDamage()
        {
            return _baseDamage;
        }
    }

    public class Laser : Bullet
    {
        private const int laserDamage = 1;
        public Laser(Vector2 position, Vector2 velocity) : base(Art.Bullet, position, velocity)
        {
        }

        public override int GetDamage()
        {
            return _baseDamage;
        }
    }


    /* decorators */

    public abstract class BulletDecorator : Bullet
    {
        protected Bullet _bullet;

        public BulletDecorator(Bullet bullet)
        {
            _bullet = bullet;
        }

        public void SetBullet(Bullet bullet)
        {
            _bullet = bullet;
        }

        public override int GetDamage()
        {
            if (_bullet is null)
            {
                return 0;
            }

            return _bullet.GetDamage();
        }
    }

    public class DoubleDamageDecorator : BulletDecorator
    {
        public DoubleDamageDecorator(Bullet bullet) : base(bullet)
        {
        }

        public override int GetDamage()
        {
            return base.GetDamage()*2;
        }
    }

    public class RangeBulletDecorator : BulletDecorator
    {
        public RangeBulletDecorator(Bullet bullet) : base(bullet)
        {
        }
        //change radius or sth
    }
}
