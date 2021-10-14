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

        public Skeleton(Vector2 pos)
        {
            Speed = 60.0f;
            this.Position = pos;
            Direction = CharacterDirection.RIGHT;

            Animation = new Animation(spriteSheetIdle, true, 0.15);
            Animation.Effect = SpriteEffects.None;
            Animation.Scale = 1.8f;
            ActionIdle();
        }

        public override void Update(GameTime gameTime)
        {
            if (State == CharacterState.RUN)
            {
                CalculateNewPosition(gameTime);
            }
            Animation.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Animation.Draw(spriteBatch, Position);
        }

        public override void ActionIdle()
        {
            Animation.NewSpriteSheet(spriteSheetIdle);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = true;
            State = CharacterState.IDLE;
        }

        public override void ActionDeath()
        {
            Animation.NewSpriteSheet(spriteSheetDeath);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = false;
            State = CharacterState.DEATH;
        }

        public override void ActionRun()
        {
            Animation.NewSpriteSheet(spriteSheetRun);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = true;
            State = CharacterState.RUN;
        }

        public override void ActionTakeHit()
        {
            Animation.NewSpriteSheet(spriteSheetTakeHit);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = true;
            State = CharacterState.TAKEHIT;
        }

        public override void ActionAttack()
        {
            Animation.NewSpriteSheet(spriteSheetAttack);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = false;
            State = CharacterState.ATTACK;
        }
    }
}
