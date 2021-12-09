using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer.Scenes
{
    public class MenuScene : Scene
    {
        private SpriteFont font;
        Button buttonNewGame;
        Button buttonExit;

        public MenuScene(Game1 game, ContentManager content) : base(game, content)
        {
            game.IsMouseVisible = true;
        }

        public override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Fonts/Font");

            float sizex = 230, sizey = 50;
            Vector2 pos = new Vector2(_Game.GraphicsDevice.Viewport.Width / 2 - sizex / 2, _Game.GraphicsDevice.Viewport.Height / 2 - 35);
            buttonNewGame = new Button(font, "New Game", pos, new Vector2(sizex, sizey), new Color(153, 153, 153), new Color(128, 128, 128));
            buttonNewGame.Click += ButtonNewGame_Click;

            sizex = 230; sizey = 50;
            pos = new Vector2(_Game.GraphicsDevice.Viewport.Width / 2 - sizex / 2, _Game.GraphicsDevice.Viewport.Height / 2 + 35);
            buttonExit = new Button(font, "Exit", pos, new Vector2(sizex, sizey), new Color(153, 153, 153), new Color(128, 128, 128));
            buttonExit.Click += ButtonExit_Click;
        }

        public override void Update(GameTime gameTime)
        {
            buttonNewGame.Update(gameTime);
            buttonExit.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            buttonNewGame.Draw(spriteBatch, gameTime);
            buttonExit.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            _Game.Exit();
        }

        private void ButtonNewGame_Click(object sender, EventArgs e)
        {
            _Game.ChangeScene(new CharacterSelectorScene(_Game, Content));
        }
    }
}
