using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;
using Platformer.Characters;
using Platformer.Collision;
using Platformer.Maps;
using Platformer.Scenes;
using Platformer.Traps;
using System.Collections.Generic;

namespace Platformer.Map
{
    public class MapManager
    {
        private const float GAME_OVER_TIME = 3.0f;

        private bool gameOver;
        private float gameOverTime;

        private GameScene gameScene;
        private Map map;
        private PlayerCharacterController playerController;
        private PlayerCharacter player;
        private PlayerChacterType playerType;
        private List<EnemyCharacterController> enemyControllers;
        private List<EnemyCharacter> diedEnemy;
        private List<Trap> traps;
        private OrthographicCamera _camera;
        private SpriteFont font;
        private ContentManager content;

        public MapManager(GameScene gameScene, MapType mapType, PlayerChacterType playerType, Game game, ContentManager content)
        {
            enemyControllers = new List<EnemyCharacterController>();
            diedEnemy = new List<EnemyCharacter>();
            traps = new List<Trap>();
            this.gameScene = gameScene;
            this.playerType = playerType;
            this.content = content;

            gameOver = false;
            gameOverTime = GAME_OVER_TIME;

            switch (mapType)
            {
                case MapType.BASICMAP:
                    this.map = new BasicMap(content, game.GraphicsDevice);
                    break;
                default:
                    this.map = new BasicMap(content, game.GraphicsDevice);
                    break;
            }

            var viewportAdapter = new BoxingViewportAdapter(game.Window, game.GraphicsDevice, 960, 540);
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
                            switch (playerType)
                            {
                                case PlayerChacterType.HUNTRESS:
                                    player = new Huntress(this, o.Position * scale);
                                    break;
                                default:
                                    player = new Huntress(this, o.Position * scale);
                                    break;
                            }
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
                            EnemyCharacter enemy = new Skeleton(this, o.Position * scale);
                            enemyControllers.Add(new EnemyCharacterController(enemy, o.Position.X * scale, o.Position.X * scale + o.Size.Width * scale));
                        }
                    }
                }
                else if (e.Name == "Traps")
                {
                    foreach (var o in e.Objects)
                    {
                        if (o.Name == "SpikesTrap")
                        {
                            SpikesTrap trap = new SpikesTrap(o.Position * scale);
                            traps.Add(trap);
                        }
                    }
                }
            }

            font = content.Load<SpriteFont>("Fonts/Font");
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
                e.Update(gameTime);

            foreach (var e in diedEnemy)
                e.Update(gameTime);

            foreach (var e in traps)
                e.Update(gameTime);

            map._TiledMapRenderer.Update(gameTime);

            if (CollisionBoxManager.IntersectWithGameEndBoxes(player.CharacterCollisionBox))
                gameScene.PlayerWin();

            if (gameOver)
            {
                if (gameOverTime > 0)
                    gameOverTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                else gameScene.PlayerDied();
            }

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
                e.Draw(gameTime, spriteBatch);

            foreach (var e in diedEnemy)
                e.Draw(gameTime, spriteBatch);

            foreach (var e in traps)
                e.Draw(gameTime, spriteBatch);

            //CollisionBoxManager.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Character Health: " + player.Health.ToString(), new Vector2(10, 10), Color.Black);
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

        public void CharacterDied(Character character)
        {
            if (player == character)
            {
                CollisionBoxManager.RemovePlayerCollisionBox(player.CharacterCollisionBox);
                gameOver = true;
            } 
            else 
            {
                CollisionBoxManager.RemoveEnemyCollisionBox(character.CharacterCollisionBox);
                enemyControllers.RemoveAll(x => x.Character == character);
                diedEnemy.Add((EnemyCharacter)character);
            }
        }
    }
}
