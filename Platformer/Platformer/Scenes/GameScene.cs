using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Characters;
using Platformer.Map;
using Platformer.Maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer.Scenes
{
    public class GameScene : Scene
    {
        MapManager mapManager;
        MapType mapType;
        PlayerChacterType playerType;

        public GameScene(Game1 game, ContentManager content, MapType mapType, PlayerChacterType playerType) : base(game, content)
        {
            this._Game = game;
            this.mapType = mapType;
            this.playerType = playerType;

            game.IsMouseVisible = false;
        }

        public override void LoadContent()
        {
            mapManager = new MapManager(this, mapType, playerType, _Game, Content);
        }

        public override void Update(GameTime gameTime)
        {
            mapManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            mapManager.Draw(spriteBatch, gameTime);
        }

        public void PlayerDied()
        {
            _Game.ChangeScene(new GameOverScene(_Game, Content, false, mapManager.PlayerCoins));
        }

        public void PlayerWin()
        {
            _Game.ChangeScene(new GameOverScene(_Game, Content, true, mapManager.PlayerCoins));
        }
    }
}
