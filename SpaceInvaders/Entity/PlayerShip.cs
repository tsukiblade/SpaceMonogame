using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Core;
using SpaceInvaders.Extensions;
using SpaceInvaders.Helpers;

namespace SpaceInvaders.Entity
{
    public class PlayerShip : Entity
    {
        private const float Speed = 8;
        private const int CooldownFrames = 6;
        private int _cooldownRemaining = 0;
        private int _framesUntilActive = 0;
        private int _framesUntilRespawn = 0;
        private readonly IBulletFactory _bulletFactory;

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

        public bool IsDead => _framesUntilRespawn > 0;

        public bool IsActive => _framesUntilActive <= 0;

        public PlayerShip()
        {
            Image = Art.Player;
            Orientation = new Vector2(0,1).ToAngle();
            Position = new Vector2(Game1.ScreenSize.X/2, Game1.ScreenSize.Y);
            Radius = 10;

            _bulletFactory = new RocketFactory();
        }

        public override void Update()
        {
            if (IsDead)
            {
                if (--_framesUntilRespawn <= 0)
                {
                    if (PlayerContext.Instance.Lives == 0)
                    {
                        PlayerContext.Instance.Reset();
                        Position = new Vector2(Game1.ScreenSize.X / 2, Game1.ScreenSize.Y);
                    }
                }

                return;
            }

            if (_framesUntilActive < 0)
            {
                TransitionToState(new DefaultEntityState());
                _framesUntilActive = 0;
            }
            else
            {
                --_framesUntilActive;
            }

            Velocity += Speed * Input.GetMovementDirection();
            //Velocity += Speed * Input.GetMovementInput();
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
            //if (!IsDead)
            //{
            //    base.Draw(spriteBatch);
            //}
            base.Draw(spriteBatch);
        }

        public void Kill()
        {
            PlayerContext.Instance.RemoveLife();
            _framesUntilRespawn = PlayerContext.Instance.IsGameOver ? 300 : 120;
            _framesUntilActive = _framesUntilRespawn + 180;
            TransitionToState(new GodModeEntityState());
        }

        public Bullet FireBullet()
        {
            var aim = Input.GetAimDirection();
            
            float aimAngle = aim.ToAngle();
            Quaternion aimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, aimAngle);

            float randomSpread = rand.NextFloat(-0.04f, 0.04f) + rand.NextFloat(-0.04f, 0.04f);
            Vector2 vel = MathUtil.FromPolar(aimAngle + randomSpread, 11f);

            Vector2 offset = Vector2.Transform(new Vector2(35, 8), aimQuat);

            return _bulletFactory.CreateBullet(Position + offset, vel);
        }
    }
}
