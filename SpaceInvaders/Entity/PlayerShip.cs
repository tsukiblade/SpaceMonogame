using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Core;
using SpaceInvaders.Extensions;

namespace SpaceInvaders.Entity
{
    public class PlayerShip : Entity
    {
        private const float Speed = 8;
        private const int CooldownFrames = 6;
        private int _cooldownRemaining = 0;
        private int _framesUntilRespawn = 0;

        static Random rand = new Random();

        private static PlayerShip _instance;
        public static PlayerShip Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlayerShip();

                return _instance;
            }
        }

        public bool IsDead { get { return _framesUntilRespawn > 0; } }

        public PlayerShip()
        {
            Image = Art.Player;
            Position = Game1.ScreenSize / 2;
            Radius = 10;
        }

        public override void Update()
        {
            if (IsDead)
            {
                if (--_framesUntilRespawn == 0)
                {
                    if (PlayerContext.Instance.Lives == 0)
                    {
                        PlayerContext.Instance.Reset();
                        Position = Game1.ScreenSize / 2;
                    }
                }

                return;
            }

            Velocity += Speed * Input.GetMovementDirection();
            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, Game1.ScreenSize - (Size / 2));

            if (Velocity.LengthSquared() > 0)
            {
                Orientation = Velocity.ToAngle();
            }

            Velocity = Vector2.Zero;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDead)
            {
                base.Draw(spriteBatch);
            }
        }

        public void Kill()
        {
            PlayerContext.Instance.RemoveLife();
            _framesUntilRespawn = PlayerContext.Instance.IsGameOver ? 300 : 120;
        }
    }
}
