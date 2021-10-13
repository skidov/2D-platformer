using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Objects.Characters
{
    class Huntress : PlayerCharacter
    {
        private static SpriteSheet spriteSheetIdle, spriteSheetDeath, spriteSheetFall, spriteSheetJump, spriteSheetRun, spriteSheetTakeHit, spriteSheetAttack1, spriteSheetAttack2;

        public static void LoadContent(ContentManager content)
        {
            spriteSheetIdle = new SpriteSheet(content.Load<Texture2D>("Characters/Huntress/Idle"), 8, 1);
            spriteSheetDeath = new SpriteSheet(content.Load<Texture2D>("Characters/Huntress/Death"), 8, 1);
            spriteSheetFall = new SpriteSheet(content.Load<Texture2D>("Characters/Huntress/Fall"), 2, 1);
            spriteSheetJump = new SpriteSheet(content.Load<Texture2D>("Characters/Huntress/Jump"), 2, 1);
            spriteSheetRun = new SpriteSheet(content.Load<Texture2D>("Characters/Huntress/Run"), 8, 1);
            spriteSheetTakeHit = new SpriteSheet(content.Load<Texture2D>("Characters/Huntress/Take hit"), 3, 1);
            spriteSheetAttack1 = new SpriteSheet(content.Load<Texture2D>("Characters/Huntress/Attack1"), 5, 1);
            spriteSheetAttack2 = new SpriteSheet(content.Load<Texture2D>("Characters/Huntress/Attack2"), 5, 1);
        }

        Animation animation;

        public Huntress(Vector2 pos)
        {
            this.Position = pos;

            animation = new Animation(spriteSheetIdle, true, 0.15);
            ActionIdle();
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animation.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch, Position);
        }

        private void ActionIdle()
        {
            animation.NewSpriteSheet(spriteSheetIdle);
            animation.AnimationTime = 0.15;
            animation.Repeat = true;
        }

        private void ActionDeath()
        {
            animation.NewSpriteSheet(spriteSheetDeath);
            animation.AnimationTime = 0.15;
            animation.Repeat = false;
        }

        private void ActionFall()
        {
            animation.NewSpriteSheet(spriteSheetFall);
            animation.AnimationTime = 0.15;
            animation.Repeat = false;
        }

        private void ActionJump()
        {
            animation.NewSpriteSheet(spriteSheetJump);
            animation.AnimationTime = 0.15;
            animation.Repeat = false;
        }

        private void ActionRun()
        {
            animation.NewSpriteSheet(spriteSheetRun);
            animation.AnimationTime = 0.15;
            animation.Repeat = true;
        }

        private void ActionTakeHit()
        {
            animation.NewSpriteSheet(spriteSheetTakeHit);
            animation.AnimationTime = 0.15;
            animation.Repeat = true;
        }

        private void ActionAttack1()
        {
            animation.NewSpriteSheet(spriteSheetAttack1);
            animation.AnimationTime = 0.15;
            animation.Repeat = false;
        }

        private void ActionAttack2()
        {
            animation.NewSpriteSheet(spriteSheetAttack2);
            animation.AnimationTime = 0.15;
            animation.Repeat = false;
        }
    }
}
