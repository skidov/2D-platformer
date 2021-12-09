using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Characters
{
    public abstract class PlayerCharacter : Character
    {
        abstract public void ActionDeath();

        abstract public void ActionJump();

        abstract public void ActionRun();

        abstract public void ActionTakeHit();

        abstract public void ActionAttack1();

        abstract public void ActionAttack2();
    }
}
