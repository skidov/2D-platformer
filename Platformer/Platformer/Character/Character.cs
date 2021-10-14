using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Texture;

namespace Platformer.Character
{
    abstract class Character
    {
        private CharacterDirection direction;
        internal Animation Animation { get; set; }
        public Vector2 Position { get; set; }
        public CharacterState State { get; internal set; }
        public float Speed { get; set; }

        public CharacterDirection Direction 
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
                if (Animation != null)
                {
                    if (value == CharacterDirection.LEFT)
                        Animation.Effect = SpriteEffects.FlipHorizontally;
                    else
                        Animation.Effect = SpriteEffects.None;
                }
            }
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
