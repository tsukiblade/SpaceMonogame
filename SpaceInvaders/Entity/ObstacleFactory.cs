using Microsoft.Xna.Framework;

namespace SpaceInvaders.Entity
{
    public interface IObstacleFactory
    {
        Entity CreateObstacle(Vector2 position);
    }

    public class ObstacleFactory : IObstacleFactory
    {
        public Entity CreateObstacle(Vector2 position)
        {
            return new Obstacle(position);
        }
    }
}