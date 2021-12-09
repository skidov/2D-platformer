using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Characters;
using Platformer.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer.Scenes
{
    class CharacterSelectorScene : Scene
    {
        private SpriteFont font;
        Button buttonHuntress;
        Button buttonBackToMenu;

        public CharacterSelectorScene(Game1 game, ContentManager content) : base(game, content)
        {
            game.IsMouseVisible = true;
        }

        public override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Fonts/Font");

            float sizex = 230, sizey = 50;
            Vector2 pos = new Vector2(_Game.GraphicsDevice.Viewport.Width / 2 - sizex / 2, _Game.GraphicsDevice.Viewport.Height / 2 - 35);
            buttonHuntress = new Button(font, "Huntress", pos, new Vector2(sizex, sizey), new Color(153, 153, 153), new Color(128, 128, 128));
            buttonHuntress.Click += ButtonHuntress_Click;

            sizex = 230; sizey = 50;
            pos = new Vector2(_Game.GraphicsDevice.Viewport.Width / 2 - sizex / 2, _Game.GraphicsDevice.Viewport.Height / 2 + 35);
            buttonBackToMenu = new Button(font, "Back to menu", pos, new Vector2(sizex, sizey), new Color(153, 153, 153), new Color(128, 128, 128));
            buttonBackToMenu.Click += ButtonBackToMenu_Click;
        }

        public override void Update(GameTime gameTime)
        {
            buttonHuntress.Update(gameTime);
            buttonBackToMenu.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            string text = "Choose a character!";

            float x = _Game.GraphicsDevice.Viewport.Width / 2 - font.MeasureString(text).X / 2;
            float y = _Game.GraphicsDevice.Viewport.Height / 2 - font.MeasureString(text).Y / 2 - 60;

            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.Black);
            buttonHuntress.Draw(spriteBatch, gameTime);
            buttonBackToMenu.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }

        private void ButtonHuntress_Click(object sender, EventArgs e)
        {
            _Game.ChangeScene(new MapSelectorScene(_Game, Content, PlayerChacterType.HUNTRESS));
        }

        private void ButtonBackToMenu_Click(object sender, EventArgs e)
        {
            _Game.ChangeScene(new MenuScene(_Game, Content));
        }
    }
}
