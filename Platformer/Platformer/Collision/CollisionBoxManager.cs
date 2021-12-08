using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Platformer.Collision
{
    class CollisionBoxManager
    {
        private static Texture2D _pointTexture;
        private static List<CollisionBox> playerCharactersBoxes = new List<CollisionBox>();
        private static List<CollisionBox> enemyCharactersBoxes = new List<CollisionBox>();
        private static List<CollisionBox> gameEndBoxes = new List<CollisionBox>();
        private static List<CollisionBox> mapBoxes = new List<CollisionBox>();

        public static void Reset()
        {
            playerCharactersBoxes.Clear();
            enemyCharactersBoxes.Clear();
            gameEndBoxes.Clear();
            mapBoxes.Clear();
        }

        public static void AddPlayerCollisionBox(CollisionBox collisionBox)
        {
            playerCharactersBoxes.Add(collisionBox);
        }

        public static void AddEnemyCollisionBox(CollisionBox collisionBox)
        {
            enemyCharactersBoxes.Add(collisionBox);
        }

        public static void AddGameEndCollisionBox(CollisionBox collisionBox)
        {
            gameEndBoxes.Add(collisionBox);
        }

        public static void AddMapCollisionBox(CollisionBox collisionBox)
        {
            mapBoxes.Add(collisionBox);
        }

        public static void RemovePlayerCollisionBox(CollisionBox collisionBox)
        {
            playerCharactersBoxes.Remove(collisionBox);
        }

        public static void RemoveEnemyCollisionBox(CollisionBox collisionBox)
        {
            enemyCharactersBoxes.Remove(collisionBox);
        }

        public static void RemoveGameEndCollisionBox(CollisionBox collisionBox)
        {
            gameEndBoxes.Remove(collisionBox);
        }

        public static void RemoveMapCollisionBox(CollisionBox collisionBox)
        {
            mapBoxes.Remove(collisionBox);
        }

        public static List<CollisionBox> IntersectWithMap(CollisionBox collisionBox)
        {
            List<CollisionBox> collidedBoxes = new List<CollisionBox>();

            foreach (var e in mapBoxes)
            {
                if (collisionBox.IsCollided(e))
                    collidedBoxes.Add(e);
            }
            return collidedBoxes;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var e in playerCharactersBoxes)
                DrawCollisionBox(spriteBatch, e, Color.Red);
            foreach (var e in enemyCharactersBoxes)
                DrawCollisionBox(spriteBatch, e, Color.Red);
            foreach (var e in gameEndBoxes)
                DrawCollisionBox(spriteBatch, e, Color.Blue);
            foreach (var e in mapBoxes)
                DrawCollisionBox(spriteBatch, e, Color.Purple);
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
