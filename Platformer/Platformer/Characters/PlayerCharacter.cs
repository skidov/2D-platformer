using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Map;

namespace Platformer.Characters
{
    public abstract class PlayerCharacter : Character
    {
        public PlayerCharacter(MapManager mapManager) : base(mapManager) { }

        abstract public void ActionJump();

        abstract public void ActionRun();

        abstract public void ActionAttack1();

        abstract public void ActionAttack2();
    }
}
