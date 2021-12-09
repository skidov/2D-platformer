using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Collision;
using Platformer.Texture;

namespace Platformer.Traps
{
    public abstract class Trap
    {
        private Vector2 position;

        internal Animation Animation { get; set; }
        internal CollisionBox TrapAttackCollisionBox { get; set; }
        internal Vector2 TrapAttackCollisionBoxOffSet { get; set; }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                TrapAttackCollisionBox.Center = value + TrapAttackCollisionBoxOffSet;
            }
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
