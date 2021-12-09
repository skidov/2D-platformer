using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Collision;

namespace Platformer.Characters
{
    public class EnemyCharacterController
    {
        public EnemyCharacter Character { get; private set; }
        float minXPos;
        float maxXPos;

        public EnemyCharacterController(EnemyCharacter character, float minXPos, float maxXPos)
        {
            this.Character = character;
            this.minXPos = minXPos;
            this.maxXPos = maxXPos;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 pos = Character.Position;
            if (pos.X > maxXPos)
                Character.Direction = CharacterDirection.LEFT;
            else if (pos.X < minXPos)
                Character.Direction = CharacterDirection.RIGHT;

            bool playerInRange = CollisionBoxManager.IntersectWithPlayer(Character.GetAttackCollisionBox).Count > 0;
            switch (Character.State)
            {
                case CharacterState.IDLE:
                    if (playerInRange)
                        Character.ActionAttack();
                    else
                        Character.ActionRun();
                    break;
                case CharacterState.RUN:
                    if (playerInRange)
                        Character.ActionAttack();
                    break;
            }
            Character.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Character.Draw(gameTime, spriteBatch);
        }
    }
}
