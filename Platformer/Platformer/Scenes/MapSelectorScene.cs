using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Characters;
using Platformer.Components;
using Platformer.Maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer.Scenes
{
    class MapSelectorScene : Scene
    {
        private PlayerChacterType playerType;
        private SpriteFont font;
        Button buttonBasicMap;
        Button buttonMap2;
        Button buttonBackToMenu;

        public MapSelectorScene(Game1 game, ContentManager content, PlayerChacterType playerType) : base(game, content)
        {
            this.playerType = playerType;

            game.IsMouseVisible = true;
        }

        public override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Fonts/Font");

            float sizex = 230, sizey = 50;
            Vector2 pos = new Vector2(_Game.GraphicsDevice.Viewport.Width / 2 - sizex / 2, _Game.GraphicsDevice.Viewport.Height / 2 - 105);
            buttonBasicMap = new Button(font, "Basic Map", pos, new Vector2(sizex, sizey), new Color(153, 153, 153), new Color(128, 128, 128));
            buttonBasicMap.Click += ButtonBasicMap_Click;

            sizex = 230; sizey = 50;
            pos = new Vector2(_Game.GraphicsDevice.Viewport.Width / 2 - sizex / 2, _Game.GraphicsDevice.Viewport.Height / 2 - 35);
            buttonMap2 = new Button(font, "Map 2", pos, new Vector2(sizex, sizey), new Color(153, 153, 153), new Color(128, 128, 128));
            buttonMap2.Click += ButtonMap2_Click;

            sizex = 230; sizey = 50;
            pos = new Vector2(_Game.GraphicsDevice.Viewport.Width / 2 - sizex / 2, _Game.GraphicsDevice.Viewport.Height / 2 + 35);
            buttonBackToMenu = new Button(font, "Back to menu", pos, new Vector2(sizex, sizey), new Color(153, 153, 153), new Color(128, 128, 128));
            buttonBackToMenu.Click += ButtonBackToMenu_Click;
        }

        public override void Update(GameTime gameTime)
        {
            buttonBasicMap.Update(gameTime);
            buttonMap2.Update(gameTime);
            buttonBackToMenu.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            string text = "Choose a map!";

            float x = _Game.GraphicsDevice.Viewport.Width / 2 - font.MeasureString(text).X / 2;
            float y = _Game.GraphicsDevice.Viewport.Height / 2 - font.MeasureString(text).Y / 2 - 130;

            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.Black);
            buttonBasicMap.Draw(spriteBatch, gameTime);
            buttonMap2.Draw(spriteBatch, gameTime);
            buttonBackToMenu.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }

        private void ButtonBasicMap_Click(object sender, EventArgs e)
        {
            _Game.ChangeScene(new GameScene(_Game, Content, MapType.BASICMAP, PlayerChacterType.HUNTRESS));
        }

        private void ButtonMap2_Click(object sender, EventArgs e)
        {
            _Game.ChangeScene(new GameScene(_Game, Content, MapType.MAP2, PlayerChacterType.HUNTRESS));
        }

        private void ButtonBackToMenu_Click(object sender, EventArgs e)
        {
            _Game.ChangeScene(new MenuScene(_Game, Content));
        }
    }
}
