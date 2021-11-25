using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Camera;
using Platformer.Character;
using Platformer.Map;
using Platformer.Physics;

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
        EnemyCharacter skeleton;
        BasicMap basicMap;

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
            skeleton = new Skeleton(new Vector2(50, 0));
            enemyController = new EnemyCharacterController(skeleton, 50, 300);

            basicMap = new BasicMap();
            basicMap.LoadContent(Content, GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            basicMap.Update(gameTime);

            playerController.Update(gameTime);
            enemyController.Update(gameTime);

            camera.Follow(huntress.CharacterCollisionBox.Center);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            basicMap.DrawBackground(_spriteBatch);
            _spriteBatch.End();

            basicMap.DrawMap(gameTime, camera);

            _spriteBatch.Begin(transformMatrix: camera.Transform);

            playerController.Draw(gameTime, _spriteBatch);
            enemyController.Draw(gameTime, _spriteBatch);

            CollisionBoxManager.Draw(_spriteBatch);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
