using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Map;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer.Scenes
{
    public class GameScene : Scene
    {
        MapManager mapManager;

        public GameScene(MapManager mapManager)
        {
            this.mapManager = mapManager;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            mapManager.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            mapManager.Update(gameTime);
        }
    }
}
