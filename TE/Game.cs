using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TE.GameComponents;

namespace TE
{
    public class TEGame : Game
    {
        private SpriteFont minecraftFont;
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphics;

        private Player player;
        private List<Ball> balls;

        private int score = 0;
        private int misses = 0;

        private float timer = 1.0f;

        private Random random = new Random(Guid.NewGuid().GetHashCode());

        public TEGame()
        {
            balls = new List<Ball>();

            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            player = new Player(this);

            Components.Add(player);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            minecraftFont = Content.Load<SpriteFont>(@"Fonts\Minecraft");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Services.AddService(typeof(SpriteBatch), spriteBatch);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer <= 0)
            {
                var ball = new Ball(this);

                balls.Add(ball);
                Components.Add(ball);

                timer = random.Next(1, 4);
            }

            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].Position.Y > GraphicsDevice.Viewport.Height)
                {
                    balls[i].Dispose();

                    Components.Remove(balls[i]);

                    balls.RemoveAt(i);

                    ++misses;

                    Console.WriteLine("[Runtime Logs] Ball Touched Ground");
                }

                if (balls[i].Position.X + Ball.width > player.Position.X && balls[i].Position.X < player.Position.X + Player.width &&
                    balls[i].Position.Y + Ball.heigth > player.Position.Y && balls[i].Position.Y < player.Position.Y + Player.heigth)
                {
                    balls[i].Dispose();

                    Components.Remove(balls[i]);

                    balls.RemoveAt(i);

                    score += 10;

                    Console.WriteLine("[Runtime Logs] Ball Touched Player");
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(minecraftFont, "Score: " + score, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(minecraftFont, "Misses: " + misses, new Vector2(0, 20), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}