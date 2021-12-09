using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Map;

namespace Platformer.Characters
{
    public abstract class EnemyCharacter : Character
    {
        public EnemyCharacter(MapManager mapManager) : base(mapManager) { }

        abstract public void ActionDeath();

        abstract public void ActionRun();

        abstract public void ActionTakeHit();

        abstract public void ActionAttack();
    }
}
