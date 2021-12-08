using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Platformer.Camera;
using Platformer.Character;
using Platformer.Collision;
using System.Collections.Generic;

namespace Platformer.Map
{
    public class BasicMap : Map
    {
        private const float SCALE = 1.8f;

        public BasicMap(ContentManager content, GraphicsDevice graphicsDevice)
        {
            BackGround = new List<Texture2D>();
            BackGround.Add(content.Load<Texture2D>("Map/basicmap/tiles/Background_2"));
            BackGround.Add(content.Load<Texture2D>("Map/basicmap/tiles/Background_1"));
            _TiledMap = content.Load<TiledMap>("Map/basicmap/basicmap");
            _TiledMapRenderer = new TiledMapRenderer(graphicsDevice, _TiledMap);
            Scale = SCALE;

            Huntress.LoadContent(content);
            Skeleton.LoadContent(content);
        }
    }
}
