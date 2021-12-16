using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Platformer.Characters;
using Platformer.Items;
using Platformer.Traps;
using System.Collections.Generic;

namespace Platformer.Map
{
    public class Map2 : Map
    {
        private const float SCALE = 1.8f;

        public Map2(ContentManager content, GraphicsDevice graphicsDevice)
        {
            BackGround = new List<Texture2D>();
            BackGround.Add(content.Load<Texture2D>("Map/basicmaptype/tiles/Background_2"));
            BackGround.Add(content.Load<Texture2D>("Map/basicmaptype/tiles/Background_1"));
            _TiledMap = content.Load<TiledMap>("Map/basicmaptype/map2");
            _TiledMapRenderer = new TiledMapRenderer(graphicsDevice, _TiledMap);
            Scale = SCALE;

            Skeleton.LoadContent(content);
            SpikesTrap.LoadContent(content);
            Coin.LoadContent(content);
        }
    }
}
