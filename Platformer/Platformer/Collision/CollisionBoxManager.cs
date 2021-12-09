using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Characters;
using System.Collections.Generic;

namespace Platformer.Collision
{
    class CollisionBoxManager
    {
        private static Texture2D _pointTexture;
        private static Dictionary<CollisionBox, PlayerCharacter> playerCharactersBoxes = new Dictionary<CollisionBox, PlayerCharacter>();
        private static Dictionary<CollisionBox, EnemyCharacter> enemyCharactersBoxes = new Dictionary<CollisionBox, EnemyCharacter>();
        private static List<CollisionBox> gameEndBoxes = new List<CollisionBox>();
        private static List<CollisionBox> mapBoxes = new List<CollisionBox>();

        public static void Reset()
        {
            playerCharactersBoxes.Clear();
            enemyCharactersBoxes.Clear();
            gameEndBoxes.Clear();
            mapBoxes.Clear();
        }

        public static void AddPlayerCollisionBox(CollisionBox collisionBox, PlayerCharacter character)
        {
            playerCharactersBoxes.Add(collisionBox, character);
        }

        public static void AddEnemyCollisionBox(CollisionBox collisionBox, EnemyCharacter character)
        {
            enemyCharactersBoxes.Add(collisionBox, character);
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

        public static List<PlayerCharacter> IntersectWithPlayer(CollisionBox collisionBox)
        {
            List<PlayerCharacter> collidedBoxes = new List<PlayerCharacter>();

            foreach (var e in playerCharactersBoxes)
            {
                if (collisionBox.IsCollided(e.Key))
                    collidedBoxes.Add(e.Value);
            }
            return collidedBoxes;
        }

        public static List<EnemyCharacter> IntersectWithEnemy(CollisionBox collisionBox)
        {
            List<EnemyCharacter> collidedBoxes = new List<EnemyCharacter>();

            foreach (var e in enemyCharactersBoxes)
            {
                if (collisionBox.IsCollided(e.Key))
                    collidedBoxes.Add(e.Value);
            }
            return collidedBoxes;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var e in playerCharactersBoxes)
                DrawCollisionBox(spriteBatch, e.Key, Color.Red);
            foreach (var e in enemyCharactersBoxes)
                DrawCollisionBox(spriteBatch, e.Key, Color.Red);
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
