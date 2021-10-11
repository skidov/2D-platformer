using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Objects
{
    class Animation
    {
        private double elapsedTime = 0;
        private double lastSwitch = 0;
        private SpriteSheet spriteSheet;
        
        public bool Repeat { get; set; }
        public double AnimationTime { get; set; }

        public Animation(SpriteSheet spriteSheet, bool repeat, double animationTime)
        {
            this.spriteSheet = spriteSheet;
            this.Repeat = repeat;
            this.AnimationTime = animationTime;
        }

        /**
         * Újraindítja az animációt.
         * 0-ra állítja a spritesheet x koordinátáját
         */
        public void ResetAnimation()
        {
            spriteSheet.SpritePositionX = 0;
            elapsedTime = 0;
            lastSwitch = 0;
        }

        public void NewSpriteSheet(SpriteSheet spriteSheet)
        {
            this.spriteSheet = spriteSheet;
            elapsedTime = 0;
            lastSwitch = 0;
        }

        public void Update(GameTime gameTime)
        {

            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            while (lastSwitch + AnimationTime < elapsedTime)
            {
                lastSwitch += AnimationTime;
                int nextPositionX = spriteSheet.SpritePositionX + 1;
                if (nextPositionX != spriteSheet.SpriteXCount)
                    spriteSheet.SpritePositionX = nextPositionX;
                else if (Repeat)
                    spriteSheet.SpritePositionX = 0;
            }
        }
    }
}
