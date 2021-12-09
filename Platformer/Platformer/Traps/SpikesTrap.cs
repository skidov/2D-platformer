using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Collision;
using Platformer.Texture;

namespace Platformer.Traps
{
    class SpikesTrap : Trap
    {
        private const int COLLISION_BOX_OFF_SET_X = 41;
        private const int COLLISION_BOX_OFF_SET_Y = 80;
        private const int COLLISION_BOX_HALF_SIZE_X = 2;
        private const int COLLISION_BOX_HALF_SIZE_Y = 4;

        private const int TRAP_STRENGTH = 2;

        private static SpriteSheet sheet;

        public static void LoadContent(ContentManager content)
        {
            sheet = new SpriteSheet(content.Load<Texture2D>("Traps/16x16 traps"), 12, 15);
        }

        public SpikesTrap(Vector2 pos)
        {
            TrapAttackCollisionBoxOffSet = new Vector2(COLLISION_BOX_OFF_SET_X, COLLISION_BOX_OFF_SET_Y);
            TrapAttackCollisionBox = new CollisionBox(new Vector2(0, 0), new Vector2(COLLISION_BOX_HALF_SIZE_X, COLLISION_BOX_HALF_SIZE_Y));
            this.Position = pos - TrapAttackCollisionBoxOffSet - new Vector2(0, COLLISION_BOX_HALF_SIZE_Y);

            Animation = new Animation(sheet, false, 0.15);
            Animation.Effect = SpriteEffects.None;
            Animation.Scale = 1.3f;
            Animation.SpritePositionY = 14;
            Animation.Stop = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (Animation.Stop)
            {
                var players = CollisionBoxManager.IntersectWithPlayer(TrapAttackCollisionBox);
                if (players.Count > 0)
                {
                    Animation.Stop = false;
                    foreach (var e in players)
                    {
                        e.Hit(TRAP_STRENGTH);
                    }
                }
            }
            else if (Animation.IsEnded)
            {
                Animation.ResetAnimation();
                Animation.Stop = true;
            }
            Animation.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Animation.Draw(spriteBatch, Position);
            CollisionBoxManager.DrawCollisionBox(spriteBatch, TrapAttackCollisionBox, Color.Black);
        }
    }
}
