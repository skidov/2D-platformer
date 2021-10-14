using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Character
{
    abstract class PlayerCharacter : Character
    {
        abstract public override void Update(GameTime gameTime);

        abstract public override void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        abstract internal void ActionIdle();

        abstract internal void ActionDeath();

        abstract internal void ActionFall();

        abstract internal void ActionJump();

        abstract internal void ActionRun();

        abstract internal void ActionTakeHit();

        abstract internal void ActionAttack1();

        abstract internal void ActionAttack2();
    }
}
