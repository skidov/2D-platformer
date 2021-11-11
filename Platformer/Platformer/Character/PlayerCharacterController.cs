using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Character
{
    public class PlayerCharacterController
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
                    if (Keyboard.GetState().IsKeyDown(Keys.N))
                        character.ActionAttack1();
                    else if (Keyboard.GetState().IsKeyDown(Keys.M))
                        character.ActionAttack2();
                    else if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D))
                        character.ActionRun();
                    else if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        character.ActionJump();
                     break;
                case CharacterState.RUN:
                    if (Keyboard.GetState().IsKeyDown(Keys.N))
                        character.ActionAttack1();
                    else if (Keyboard.GetState().IsKeyDown(Keys.M))
                        character.ActionAttack2();
                    else if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        character.ActionJump();
                    else if (!Keyboard.GetState().IsKeyDown(Keys.A) && !Keyboard.GetState().IsKeyDown(Keys.D))
                        character.ActionIdle();
                    break;
                case CharacterState.JUMP:
                    if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D))
                        character.MoveWhileFall = true;
                    else
                        character.MoveWhileFall = false;
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
