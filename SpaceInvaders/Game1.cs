using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Core;
using SpaceInvaders.Entity;

namespace SpaceInvaders
{
    public class Game1 : Game
    {
        private bool _paused = false;
        public static Game1 Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static GameTime GameTime { get; private set; }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private EntityManager _entityManager;

        public ICommand FireCommand { get; set; }

        public Game1()
        {
            Instance = this;
            _graphics = new GraphicsDeviceManager(this);
            _entityManager = new EntityManager();
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            FireCommand = new FireCommand(_entityManager);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Art.Load(Content);
            Sound.Load(Content);

            _entityManager.Add(PlayerShip.Instance);
            //load game level here

            foreach (var entity in GameManager.Instance.LoadGameLevel("xxx"))
            {
                //loading every entity from game level
                _entityManager.Add(entity);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            Input.Update();

            /* input controls */

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Input.WasKeyPressed(Keys.P))
                _paused = !_paused;

            if (Input.WasFireKeyPressed())
            {
                FireCommand.Execute(); //execute command
            }

            if (!_paused)
            {
                _entityManager.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
            _entityManager.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);

            //draw ui here
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            DrawTitleSafeAlignedString("Lives: " + PlayerContext.Instance.Lives, 5);
            DrawTitleSafeRightAlignedString("Score: " + PlayerContext.Instance.Score, 5);
            if (_paused)
            {
                DrawTitleSafeCenterAlignedString("Paused", (int)(Game1.ScreenSize.Y/2));
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
            _spriteBatch.DrawString(Art.Font, text, new Vector2(ScreenSize.X/2 - textWidth - 5 - Viewport.TitleSafeArea.X, ScreenSize.Y/2 - textHeight - Viewport.TitleSafeArea.Y + y), Color.White);
        }
    }
}
