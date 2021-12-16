using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Characters;
using Platformer.Collision;
using Platformer.Map;
using Platformer.Texture;

namespace Platformer.Items
{
    public abstract class Item
    {
        private Vector2 position;

        internal Animation Animation { get; set; }
        internal CollisionBox ItemCollisionBox { get; set; }
        internal Vector2 ItemCollisionBoxOffSet { get; set; }
        internal MapManager _MapManager { get; set; }

        public Item(MapManager mapManager)
        {
            _MapManager = mapManager;
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                ItemCollisionBox.Center = value + ItemCollisionBoxOffSet;
            }
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
