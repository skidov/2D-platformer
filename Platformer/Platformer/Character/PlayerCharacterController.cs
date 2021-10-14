using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Character
{
    class PlayerCharacterController
    {
        PlayerCharacter character;

        public PlayerCharacterController(PlayerCharacter character)
        {
            this.character = character;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                character.Direction = CharacterDirection.LEFT;
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                character.Direction = CharacterDirection.RIGHT;

            switch (character.State)
            {
                case CharacterState.IDLE:
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                        character.ActionRun();
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                        character.ActionRun();
                    break;
                case CharacterState.RUN:
                    if (!Keyboard.GetState().IsKeyDown(Keys.A) && !Keyboard.GetState().IsKeyDown(Keys.D))
                        character.ActionIdle();
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
