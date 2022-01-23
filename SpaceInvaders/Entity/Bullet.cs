using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Extensions;

namespace SpaceInvaders.Entity
{
    public class Bullet : Entity
    {
        public Bullet(Texture2D image, Vector2 position, Vector2 velocity)
        {
            _image = image;
            Position = position;
            Velocity = velocity;
            Orientation = Velocity.ToAngle();
            Radius = 8; //depends
        }

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
}
