using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Collision;
using Platformer.Map;

namespace Platformer.Characters
{
    public abstract class EnemyCharacter : Character
    {
        internal Vector2 AttackCollisionBoxOffSet;
        internal Vector2 AttackCollisionBoxHalfSize;
        internal float AttackCollisionBoxRightOffSet;

        public CollisionBox GetAttackCollisionBox
        {
            get
            {
                CollisionBox cb = new CollisionBox(
                new Vector2(Position.X + AttackCollisionBoxOffSet.X, Position.Y + AttackCollisionBoxOffSet.Y),
                new Vector2(AttackCollisionBoxHalfSize.X, AttackCollisionBoxHalfSize.Y));
                if (Direction == CharacterDirection.RIGHT)
                    cb.Center += Vector2.UnitX * AttackCollisionBoxRightOffSet;
                return cb;
            }
        }

        public EnemyCharacter(MapManager mapManager) : base(mapManager) { }

        abstract public void ActionRun();

        abstract public void ActionAttack();
    }
}
