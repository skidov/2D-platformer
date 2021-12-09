using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Collision;
using Platformer.Map;
using Platformer.Texture;
using System;
using System.Collections.Generic;

namespace Platformer.Characters
{
    public abstract class Character
    {
        public const int MAX_FALLING_SPEED = 100;
        public const int GRAVITY = 100;

        private const float HIT_TIME = 1.8f;

        internal float HitTime { get; set; }

        private MapManager MapManager { get; set; }

        private CharacterDirection direction;
        internal Animation Animation { get; set; }
        public CharacterState State { get; internal set; }

        public int Health { get; internal set; }

        public CollisionBox CharacterCollisionBox { get; set; }
        internal Vector2 CharacterCollisionBoxOffSet { get; set; }
        public Vector2 Speed { get; set; }
        public Vector2 Mass { get; set; }
        public bool MoveWhileFall { get; set; }

        private Vector2 position;
        public Vector2 Position 
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                CharacterCollisionBox.Center = value + CharacterCollisionBoxOffSet;
            }
        }

        public CharacterDirection Direction 
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
                if (Animation != null)
                {
                    if (value == CharacterDirection.LEFT)
                        Animation.Effect = SpriteEffects.FlipHorizontally;
                    else
                        Animation.Effect = SpriteEffects.None;
                }
            }
        }

        public Character(MapManager mapManager)
        {
            MapManager = mapManager;
            HitTime = 0;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (HitTime > 0)
                HitTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public void SetUpRunSeed(float walkSpeed)
        {
            Vector2 speed = Speed;
            if (Direction == CharacterDirection.LEFT)
                speed.X = -walkSpeed;
            else
                speed.X = walkSpeed;
            Speed = speed;
        }

        public void AddGravity(GameTime gameTime)
        {
            Vector2 speed = Speed;
            speed.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * GRAVITY;
            speed.Y = Math.Min(speed.Y, MAX_FALLING_SPEED);
            Speed = speed;
        }

        public void UpdatePhysics(GameTime gameTime)
        {
            AddGravity(gameTime);
            Vector2 addPos = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;


            List<CollisionBox> collidedBoxes;

            if (addPos.X != 0)
            {
                Position += new Vector2(addPos.X, 0);
                collidedBoxes = CollisionBoxManager.IntersectWithMap(
                    new CollisionBox(CharacterCollisionBox.Center, CharacterCollisionBox.HalfSize - Vector2.UnitY * 2));

                foreach (var e in collidedBoxes)
                {
                    if (addPos.X > 0)
                        Position = new Vector2(e.Center.X - e.HalfSize.X - CharacterCollisionBox.HalfSize.X - CharacterCollisionBoxOffSet.X, Position.Y);
                    else
                        Position = new Vector2(e.Center.X + e.HalfSize.X + CharacterCollisionBox.HalfSize.X - CharacterCollisionBoxOffSet.X, Position.Y);
                }
            }

            Position += new Vector2(0, addPos.Y);
            collidedBoxes = CollisionBoxManager.IntersectWithMap(
                    new CollisionBox(CharacterCollisionBox.Center, CharacterCollisionBox.HalfSize - Vector2.UnitX * 2));
            if (addPos.Y != 0)
            {
                foreach (var e in collidedBoxes)
                {
                    if (addPos.Y > 0)
                    {
                        Position = new Vector2(Position.X, e.Center.Y - e.HalfSize.Y - CharacterCollisionBox.HalfSize.Y - CharacterCollisionBoxOffSet.Y);
                        if (State == CharacterState.FALL)
                        {
                            ActionIdle();
                        }
                    }
                    else
                    {
                        Position = new Vector2(Position.X, e.Center.Y + e.HalfSize.Y + CharacterCollisionBox.HalfSize.Y - CharacterCollisionBoxOffSet.Y);
                        if (State == CharacterState.JUMP)
                        {
                            ActionFall();
                            Speed = new Vector2(Speed.X, 0);
                        }

                    }
                }
            }

            if (State != CharacterState.JUMP && State != CharacterState.FALL && collidedBoxes.Count == 0)
                ActionFall();

            if (State == CharacterState.JUMP && Speed.Y > 0)
                ActionFall();
        }

        public void Hit(int damage)
        {
            if (HitTime <= 0)
            {
                Health -= damage;
                if (Health < 0)
                    Health = 0;
                if (Health == 0)
                {
                    MapManager.CharacterDied(this);
                    ActionDeath();
                }
                else
                {
                    HitTime = HIT_TIME;
                    ActionTakeHit();
                }
            }
        }

        abstract public void ActionDeath();

        abstract public void ActionIdle();

        abstract public void ActionFall();

        abstract public void ActionTakeHit();
    }
}
