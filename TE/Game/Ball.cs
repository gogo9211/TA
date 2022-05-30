using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TE.GameComponents
{
    public class Ball : DrawableGameComponent
    {
        public const int width = 17;
        public const int heigth = 17;

        private int speed;

        private Texture2D texture;
        private SpriteBatch spriteBatch;

        private Vector2 position;

        public Vector2 Position { get { return position; } }

        public Ball(Game game) : base(game)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            speed = random.Next(30, 150);

            position = new Vector2(random.Next(width, Game.Window.ClientBounds.Width) - width, 0);

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

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position.Y += speed * elapsed;
        }

        public override void Draw(GameTime gameTime)
        {
            if (spriteBatch == null)
                spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, width, heigth), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}