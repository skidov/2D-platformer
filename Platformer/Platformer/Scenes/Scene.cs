using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Scenes
{
    public abstract class Scene
    {
        protected ContentManager Content { get; set; }
        protected Game1 _Game { get; set; }

        public Scene(Game1 game, ContentManager content)
        {
            _Game = game;
            Content = content;
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
