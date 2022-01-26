using System;
using Microsoft.Xna.Framework;
using SpaceInvaders.Core;
using SpaceInvaders.Extensions;
using SpaceInvaders.Helpers;

namespace SpaceInvaders.Entity
{
    public enum WeaponType
    {
        Rocket = 1,
        Bomb,
        Laser
    }

    public class PlayerShip : Entity
    {
        private const float Speed = 8;
        private const int CooldownFrames = 6;

        private static readonly Random rand = new Random();

        private static PlayerShip _instance;
        private IBulletFactory _bulletFactory;
        private int _cooldownRemaining = 0;
        private int _framesUntilActive;
        private int _framesUntilRespawn;

        private PlayerShip()
        {
            Image = Art.Player;
            Orientation = new Vector2(0, 1).ToAngle();
            Position = new Vector2(Game1.ScreenSize.X / 2, Game1.ScreenSize.Y);
            Radius = 10;

            _bulletFactory = new RocketFactory();
        }

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

        public WeaponType CurrentWeapon { get; set; } = WeaponType.Rocket;

        public int WeaponUpgradeLevel { get; private set; }

        public void UpgradeWeapon()
        {
            if (PlayerContext.Instance.Score < 10) return;
            WeaponUpgradeLevel++;
            PlayerContext.Instance.Score -= 10;
        }

        public void ChangeWeapon(WeaponType type)
        {
            _bulletFactory = type switch
            {
                WeaponType.Rocket => new RocketFactory(),
                WeaponType.Bomb => new BombFactory(),
                WeaponType.Laser => new LaserFactory(),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
            CurrentWeapon = type;
        }

        public override void Update()
        {
            if (IsDead)
            {
                if (--_framesUntilRespawn <= 0)
                    if (PlayerContext.Instance.Lives == 0)
                    {
                        PlayerContext.Instance.Reset();
                        Position = new Vector2(Game1.ScreenSize.X / 2, Game1.ScreenSize.Y);
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
            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, Game1.ScreenSize - Size / 2);

            if (Velocity.LengthSquared() > 0) Orientation = Velocity.ToAngle();

            Velocity = Vector2.Zero;
        }

        public override void OnCollision(Entity other)
        {
            Kill();
            other.HandleCollision(this);
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

            var aimAngle = aim.ToAngle();
            var aimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, aimAngle);

            var randomSpread = rand.NextFloat(-0.04f, 0.04f) + rand.NextFloat(-0.04f, 0.04f);
            var vel = MathUtil.FromPolar(aimAngle + randomSpread, 11f);

            var offset = Vector2.Transform(new Vector2(35, 8), aimQuat);

            return GetBulletAfterUpgrades(Position + offset, vel);
        }

        private Bullet GetBulletAfterUpgrades(Vector2 position, Vector2 velocity)
        {
            var bullet = _bulletFactory.CreateBullet(position, velocity);

            bullet.BulletStatistics = WeaponUpgradeLevel switch
            {
                0 => bullet.BulletStatistics,
                1 => new RangeBulletDecorator(bullet.BulletStatistics),
                2 => new DoubleDamageDecorator(bullet.BulletStatistics),
                _ => new DoubleDamageDecorator(new RangeBulletDecorator(bullet.BulletStatistics))
            };

            return bullet;
        }
    }
}