using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Collision;
using Platformer.Map;
using Platformer.Texture;

namespace Platformer.Characters
{
    public class Skeleton : EnemyCharacter
    {
        private const int COLLISION_BOX_OFF_SET_X = 20;
        private const int COLLISION_BOX_OFF_SET_Y = 36;
        private const int COLLISION_BOX_HALF_SIZE_X = 8;
        private const int COLLISION_BOX_HALF_SIZE_Y = 22;
        private const float WALK_SPEED = 60.0f;

        private const int ATTACK_COLLISION_BOX_OFF_SET_X = 0;
        private const int ATTACK_COLLISION_BOX_OFF_SET_Y = 36;
        private const int ATTACK_COLLISION_BOX_HALF_SIZE_X = 15;
        private const int ATTACK_COLLISION_BOX_HALF_SIZE_Y = 18;
        private const int ATTACK_COLLISION_BOX_RIGHT_DIRECTION_OFF_SET = 48;
        private const float ATTACK_TIME = 0.3f;
        private const int ATTACK_STRENGTH = 1;

        private float attackTime;

        private static SpriteSheet spriteSheetIdle, spriteSheetDeath, spriteSheetRun, spriteSheetTakeHit, spriteSheetAttack;

        public static void LoadContent(ContentManager content)
        {
            spriteSheetIdle = new SpriteSheet(content.Load<Texture2D>("Characters/Skeleton/Skeleton Idle"), 11, 1);
            spriteSheetDeath = new SpriteSheet(content.Load<Texture2D>("Characters/Skeleton/Skeleton Dead"), 15, 1);
            spriteSheetRun = new SpriteSheet(content.Load<Texture2D>("Characters/Skeleton/Skeleton Walk"), 13, 1);
            spriteSheetTakeHit = new SpriteSheet(content.Load<Texture2D>("Characters/Skeleton/Skeleton Hit"), 8, 1);
            spriteSheetAttack = new SpriteSheet(content.Load<Texture2D>("Characters/Skeleton/Skeleton Attack"), 18, 1);
        }

        public Skeleton(MapManager mapManager, Vector2 pos) : base(mapManager)
        {
            CharacterCollisionBoxOffSet = new Vector2(COLLISION_BOX_OFF_SET_X, COLLISION_BOX_OFF_SET_Y);
            CharacterCollisionBox = new CollisionBox(new Vector2(0, 0), new Vector2(COLLISION_BOX_HALF_SIZE_X, COLLISION_BOX_HALF_SIZE_Y));
            this.Position = pos - CharacterCollisionBoxOffSet - new Vector2(0, COLLISION_BOX_HALF_SIZE_Y);

            CollisionBoxManager.AddEnemyCollisionBox(CharacterCollisionBox, this);

            AttackCollisionBoxOffSet = new Vector2(ATTACK_COLLISION_BOX_OFF_SET_X, ATTACK_COLLISION_BOX_OFF_SET_Y);
            AttackCollisionBoxHalfSize = new Vector2(ATTACK_COLLISION_BOX_HALF_SIZE_X, ATTACK_COLLISION_BOX_HALF_SIZE_Y);
            AttackCollisionBoxRightOffSet = ATTACK_COLLISION_BOX_RIGHT_DIRECTION_OFF_SET;

            Direction = CharacterDirection.RIGHT;
            Health = 2;

            Animation = new Animation(spriteSheetIdle, true, 0.15);
            Animation.Effect = SpriteEffects.None;
            Animation.Scale = 1.8f;
            ActionIdle();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (State)
            {
                case CharacterState.ATTACK:
                    if (Animation.IsEnded)
                        ActionIdle();
                    else if (attackTime > 0)
                        attackTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else
                    {
                        var players = CollisionBoxManager.IntersectWithPlayer(GetAttackCollisionBox);
                        foreach (var e in players)
                        {
                            e.Hit(ATTACK_STRENGTH);
                        }
                    }
                    break;
                case CharacterState.DEATH:
                    break;
                case CharacterState.FALL:
                    break;
                case CharacterState.IDLE:
                    break;
                case CharacterState.JUMP:
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
            CollisionBoxManager.DrawCollisionBox(spriteBatch, GetAttackCollisionBox, Color.Black);
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

        public override void ActionRun()
        {
            Animation.NewSpriteSheet(spriteSheetRun);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = true;
            State = CharacterState.RUN;
        }

        public override void ActionTakeHit()
        {
            Speed = Vector2.Zero;

            Animation.NewSpriteSheet(spriteSheetTakeHit);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = false;
            State = CharacterState.TAKEHIT;
        }

        public override void ActionAttack()
        {
            Speed = Vector2.Zero;

            Animation.NewSpriteSheet(spriteSheetAttack);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = false;
            State = CharacterState.ATTACK;
            attackTime = ATTACK_TIME;
        }

        public override void ActionFall()
        {
            Animation.NewSpriteSheet(spriteSheetIdle);
            Animation.AnimationTime = 0.15;
            Animation.Repeat = true;
            State = CharacterState.FALL;
        }
    }
}
