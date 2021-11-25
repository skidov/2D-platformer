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
    public abstract class Map
    {
        internal TiledMap _tiledMap;
        internal TiledMapRenderer _tiledMapRenderer;

        public abstract void LoadContent(ContentManager content, GraphicsDevice graphicsDevice);
        public abstract void DrawBackground(SpriteBatch _spritebatch);
        public abstract void DrawMap(GameTime gameTime, GameCamera camera);

        internal void LoadObjects(float scale)
        {
            CollisionBoxManager.Reset();

            foreach (var e in _tiledMap.ObjectLayers)
            {
                if (e.Name == "GameObjects")
                {
                    foreach (var o in e.Objects)
                    {
                        if (o.Name == "EndPoint")
                        {
                            CollisionBoxManager.AddGameEndCollisionBox(GenerateCollisionBox(o, scale));
                        }
                        else if (o.Name == "StartPoint")
                        {

                        }
                        else if (o.Name == "PatrolEnemy")
                        {

                        }
                    }
                }
            }
        }

        internal CollisionBox GenerateCollisionBox(TiledMapObject to, float scale)
        {
            return new CollisionBox((to.Position + (Vector2)to.Size / 2) * scale - new Vector2(200, 400), to.Size * scale / 2);
        }

        public void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);
        }
    }
}
