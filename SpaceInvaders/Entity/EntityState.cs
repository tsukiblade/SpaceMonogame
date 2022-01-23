﻿using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Entity
{
    public abstract class EntityState
    {
        protected Entity EntityContext;

        /*
         * By the context we can chagne state within this state
         */

        public void SetEntityContext(Entity entity)
        {
            EntityContext = entity;
        }

        //some shared methods

        //public abstract void HandleDamage();

        public abstract void Draw(SpriteBatch spriteBatch);
        
        public abstract void HandleCollision(Entity other);
    }

    public class DefaultEntityState : EntityState
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EntityContext.Image, EntityContext.Position, null, EntityContext.color, EntityContext.Orientation, EntityContext.Size / 2f, 1f, 0, 0);
        }

        public override void HandleCollision(Entity other)
        {
            var d = EntityContext.Position - other.Position;
            EntityContext.Velocity += 10 * d / (d.LengthSquared() + 1);
            EntityContext.OnCollision(other);
        }
    }

    public class GodModeEntityState : EntityState
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.GameTime.TotalGameTime.Milliseconds%6 == 0)
                spriteBatch.Draw(EntityContext.Image, EntityContext.Position, null, EntityContext.color, EntityContext.Orientation, EntityContext.Size / 2f, 1f, 0, 0);
        }

        public override void HandleCollision(Entity other)
        {
            var d = EntityContext.Position - other.Position;
            EntityContext.Velocity += 10 * d / (d.LengthSquared() + 1);
        }
    }
}
