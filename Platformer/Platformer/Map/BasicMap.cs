using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Platformer.Camera;
using Platformer.Physics;

namespace Platformer.Map
{
    public class BasicMap : Map
    {
        private const float SCALE = 1.8f;
        
        private Texture2D background1;
        private Texture2D background2;

        public override void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            background1 = content.Load<Texture2D>("Map/basicmap/tiles/Background_1");
            background2 = content.Load<Texture2D>("Map/basicmap/tiles/Background_2");
            _tiledMap = content.Load<TiledMap>("Map/basicmap/basicmap");
            _tiledMapRenderer = new TiledMapRenderer(graphicsDevice, _tiledMap);

            LoadObjects(SCALE);
        }

        public override void DrawBackground(SpriteBatch _spritebatch)
        {
            _spritebatch.Draw(background2, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.White);
            _spritebatch.Draw(background1, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.White);
        }

        public override void DrawMap(GameTime gameTime, GameCamera camera)
        {
            foreach (TiledMapLayer layer in _tiledMap.Layers)
            {
                Matrix scaleMatrix = Matrix.CreateScale(SCALE);
                if (layer.Name == "Map")
                {
                    
                    _tiledMapRenderer.Draw(layer, viewMatrix: scaleMatrix * camera.Transform);
                }
            }
        }
    }
}
