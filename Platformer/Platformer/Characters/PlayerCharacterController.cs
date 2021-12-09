using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Characters
{
    public class PlayerCharacterController
    {
        public PlayerCharacter Character { get; private set; }

        public PlayerCharacterController(PlayerCharacter character)
        {
            this.Character = character;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Character.Direction = CharacterDirection.LEFT;
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                Character.Direction = CharacterDirection.RIGHT;

            switch (Character.State)
            {
                case CharacterState.IDLE:
                    if (Keyboard.GetState().IsKeyDown(Keys.N))
                        Character.ActionAttack1();
                    else if (Keyboard.GetState().IsKeyDown(Keys.M))
                        Character.ActionAttack2();
                    else if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D))
                        Character.ActionRun();
                    else if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        Character.ActionJump();
                     break;
                case CharacterState.RUN:
                    if (Keyboard.GetState().IsKeyDown(Keys.N))
                        Character.ActionAttack1();
                    else if (Keyboard.GetState().IsKeyDown(Keys.M))
                        Character.ActionAttack2();
                    else if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        Character.ActionJump();
                    else if (!Keyboard.GetState().IsKeyDown(Keys.A) && !Keyboard.GetState().IsKeyDown(Keys.D))
                        Character.ActionIdle();
                    break;
                case CharacterState.JUMP:
                    if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D))
                        Character.MoveWhileFall = true;
                    else
                        Character.MoveWhileFall = false;
                    break;
                case CharacterState.FALL:
                    if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D))
                        Character.MoveWhileFall = true;
                    else
                        Character.MoveWhileFall = false;
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
