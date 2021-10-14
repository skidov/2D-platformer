using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Platformer.Texture
{
    class Animation
    {
        private double elapsedTime = 0;
        private double lastSwitch = 0;

        private SpriteSheet spriteSheet;
        private int spritePositionX = 0;
        private int spritePositionY = 0;

        public int SpritePositionX
        {
            get { return spritePositionX; }
            set { if (value < spriteSheet.SpriteXCount || value < 0) spritePositionX = value; else throw new ArgumentException("Nem megfelelő érték!"); }
        }

        public int SpritePositionY
        {
            get { return spritePositionY; }
            set { if (value < spriteSheet.SpriteYCount || value < 0) spritePositionY = value; else throw new ArgumentException("Nem megfelelő érték!"); }
        }

        public bool Repeat { get; set; }
        public double AnimationTime { get; set; }
        public bool IsEnded { get; private set; }
        public SpriteEffects Effect { get; set; }
        public float Scale { get; set; }

        public Animation(SpriteSheet spriteSheet, bool repeat, double animationTime)
        {
            this.spriteSheet = spriteSheet;
            Repeat = repeat;
            AnimationTime = animationTime;
            IsEnded = false;
            Effect = SpriteEffects.None;
            Scale = 1.0f;
        }

        /**
         * Újraindítja az animációt.
         * 0-ra állítja a spritesheet x koordinátáját
         */
        public void ResetAnimation()
        {
            SpritePositionX = 0;
            elapsedTime = 0;
            lastSwitch = 0;
            IsEnded = false;
        }

        public void NewSpriteSheet(SpriteSheet spriteSheet)
        {
            this.spriteSheet = spriteSheet;
            SpritePositionX = 0;
            elapsedTime = 0;
            lastSwitch = 0;
            IsEnded = false;
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            while (lastSwitch + AnimationTime < elapsedTime)
            {
                lastSwitch += AnimationTime;
                int nextPositionX = SpritePositionX + 1;
                if (nextPositionX < spriteSheet.SpriteXCount)
                    SpritePositionX = nextPositionX;
                else if (Repeat)
                    SpritePositionX = 0;
                else
                    IsEnded = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteSheet.Draw(spriteBatch, position, SpritePositionX, SpritePositionY, Scale, Effect);
        }
    }
}
