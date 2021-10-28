using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Camera;
using Platformer.Character;

namespace Platformer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }

        PlayerCharacterController playerController;
        EnemyCharacterController enemyController;
        GameCamera camera;
        PlayerCharacter huntress;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

            camera = new GameCamera();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Huntress.LoadContent(Content);
            huntress = new Huntress(new Vector2(0, 0));
            playerController = new PlayerCharacterController(huntress);

            Skeleton.LoadContent(Content);
            Skeleton skeleton = new Skeleton(new Vector2(50, 250));
            enemyController = new EnemyCharacterController(skeleton, 50, 300);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            playerController.Update(gameTime);
            enemyController.Update(gameTime);

            camera.Follow(huntress.Position);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            _spriteBatch.Begin(transformMatrix: camera.Transform);
            playerController.Draw(gameTime, _spriteBatch);
            enemyController.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
