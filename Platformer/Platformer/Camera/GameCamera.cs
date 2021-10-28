using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Platformer.Camera
{
    class GameCamera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Vector2 target)
        {
            var gameScreen = Matrix.CreateTranslation(
                Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2,
                0);

            Debug.WriteLine(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2);
            Debug.WriteLine(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2);

            Transform = Matrix.CreateTranslation(
                -target.X - 150,
                -target.Y - 150,
                0) * gameScreen;
        }
    }
}
