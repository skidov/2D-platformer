using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Texture;

namespace Platformer.Character
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
        public CharacterState State { get; private set; }
        public CharacterDirection Direction { get; set; }

        public Huntress(Vector2 pos)
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

        internal override void ActionFall()
        {
            animation.NewSpriteSheet(spriteSheetFall);
            animation.AnimationTime = 0.15;
            animation.Repeat = false;
            State = CharacterState.FALL;
        }

        internal override void ActionJump()
        {
            animation.NewSpriteSheet(spriteSheetJump);
            animation.AnimationTime = 0.15;
            animation.Repeat = false;
            State = CharacterState.JUMP;
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

        internal override void ActionAttack1()
        {
            animation.NewSpriteSheet(spriteSheetAttack1);
            animation.AnimationTime = 0.15;
            animation.Repeat = false;
            State = CharacterState.ATTACK;
        }

        internal override void ActionAttack2()
        {
            animation.NewSpriteSheet(spriteSheetAttack2);
            animation.AnimationTime = 0.15;
            animation.Repeat = false;
            State = CharacterState.ATTACK;
        }
    }
}
