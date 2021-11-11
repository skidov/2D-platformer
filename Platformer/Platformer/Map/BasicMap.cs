using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace Platformer.Map
{
    public class BasicMap
    {
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Texture2D background1;
        private Texture2D background2;

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            background1 = content.Load<Texture2D>("Map/basicmap/tiles/Background_1");
            background2 = content.Load<Texture2D>("Map/basicmap/tiles/Background_2");
            _tiledMap = content.Load<TiledMap>("Map/basicmap/basicmap");
            _tiledMapRenderer = new TiledMapRenderer(graphicsDevice, _tiledMap);

        }

        public void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);
        }

        public void DrawBackground(SpriteBatch _spritebatch)
        {
            _spritebatch.Draw(background2, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.White);
            _spritebatch.Draw(background1, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.White);
        }

        public void DrawMap(GameTime gameTime)
        {
            _tiledMapRenderer.Draw();
        }
    }
}
