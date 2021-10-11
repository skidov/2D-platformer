using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Platformer.Objects
{
    class SpriteSheet
    {
        private Texture2D texture;
        private int spritePositionX = 0;
        private int spritePositionY = 0;

        public int SpriteXCount { get; private set; }
        public int SpriteYCount { get; private set; }

        public int SpritePositionX
        {
            get { return spritePositionX; }
            set { if (value < SpriteXCount || value < 0) spritePositionX = value; else throw new ArgumentException("Nem megfelelő érték!"); }
        }

        public int SpritePositionY
        {
            get { return spritePositionY; }
            set { if (value < SpriteYCount || value < 0) spritePositionY = value; else throw new ArgumentException("Nem megfelelő érték!"); }
        }
        
        public SpriteSheet(Texture2D texture, int spriteXCount, int spriteYCount)
        {
            this.texture = texture;
            this.SpriteXCount = spriteXCount;
            this.SpriteYCount = spriteYCount;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int spriteWidth = texture.Width / SpriteXCount;
            int spriteHeight = texture.Height / SpriteYCount;
            Rectangle sourceRectangle = new Rectangle(spriteWidth * SpritePositionX, spriteHeight * SpritePositionY, spriteWidth, spriteHeight);
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White);
        }
    }
}
