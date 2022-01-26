using Microsoft.Xna.Framework;
using SpaceInvaders.Core;

namespace SpaceInvaders.Entity
{
    public class Obstacle : Entity
    {
        public Obstacle()
        {
        }

        public Obstacle(Vector2 position)
        {
            _image = Art.Obstacle;
            Position = position;
            Radius = 15; //todo change that
        }

        public override void Update()
        {
        }

        public override void OnCollision(Entity other)
        {
            if (other is Bullet bullet)
            {
                bullet.IsExpired = true;
                return;
            }

            if (other is PlayerShip { IsActive: true } player)
            {
                player.Kill();
                IsExpired = true;
            }
        }
    }
}