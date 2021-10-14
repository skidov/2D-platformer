using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Texture;

namespace Platformer.Character
{
    class Skeleton : EnemyCharacter
    {
        private static SpriteSheet spriteSheetIdle, spriteSheetDeath, spriteSheetRun, spriteSheetTakeHit, spriteSheetAttack;

        public static void LoadContent(ContentManager content)
        {
            spriteSheetIdle = new SpriteSheet(content.Load<Texture2D>("Characters/Skeleton/Skeleton Idle"), 11, 1);
            spriteSheetDeath = new SpriteSheet(content.Load<Texture2D>("Characters/Skeleton/Skeleton Dead"), 15, 1);
            spriteSheetRun = new SpriteSheet(content.Load<Texture2D>("Characters/Skeleton/Skeleton Walk"), 13, 1);
            spriteSheetTakeHit = new SpriteSheet(content.Load<Texture2D>("Characters/Skeleton/Skeleton Hit"), 8, 1);
            spriteSheetAttack = new SpriteSheet(content.Load<Texture2D>("Characters/Skeleton/Skeleton Attack"), 18, 1);
        }

        Animation animation;

        public Skeleton(Vector2 pos)
        {
            this.Position = pos;
            Direction = CharacterDirection.RIGHT;

            animation = new Animation(spriteSheetIdle, true, 0.15);
            ActionIdle();
        }

        public override void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch, Position);
        }

        internal override void ActionIdle()
        {
            animation.NewSpriteSheet(spriteSheetIdle);
            animation.AnimationTime = 0.15;
            animation.Repeat = true;
            State = CharacterState.IDLE;
        }

        internal override void ActionDeath()
        {
            animation.NewSpriteSheet(spriteSheetDeath);
            animation.AnimationTime = 0.15;
            animation.Repeat = false;
            State = CharacterState.DEATH;
        }

        internal override void ActionRun()
        {
            animation.NewSpriteSheet(spriteSheetRun);
            animation.AnimationTime = 0.15;
            animation.Repeat = true;
            State = CharacterState.RUN;
        }

        internal override void ActionTakeHit()
        {
            animation.NewSpriteSheet(spriteSheetTakeHit);
            animation.AnimationTime = 0.15;
            animation.Repeat = true;
            State = CharacterState.TAKEHIT;
        }

        internal override void ActionAttack()
        {
            animation.NewSpriteSheet(spriteSheetAttack);
            animation.AnimationTime = 0.15;
            animation.Repeat = false;
            State = CharacterState.ATTACK;
        }
    }
}
