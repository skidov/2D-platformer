using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Platformer.Physics
{
    class CollisionBoxManager
    {
        private static Texture2D _pointTexture;
        private static List<CollisionBox> playerCharacters = new List<CollisionBox>();
        private static List<CollisionBox> enemyCharacters = new List<CollisionBox>();

        public static void Reset()
        {
            playerCharacters.Clear();
            enemyCharacters.Clear();
        }

        public static void AddPlayerCollisionBox(CollisionBox collisionBox)
        {
            playerCharacters.Add(collisionBox);
        }

        public static void AddEnemyCollisionBox(CollisionBox collisionBox)
        {
            enemyCharacters.Add(collisionBox);
        }

        public static void RemovePlayerCollisionBox(CollisionBox collisionBox)
        {
            playerCharacters.Remove(collisionBox);
        }

        public static void RemoveEnemyCollisionBox(CollisionBox collisionBox)
        {
            enemyCharacters.Remove(collisionBox);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var e in playerCharacters)
                DrawCollisionBox(spriteBatch, e, Color.Red);
            foreach (var e in enemyCharacters)
                DrawCollisionBox(spriteBatch, e, Color.Red);

            spriteBatch.Draw(_pointTexture, new Rectangle(-1000, -1, 2000, 1), Color.Black);
        }

        public static void DrawCollisionBox(SpriteBatch spriteBatch, CollisionBox collisionBox, Color color)
        {
            DrawRectangle(spriteBatch, new Rectangle(
                (int)(collisionBox.Center.X - collisionBox.HalfSize.X),
                (int)(collisionBox.Center.Y - collisionBox.HalfSize.Y),
                (int)collisionBox.HalfSize.X * 2,
                (int)collisionBox.HalfSize.Y * 2),
                color, 1);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            if (_pointTexture == null)
            {
                _pointTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _pointTexture.SetData<Color>(new Color[] { Color.White });
            }

            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + lineWidth, lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + lineWidth, lineWidth), color);
        }
    }
}
