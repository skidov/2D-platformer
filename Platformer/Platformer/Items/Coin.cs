using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Characters;
using Platformer.Collision;
using Platformer.Map;
using Platformer.Texture;

namespace Platformer.Items
{
    class Coin : Item
    {
        private const int COLLISION_BOX_OFF_SET_X = 10;
        private const int COLLISION_BOX_OFF_SET_Y = 15;
        private const int COLLISION_BOX_HALF_SIZE_X = 10;
        private const int COLLISION_BOX_HALF_SIZE_Y = 10;

        private static SpriteSheet sheetRed;
        private static SpriteSheet sheetSilver;
        private static SpriteSheet sheetGold;

        public static void LoadContent(ContentManager content)
        {
            sheetRed = new SpriteSheet(content.Load<Texture2D>("Items/MonedaR"), 5, 1);
            sheetSilver = new SpriteSheet(content.Load<Texture2D>("Items/MonedaP"), 5, 1);
            sheetGold = new SpriteSheet(content.Load<Texture2D>("Items/MonedaD"), 5, 1);
        }


        public int Value { get; set; }

        public Coin(MapManager mapManager, Vector2 pos, int value) : base(mapManager)
        {
            ItemCollisionBoxOffSet = new Vector2(COLLISION_BOX_OFF_SET_X, COLLISION_BOX_OFF_SET_Y);
            ItemCollisionBox = new CollisionBox(new Vector2(0, 0), new Vector2(COLLISION_BOX_HALF_SIZE_X, COLLISION_BOX_HALF_SIZE_Y));
            this.Position = pos - ItemCollisionBoxOffSet - new Vector2(0, COLLISION_BOX_HALF_SIZE_Y);

            Value = value;

            if (value < 2)
                Animation = new Animation(sheetRed, true, 0.15);
            else if (value < 5)
                Animation = new Animation(sheetSilver, true, 0.15);
            else
                Animation = new Animation(sheetGold, true, 0.15);

            Animation.Effect = SpriteEffects.None;
            Animation.Scale = 1.3f;
        }

        public override void Update(GameTime gameTime)
        {
            var players = CollisionBoxManager.IntersectWithPlayer(ItemCollisionBox);
            if (players.Count > 0)
            {
                foreach (var e in players)
                {
                    e.Coins += Value;
                }
                _MapManager.DeleteItem(this);
            }
            Animation.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Animation.Draw(spriteBatch, Position);
        }
    }
}
