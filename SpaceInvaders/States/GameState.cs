using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Core;
using SpaceInvaders.Entity;

namespace SpaceInvaders.States
{
    public class GameState : State
    {
        private bool _paused = false;
        public static Game1 Instance { get; private set; }
        public static Viewport Viewport => Instance.GraphicsDevice.Viewport;
        public static Vector2 ScreenSize => new Vector2(Viewport.Width, Viewport.Height);
        public static GameTime GameTime { get; private set; }

        private SpriteBatch _spriteBatch;
        private EntityManager _entityManager;

        public ICommand FireCommand { get; }
        public ICommand<WeaponType> ChangeWeaponCommand { get; }


        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            Instance = game;
            _entityManager = new EntityManager();
            game.IsMouseVisible = false;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            FireCommand = new FireCommand(_entityManager);
            ChangeWeaponCommand = new ChangeWeaponCommand();

            _entityManager.Add(PlayerShip.Instance);
            //load game level here

            foreach (var entity in GameManager.Instance.LoadGameLevel("xxx"))
            {
                //loading every entity from game level
                _entityManager.Add(entity);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
            _entityManager.Draw(_spriteBatch);
            _spriteBatch.End();


            //draw ui here
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            DrawTitleSafeAlignedString("Lives: " + PlayerContext.Instance.Lives, 5);
            DrawTitleSafeAlignedString("Weapon: " + PlayerShip.Instance.CurrentWeapon, 15);
            DrawTitleSafeRightAlignedString("Score: " + PlayerContext.Instance.Score, 5);
            if (_paused)
            {
                DrawTitleSafeCenterAlignedString("Paused", (int)(Game1.ScreenSize.Y / 2));
            }

            _spriteBatch.Draw(Art.Pointer, Input.MousePosition, Color.White);

            if (PlayerContext.Instance.IsGameOver)
            {
                string text = "Game Over\n" +
                              "Your Score: " + PlayerContext.Instance.Score + "\n";
                //"High Score: " + PlayerStatus.HighScore;

                Vector2 textSize = Art.Font.MeasureString(text);
                _spriteBatch.DrawString(Art.Font, text, ScreenSize / 2 - textSize / 2, Color.White);
            }
            _spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            Input.Update();

            /* input controls */

            

            if (Input.WasKeyPressed(Keys.P))
                _paused = !_paused;

            if (Input.WasFireKeyPressed())
            {
                FireCommand.Execute(); //execute command
            }
            
            ChangeWeaponType();
            

            if (!_paused)
            {
                _entityManager.Update();
            }
        }

        private void ChangeWeaponType()
        {
            if (Input.WasRocketKeyPressed())
            {
                ChangeWeaponCommand.Execute(WeaponType.Rocket);
            }

            if (Input.WasBombKeyPressed())
            {
                ChangeWeaponCommand.Execute(WeaponType.Bomb);
            }

            if (Input.WasLaserKeyPressed())
            {
                ChangeWeaponCommand.Execute(WeaponType.Laser);
            }
        }

        private void DrawRightAlignedString(string text, float y)
        {
            var textWidth = Art.Font.MeasureString(text).X;
            _spriteBatch.DrawString(Art.Font, text, new Vector2(ScreenSize.X - textWidth - 5, y), Color.White);
        }

        private void DrawTitleSafeAlignedString(string text, int pos)
        {
            _spriteBatch.DrawString(Art.Font, text, new Vector2(Viewport.TitleSafeArea.X + pos), Color.White);
        }

        private void DrawTitleSafeRightAlignedString(string text, float y)
        {
            var textWidth = Art.Font.MeasureString(text).X;
            _spriteBatch.DrawString(Art.Font, text, new Vector2(ScreenSize.X - textWidth - 5 - Viewport.TitleSafeArea.X, Viewport.TitleSafeArea.Y + y), Color.White);
        }

        private void DrawTitleSafeCenterAlignedString(string text, float y)
        {
            var textWidth = Art.Font.MeasureString(text).X;
            var textHeight = Art.Font.MeasureString(text).Y;
            _spriteBatch.DrawString(Art.Font, text, new Vector2(ScreenSize.X / 2 - textWidth - 5 - Viewport.TitleSafeArea.X, ScreenSize.Y / 2 - textHeight - Viewport.TitleSafeArea.Y + y), Color.White);
        }
    }

}