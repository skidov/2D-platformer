using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Collision;
using Platformer.Map;

namespace Platformer.Characters
{
    public abstract class PlayerCharacter : Character
    {
        internal Vector2 Attack1CollisionBoxOffSet;
        internal Vector2 Attack1CollisionBoxHalfSize;
        internal float Attack1CollisionBoxRightOffSet;
        internal Vector2 Attack2CollisionBoxOffSet;
        internal Vector2 Attack2CollisionBoxHalfSize;
        internal float Attack2CollisionBoxRightOffSet;

        public CollisionBox GetAttack1CollisionBox
        {
            get
            {
                CollisionBox cb = new CollisionBox(
                new Vector2(Position.X + Attack1CollisionBoxOffSet.X, Position.Y + Attack1CollisionBoxOffSet.Y),
                new Vector2(Attack1CollisionBoxHalfSize.X, Attack1CollisionBoxHalfSize.Y));
                if (Direction == CharacterDirection.RIGHT)
                    cb.Center += Vector2.UnitX * Attack1CollisionBoxRightOffSet;
                return cb;
            }
        }

        public CollisionBox GetAttack2CollisionBox
        {
            get
            {
                CollisionBox cb = new CollisionBox(
                new Vector2(Position.X + Attack2CollisionBoxOffSet.X, Position.Y + Attack2CollisionBoxOffSet.Y),
                new Vector2(Attack2CollisionBoxHalfSize.X, Attack2CollisionBoxHalfSize.Y));
                if (Direction == CharacterDirection.RIGHT)
                    cb.Center += Vector2.UnitX * Attack2CollisionBoxRightOffSet;
                return cb;
            }
        }

        public PlayerCharacter(MapManager mapManager) : base(mapManager) { }

        abstract public void ActionJump();

        abstract public void ActionRun();

        abstract public void ActionAttack1();

        abstract public void ActionAttack2();
    }
}
