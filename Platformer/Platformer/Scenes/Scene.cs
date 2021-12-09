using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Scenes
{
    public abstract class Scene
    {
        protected ContentManager Content { get; set; }

        public Scene(ContentManager content)
        {
            Content = content;
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
