using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Character
{
    class EnemyCharacterController
    {
        EnemyCharacter character;
        float minXPos;
        float maxXPos;

        public EnemyCharacterController(EnemyCharacter character, float minXPos, float maxXPos)
        {
            this.character = character;
            this.minXPos = minXPos;
            this.maxXPos = maxXPos;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 pos = character.Position;
            if (pos.X > maxXPos)
                character.Direction = CharacterDirection.LEFT;
            else if (pos.X < minXPos)
                character.Direction = CharacterDirection.RIGHT;
            switch (character.State)
            {
                case CharacterState.IDLE:
                    character.ActionRun();
                    break;
            }
            character.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            character.Draw(gameTime, spriteBatch);
        }
    }
}
