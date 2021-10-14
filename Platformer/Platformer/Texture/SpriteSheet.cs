using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Texture
{
    class SpriteSheet
    {
        private Texture2D texture;

        public int SpriteXCount { get; private set; }
        public int SpriteYCount { get; private set; }

        public SpriteSheet(Texture2D texture, int spriteXCount, int spriteYCount)
        {
            this.texture = texture;
            this.SpriteXCount = spriteXCount;
            this.SpriteYCount = spriteYCount;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, int posX, int posY, float scale, SpriteEffects effect)
        {
            int spriteWidth = texture.Width / SpriteXCount;
            int spriteHeight = texture.Height / SpriteYCount;
            Rectangle sourceRectangle = new Rectangle(spriteWidth * posX, spriteHeight * posY, spriteWidth, spriteHeight);
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0, new Vector2(0, 0), scale, effect, 1.0f);
        }
    }
}
