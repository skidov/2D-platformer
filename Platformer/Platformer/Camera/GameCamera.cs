using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Camera
{
    public class GameCamera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Vector2 target)
        {
            var gameScreen = Matrix.CreateTranslation(
                Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2,
                0);
            Transform = Matrix.CreateTranslation(
                -target.X,
                -target.Y,
                0) * gameScreen;
        }
    }
}
