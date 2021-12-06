using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Platformer.Collision
{
    public class CollisionBox
    {
        public Vector2 Center { get; set; }
        public Vector2 HalfSize { get; set; }

        public CollisionBox(Vector2 center, Vector2 halfSize)
        {
            this.Center = center;
            this.HalfSize = halfSize;
        }

        public bool IsCollided(CollisionBox other)
        {
            if (Math.Abs(Center.X - other.Center.X) > HalfSize.X + other.HalfSize.X) return false;
            if (Math.Abs(Center.Y - other.Center.Y) > HalfSize.Y + other.HalfSize.Y) return false;
            return true;
        }
    }
}
