using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Platformer.Camera;
using Platformer.Character;
using Platformer.Physics;

namespace Platformer.Map
{
    public abstract class Map
    {
        internal TiledMap _tiledMap;
        internal TiledMapRenderer _tiledMapRenderer;
        internal PlayerCharacterController playerController;
        internal PlayerCharacter player;
        internal EnemyCharacterController enemyController;
        internal GameCamera camera;

        public abstract void LoadContent(ContentManager content, GraphicsDevice graphicsDevice);
        public abstract void DrawBackground(SpriteBatch _spritebatch);
        public abstract void DrawMap(GameTime gameTime, GameCamera camera);

        public Map()
        {
            camera = new GameCamera();
        }

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
                            player = new Huntress(o.Position);
                            playerController = new PlayerCharacterController(player);
                        }
                    }
                }
                else if (e.Name == "Collisions")
                {
                    foreach (var o in e.Objects)
                    {
                        CollisionBoxManager.AddMapCollisionBox(GenerateCollisionBox(o, scale));
                    }
                }
                else if (e.Name == "PatrolEnemy")
                {

                }
            }
        }

        internal CollisionBox GenerateCollisionBox(TiledMapObject to, float scale)
        {
            return new CollisionBox((to.Position + (Vector2)to.Size / 2) * scale - new Vector2(200, 400), to.Size * scale / 2);
        }

        public void Update(GameTime gameTime)
        {
            playerController.Update(gameTime);
            camera.Follow(player.CharacterCollisionBox.Center);

            _tiledMapRenderer.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            DrawBackground(spriteBatch);
            spriteBatch.End();

            DrawMap(gameTime, camera);

            spriteBatch.Begin(transformMatrix: camera.Transform);

            playerController.Draw(gameTime, spriteBatch);

            CollisionBoxManager.Draw(spriteBatch);


            spriteBatch.End();

        }
    }
}
