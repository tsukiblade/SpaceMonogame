using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Entity
{
    public abstract class EnemyStatistics
    {
        protected readonly int BaseHealthPoints = 2;
        protected int DamageTaken;

        public abstract int GetHealthPoints();
        public abstract void RemoveHealthPoints(int amount);

        public abstract Color GetColor();
    }

    public class EnemyAlienStatistics : EnemyStatistics
    {
        public override int GetHealthPoints()
        {
            return BaseHealthPoints - DamageTaken;
        }

        public override void RemoveHealthPoints(int amount)
        {
            DamageTaken++;
        }

        public override Color GetColor()
        {
            return Color.White;
        }
    }
    public class EnemyShipStatistics : EnemyStatistics
    {
        public override int GetHealthPoints()
        {
            return BaseHealthPoints - DamageTaken;
        }

        public override void RemoveHealthPoints(int amount)
        {
            DamageTaken++;
        }

        public override Color GetColor()
        {
            return Color.White;
        }
    }

    public abstract class EnemyStatisticsDecorator : EnemyStatistics
    {
        protected EnemyStatistics _enemy;

        public EnemyStatisticsDecorator(EnemyStatistics enemy)
        {
            _enemy = enemy;
        }

        public void SetEnemy(EnemyStatistics enemy)
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

    public class WeakEnemyStatisticsDecorator : EnemyStatisticsDecorator
    {
        public WeakEnemyStatisticsDecorator(EnemyStatistics enemy) : base(enemy)
        {
        }

        public override int GetHealthPoints()
        {
            return BaseHealthPoints - 1 - DamageTaken;
        }

        public override Color GetColor()
        {
            return Color.GreenYellow;
        }
    }

    public class StrongEnemyStatisticsDecorator : EnemyStatisticsDecorator
    {
        public StrongEnemyStatisticsDecorator(EnemyStatistics enemy) : base(enemy)
        {
        }

        public override int GetHealthPoints()
        {
            return BaseHealthPoints + 5 - DamageTaken;
        }

        public override Color GetColor()
        {
            return Color.DarkRed;
        }
    }
}
