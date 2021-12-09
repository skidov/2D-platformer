using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer.Scenes
{
    public class GameOverScene : Scene
    {
        private SpriteFont font;
        bool win;

        public GameOverScene(Game1 game, ContentManager content, bool win) : base(game, content)
        {
            this.win = win;

            game.IsMouseVisible = true;
        }

        public override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Fonts/FontBig");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            if (win)
                spriteBatch.DrawString(font, "Congratulation! You Win!", new Vector2(10, 10), Color.Black);
            else
                spriteBatch.DrawString(font, "Game Over! You Died!", new Vector2(10, 10), Color.Black);
            spriteBatch.End();
        }

    }
}
