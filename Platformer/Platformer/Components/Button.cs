using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Platformer.Components
{
    public class Button : Component
    {
        private MouseState currentMouse;
        private MouseState previousMouse;
        private bool isHovering;

        public event EventHandler Click;

        public SpriteFont Font { get; set; }
        public Color Color { get; set; }
        public Color ColorHovering { get; set; }
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public Rectangle ButtonRectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); } }

        public Button(SpriteFont font, string text, Vector2 position, Vector2 size, Color color, Color colorHovering)
        {
            Font = font;
            Text = text;
            Color = color;
            ColorHovering = colorHovering;
            Position = position;
            Size = size;
        }

        public override void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();
            isHovering = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1).Intersects(ButtonRectangle);
            if (isHovering && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                Click?.Invoke(this, new EventArgs());
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {            
            Color drawColor = Color;
            if (isHovering)
                drawColor = ColorHovering;

            RectangleDrawer.DrawRectangleWithBackground(spriteBatch, ButtonRectangle, Color.Black, 3, drawColor);

            if (!string.IsNullOrEmpty(Text))
            {
                float x = Position.X + Size.X / 2 - Font.MeasureString(Text).X / 2;
                float y = Position.Y + Size.Y / 2 - Font.MeasureString(Text).Y / 2;
                spriteBatch.DrawString(Font, Text, new Vector2(x, y), Color.Black);
            }
        }
    }
}
