using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Character
{
    abstract class EnemyCharacter : Character
    {
        abstract public void ActionIdle();

        abstract public void ActionDeath();

        abstract public void ActionRun();

        abstract public void ActionTakeHit();

        abstract public void ActionAttack();
    }
}
