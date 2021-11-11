using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Physics;
using Platformer.Texture;

namespace Platformer.Character
{
    public abstract class Character
    {
        private CharacterDirection direction;
        internal Animation Animation { get; set; }
        public CharacterState State { get; internal set; }

        public CollisionBox CharacterCollisionBox { get; set; }
        internal Vector2 CharacterCollisionBoxOffSet { get; set; }
        public float WalkSpeed { get; set; }
        public float JumpSpeed { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public Vector2 Mass { get; set; }

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

        public void SetUpRunSeed()
        {
            Vector2 speed = Speed;
            if (Direction == CharacterDirection.LEFT)
                speed.X = -WalkSpeed;
            else
                speed.X = WalkSpeed;
            Speed = speed;
        }

        public void UpdatePhysics(GameTime gameTime)
        {
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 pos = Position;
            if (pos.Y < 0.0f)
                pos.Y = 0.0f;
            Position = pos;

            CharacterCollisionBox.Center = Position + CharacterCollisionBoxOffSet;
        }
    }
}
