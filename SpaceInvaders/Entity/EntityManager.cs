using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Entity
{
    public class EntityManager
    {
        private List<Entity> entities = new List<Entity>();
        private List<Enemy> enemies = new List<Enemy>();
        private List<Bullet> bullets = new List<Bullet>();
        private List<Obstacle> obstacles = new List<Obstacle>();
        private List<Entity> addedEntities = new List<Entity>();

        public bool IsUpdating { get; set; }

        public void Add(Entity entity)
        {
            if (!IsUpdating)
                AddEntity(entity);
            else
                addedEntities.Add(entity);
        }

        public int GetEnemyCount()
        {
            return enemies.Count();
        }
         
        public void ClearObstacles()
        {

            entities = entities.Except(entities.OfType<Obstacle>()).ToList();
        }

        private void AddEntity(Entity entity)
        {
            entities.Add(entity);
            switch (entity)
            {
                case Bullet bullet:
                    bullets.Add(bullet);
                    break;
                case Enemy enemy:
                    enemies.Add(enemy);
                    break;
                case Obstacle obstacle:
                    obstacles.Add(obstacle);
                    break;
            }
        }

        public void Update()
        {
            IsUpdating = true;
            HandleCollisions();

            foreach (var entity in entities)
                entity.Update();

            IsUpdating = false;

            foreach (var entity in addedEntities)
                AddEntity(entity);

            addedEntities.Clear();

            //clear expired entities
            entities = entities.Where(x => !x.IsExpired).ToList();
            bullets = bullets.Where(x => !x.IsExpired).ToList();
            enemies = enemies.Where(x => !x.IsExpired).ToList();
            obstacles = obstacles.Where(x => !x.IsExpired).ToList();
        }

        private void HandleCollisions()
        {
            // handle collisions between enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                for (int j = i + 1; j < enemies.Count; j++)
                {
                    if (IsColliding(enemies[i], enemies[j]))
                    {
                        enemies[i].HandleCollision(enemies[j]);
                        enemies[j].HandleCollision(enemies[i]);
                    }
                }
            }

            // handle collisions between bullets and enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                for (int j = 0; j < bullets.Count; j++)
                {
                    if (IsColliding(enemies[i], bullets[j]))
                    {
                        enemies[i].HandleCollision(bullets[j]);
                    }
                }
            }

            // handle collisions between the player and enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].IsActive && IsColliding(PlayerShip.Instance, enemies[i]))
                {
                    PlayerShip.Instance.HandleCollision(enemies[i]);
                    //KillPlayer();
                    //enemies[i].IsExpired = true; //delete the enemy
                    break;
                }
            }

            // handle collisions between the obstacles and bullets
            for (int i = 0; i < obstacles.Count; i++)
            {
                for (int j = 0; j < bullets.Count; j++)
                {
                    if (IsColliding(obstacles[i], bullets[j]))
                    {
                        obstacles[i].HandleCollision(bullets[j]);
                    }
                }

                //handle collision obstacle with player
                if (IsColliding(PlayerShip.Instance, obstacles[i]))
                {
                    obstacles[i].HandleCollision(PlayerShip.Instance);
                }
            }
        }

        private bool IsColliding(Entity a, Entity b)
        {
            float radius = a.Radius + b.Radius;
            return !a.IsExpired && !b.IsExpired && Vector2.DistanceSquared(a.Position, b.Position) < radius * radius;
        }

        private void KillPlayer()
        {
            PlayerShip.Instance.Kill();
            //killing all enemies
            //enemies.ForEach(x => x.WasShot());
            //blackHoles.ForEach(x => x.Kill());
            //level reset
            //EnemySpawner.Reset();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}
