using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;
using Platformer.Character;
using Platformer.Collision;
using System.Collections.Generic;

namespace Platformer.Map
{
    public class MapManager
    {
        internal GameMap map;
        internal PlayerCharacterController playerController;
        internal PlayerCharacter player;
        internal List<EnemyCharacterController> enemyControllers;
        internal OrthographicCamera _camera;

        public MapManager(GameMap map, GraphicsDevice graphicsDevice, GameWindow window)
        {
            enemyControllers = new List<EnemyCharacterController>();
            this.map = map;

            var viewportAdapter = new BoxingViewportAdapter(window, graphicsDevice, 960, 540);
            _camera = new OrthographicCamera(viewportAdapter);

            LoadObjects();
        }

        internal void LoadObjects()
        {
            CollisionBoxManager.Reset();

            float scale = map.Scale;

            foreach (var e in map._TiledMap.ObjectLayers)
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
                            player = new Huntress(o.Position * scale);
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
                    foreach (var o in e.Objects)
                    {
                        if (o.Name == "Skeleton")
                        {
                            EnemyCharacter enemy = new Skeleton(o.Position * scale);
                            enemyControllers.Add(new EnemyCharacterController(enemy, o.Position.X * scale, o.Position.X * scale + o.Size.Width * scale));
                        }
                    }
                }
            }
        }

        internal CollisionBox GenerateCollisionBox(TiledMapObject to, float scale)
        {
            return new CollisionBox((to.Position + (Vector2)to.Size / 2) * scale, to.Size * scale / 2);
        }

        public void Update(GameTime gameTime)
        {
            playerController.Update(gameTime);

            _camera.LookAt(player.CharacterCollisionBox.Center);

            foreach (var e in enemyControllers)
            {
                e.Update(gameTime);
            }

            map._TiledMapRenderer.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            DrawBackground(spriteBatch);
            spriteBatch.End();

            DrawMap(gameTime);

            spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());

            playerController.Draw(gameTime, spriteBatch);

            foreach (var e in enemyControllers)
            {
                e.Draw(gameTime, spriteBatch);
            }

            CollisionBoxManager.Draw(spriteBatch);


            spriteBatch.End();
        }

        private void DrawBackground(SpriteBatch _spritebatch)
        {
            foreach (var e in map.BackGround)
            {
                _spritebatch.Draw(e, new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height), Color.White);
            }
        }

        private void DrawMap(GameTime gameTime)
        {
            foreach (TiledMapLayer layer in map._TiledMap.Layers)
            {
                Matrix scaleMatrix = Matrix.CreateScale(map.Scale);
                if (layer.Name == "Map")
                {
                    map._TiledMapRenderer.Draw(layer, viewMatrix: scaleMatrix * _camera.GetViewMatrix());
                }
            }
        }
    }
}
