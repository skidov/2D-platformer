using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Map;
using Platformer.Scenes;

namespace Platformer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }

        Scene actualScene;
        MapManager mapManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.IsFullScreen = false;
            
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            SetUpMapmanager(new BasicMap(Content, GraphicsDevice));
            ChangeScene(GameState.GAME);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            actualScene.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            actualScene.Draw(_spriteBatch, gameTime);

            base.Draw(gameTime);
        }

        public void SetUpMapmanager(GameMap map)
        {
            mapManager = new MapManager(map, GraphicsDevice, Window);
        }

        public void ChangeScene(GameState scene)
        {
            if (scene == GameState.GAME)
            {
                actualScene = new GameScene(mapManager);
            }
        }
    }
}
