using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Collections.Generic;

namespace Platformer.Map
{
    public abstract class Map
    {
        public float Scale { get; internal set; }
        public TiledMap _TiledMap { get; internal set; }
        public TiledMapRenderer _TiledMapRenderer { get; internal set; }
        public List<Texture2D> BackGround { get; internal set; }
    }
}
