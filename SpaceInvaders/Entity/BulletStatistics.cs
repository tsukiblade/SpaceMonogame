using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Entity
{
    public abstract class BulletStatisticsBase
    {
        public abstract int GetDamage();
        public abstract float GetRadius();
    }

    public class RocketStatistics : BulletStatisticsBase
    {
        public override int GetDamage()
        {
            return 1;
        }

        public override float GetRadius()
        {
            return 1;
        }
    }

    public class BombStatistics : BulletStatisticsBase
    {
        public override int GetDamage()
        {
            return 2;
        }

        public override float GetRadius()
        {
            return 3;
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
}
