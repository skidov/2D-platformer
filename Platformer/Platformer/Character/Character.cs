using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Character
{
    abstract class Character
    {
        public Vector2 Position { get; set; }
        public CharacterState State { get; internal set; }
        public CharacterDirection Direction { get; set; }
        public float Speed { get; set; }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
