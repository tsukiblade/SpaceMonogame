using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Extensions;
using SpaceInvaders.Helpers;

namespace SpaceInvaders.Entity
{
    public interface IEnemyMovementStrategy
    {
        void Move(Enemy entity);
    }

    public class StandardEnemyMovementStrategy : IEnemyMovementStrategy
    {
        public void Move(Enemy entity)
        {
            throw new NotImplementedException();
        }
    }

    public class ChaoticEnemyMovementStrategy : IEnemyMovementStrategy
    {
        public static Random rand = new Random();
        public void Move(Enemy entity)
        {
            float direction = rand.NextFloat(0, MathHelper.TwoPi);
            direction = MathHelper.WrapAngle(direction);
            for (int i = 0; i < 6; i++)
            {
                entity.Velocity += MathUtil.FromPolar(direction, 0.4f);
                entity.Orientation -= 0.05f;

                var bounds = Game1.Viewport.Bounds;
                bounds.Inflate((-entity.Image.Width / 2) - 1, (-entity.Image.Height / 2) - 1);

                // if the enemy is outside the bounds, make it move away from the edge
                if (!bounds.Contains(entity.Position.ToPoint()))
                    direction = (Game1.ScreenSize / 2 - entity.Position).ToAngle() + rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2);
            }
        }
    }

    public class FollowPlayerStrategy : IEnemyMovementStrategy
    {
        public void Move(Enemy entity)
        {
            const float acceleration = 0.9f;
            while (true)
            {
                if (true /* player is alive*/)
                {
                    entity.Velocity += (PlayerShip.Instance.Position - entity.Position).ScaleTo(acceleration);
                }

                if (entity.Velocity != Vector2.Zero)
                    entity.Orientation = entity.Velocity.ToAngle();
            }
        }
    }
}
