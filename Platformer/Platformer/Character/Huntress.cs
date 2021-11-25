using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Physics;
using Platformer.Texture;

namespace Platformer.Character
{
    public class Huntress : PlayerCharacter
    {
        private const int COLLISION_BOX_OFF_SET_X = 97;
        private const int COLLISION_BOX_OFF_SET_Y = 102;
        private const int COLLISION_BOX_HALF_SIZE_X = 11;
        private const int COLLISION_BOX_HALF_SIZE_Y = 23;
        private const float WALK_SPEED = 100.0f;
        private const float JUMP_SPEED = 100.0f;

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

        public Huntress(Vector2 pos) : base()
        {
            CharacterCollisionBoxOffSet = new Vector2(COLLISION_BOX_OFF_SET_X, COLLISION_BOX_OFF_SET_Y);
            CharacterCollisionBox = new CollisionBox(new Vector2(0, 0), new Vector2(COLLISION_BOX_HALF_SIZE_X, COLLISION_BOX_HALF_SIZE_Y));
            this.Position = pos - CharacterCollisionBoxOffSet - new Vector2(0, COLLISION_BOX_HALF_SIZE_Y);

            CollisionBoxManager.AddPlayerCollisionBox(CharacterCollisionBox);
            
            Direction = CharacterDirection.RIGHT;

            Animation = new Animation(spriteSheetIdle, true, 0.15);
            Animation.Effect = SpriteEffects.None;
            Animation.Scale = 1.3f;
            ActionIdle();
        }

        public override void Update(GameTime gameTime)
        {
            switch (State)
            {
                case CharacterState.ATTACK:
                    if (Animation.IsEnded)
                        ActionIdle();
                    break;
                case CharacterState.DEATH:
                    break;
                case CharacterState.FALL:
                    if (MoveWhileFall)
                        SetUpRunSeed(WALK_SPEED);
                    AddGravity(gameTime);
                    break;
                case CharacterState.IDLE:
                    break;
                case CharacterState.JUMP:
                    if (MoveWhileFall)
                        SetUpRunSeed(WALK_SPEED);
                    AddGravity(gameTime);
                    if (Speed.Y > 0)
                        ActionFall();
                    break;
                case CharacterState.RUN:              
                    SetUpRunSeed(WALK_SPEED);
                    break;
                case CharacterState.TAKEHIT:
                    break;
            }

            UpdatePhysics(gameTime);
            Animation.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Animation.Draw(spriteBatch, Position);
        }

        public override void ActionIdle()
        {
            Speed = Vector2.Zero;

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

        public override void ActionFall()
        {
            Animation.NewSpriteSheet(spriteSheetFall);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = false;
            State = CharacterState.FALL;
        }

        public override void ActionJump()
        {
            Vector2 speed = Speed;
            speed.Y = -JUMP_SPEED;
            Speed = speed;

            Animation.NewSpriteSheet(spriteSheetJump);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = false;
            State = CharacterState.JUMP;
        }

        public override void ActionRun()
        {
            Animation.NewSpriteSheet(spriteSheetRun);
            Animation.AnimationTime = 0.12;
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

        public override void ActionAttack1()
        {
            Animation.NewSpriteSheet(spriteSheetAttack1);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = false;
            State = CharacterState.ATTACK;
        }

        public override void ActionAttack2()
        {
            Animation.NewSpriteSheet(spriteSheetAttack2);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = false;
            State = CharacterState.ATTACK;
        }
    }
}
