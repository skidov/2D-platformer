﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Physics;
using Platformer.Texture;
using System;

namespace Platformer.Character
{
    public abstract class Character
    {
        public const int MAX_FALLING_SPEED = 100;
        public const int GRAVITY = 100;

        private CharacterDirection direction;
        internal Animation Animation { get; set; }
        public CharacterState State { get; internal set; }

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

        public abstract void Update(GameTime gameTime);
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
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            float posY = CharacterCollisionBox.Center.Y + CharacterCollisionBox.HalfSize.Y;
            if (posY > 0.0f)
            {
                Vector2 pos = Position;
                pos.Y -= posY;
                Position = pos;
                ActionIdle();
            }
        }

        abstract public void ActionIdle();
    }
}
