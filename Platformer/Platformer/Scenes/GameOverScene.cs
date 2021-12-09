using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer.Scenes
{
    public class GameOverScene : Scene
    {
        private SpriteFont font;
        bool win;
        Button buttonBack;

        public GameOverScene(Game1 game, ContentManager content, bool win) : base(game, content)
        {
            this.win = win;

            game.IsMouseVisible = true;
        }

        public override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Fonts/FontBig");

            float sizex = 230, sizey = 50;
            Vector2 pos = new Vector2(_Game.GraphicsDevice.Viewport.Width / 2 - sizex / 2, _Game.GraphicsDevice.Viewport.Height / 2);
            buttonBack = new Button(font, "Bact to menu", pos, new Vector2(sizex, sizey), new Color(153, 153, 153), new Color(128, 128, 128));
            buttonBack.Click += ButtonBack_Click;
        }

        public override void Update(GameTime gameTime)
        {
            buttonBack.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            string text;
            if (win)
                text = "Congratulation! You Win!";
            else
                text = "Game Over! You Died!";

            float x = _Game.GraphicsDevice.Viewport.Width / 2 - font.MeasureString(text).X / 2;
            float y = _Game.GraphicsDevice.Viewport.Height / 2 - font.MeasureString(text).Y / 2 - 25;

            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.Black);
            buttonBack.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            _Game.ChangeScene(new MenuScene(_Game, Content));
        }
    }
}
