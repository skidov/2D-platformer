using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer.Scenes
{
    class MapSelectorScene : Scene
    {
        private SpriteFont font;

        public MapSelectorScene(Game1 game, ContentManager content) : base(game, content)
        {
            game.IsMouseVisible = true;
        }

        public override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Fonts/Font");
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

    }
}
