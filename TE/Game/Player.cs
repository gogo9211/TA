using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TE.GameComponents
{
    public class Player : DrawableGameComponent
    {
        private Texture2D texture;
        private Game _game;

        private Vector2 position;
        private int textureWidth = 30;

        public Vector2 Position { get { return position; } }

        public Player(Game game) : base(game)
        {
            _game = game;

            position = new Vector2(0, 0);

            if (game == null)
                throw new ArgumentNullException("Invalid Game Instance");
        }

        public override void Initialize()
        {
            position = new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth / 2, GraphicsDevice.PresentationParameters.BackBufferHeight - textureWidth);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Sprites/Player");
            
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            texture.Dispose();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            MouseState mouseState = Mouse.GetState();

            if (GraphicsDevice.Viewport.Bounds.Contains(mouseState.X, mouseState.Y) && mouseState.X < GraphicsDevice.PresentationParameters.BackBufferWidth - textureWidth)
                position.X = mouseState.X;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _game.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
            _game.spriteBatch.Draw(texture, position, Color.White);
            _game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}