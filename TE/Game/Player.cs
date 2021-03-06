using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TE.GameComponents
{
    public class Player : DrawableGameComponent
    {
        public const int width = 45;
        public const int heigth = 10;

        private Texture2D texture;
        private SpriteBatch spriteBatch;

        private Rectangle position;

        public Vector2 Position { get { return new Vector2(position.X, position.Y); } }

        public Player(Game game) : base(game)
        {
            position = new Rectangle(
              game.GraphicsDevice.Viewport.Width / 2,
              game.GraphicsDevice.Viewport.Height - heigth * 2,
              width,
              heigth);

            if (game == null)
                throw new ArgumentNullException("Invalid Game Instance");
        }

        protected override void LoadContent()
        {
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.White });

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            texture.Dispose();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MouseState mouseState = Mouse.GetState();

            if (GraphicsDevice.Viewport.Bounds.Contains(mouseState.X, mouseState.Y) && mouseState.X < GraphicsDevice.PresentationParameters.BackBufferWidth - width / 2 && mouseState.X > width / 2)
                position.X = mouseState.X - width / 2;
        }

        public override void Draw(GameTime gameTime)
        {
            if (spriteBatch == null)
                spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}