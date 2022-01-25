using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Entity
{
    public abstract class BulletStatisticsBase
    {
        public abstract int GetDamage();
        public abstract float GetRadius();

        public virtual float GetSpeed() => 1f;
    }

    public class RocketStatistics : BulletStatisticsBase
    {
        private const int baseDamage = 1;
        private const int baseRadius = 5;
        public override int GetDamage()
        {
            return baseDamage;
        }

        public override float GetRadius()
        {
            return baseRadius;
        }
    }

    public class LaserStatistics : BulletStatisticsBase
    {
        private const int baseDamage = 1;
        private const int baseRadius = 1;
        public override int GetDamage()
        {
            return baseDamage;
        }

        public override float GetRadius()
        {
            return baseRadius;
        }
    }

    public class BombStatistics : BulletStatisticsBase
    {
        private const int baseDamage = 2;
        private const int baseRadius = 15;
        public override int GetDamage()
        {
            return baseDamage;
        }

        public override float GetRadius()
        {
            return baseRadius;
        }
    }

    /* decorators */

    public abstract class BulletStatisticsDecorator : BulletStatisticsBase
    {
        protected BulletStatisticsBase _bullet;

        public BulletStatisticsDecorator(BulletStatisticsBase bullet)
        {
            _bullet = bullet;
        }

        public void SetBullet(BulletStatisticsBase bullet)
        {
            _bullet = bullet;
        }

        public override int GetDamage()
        {
            if (_bullet is null)
            {
                throw new NullReferenceException(nameof(_bullet));
            }

            return _bullet.GetDamage();
        }

        public override float GetRadius()
        {
            if (_bullet is null)
            {
                throw new NullReferenceException(nameof(_bullet));
            }

            return _bullet.GetRadius();
        }

        public override float GetSpeed()
        {
            if (_bullet is null)
            {
                throw new NullReferenceException(nameof(_bullet));
            }

            return _bullet.GetSpeed();
        }
    }

    public class DoubleDamageDecorator : BulletStatisticsDecorator
    {
        public DoubleDamageDecorator(BulletStatisticsBase bullet) : base(bullet)
        {
        }

        public override int GetDamage()
        {
            return base.GetDamage() * 2;
        }
    }

    public class RangeBulletDecorator : BulletStatisticsDecorator
    {
        public RangeBulletDecorator(BulletStatisticsBase bullet) : base(bullet)
        {
        }

        public override float GetRadius()
        {
            return base.GetRadius() * 1.5f;
        }
    }

    public class SpeedBulletDecorator : BulletStatisticsDecorator
    {
        public SpeedBulletDecorator(BulletStatisticsBase bullet) : base(bullet)
        {
        }

        public override float GetSpeed()
        {
            return base.GetSpeed() * 1.5f;
        }
    }
}
