using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Rotating_with_the_Keyboard
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D shipTexture;
        float shipAngle;
        float shipSpeed;
        Vector2 shipPosition;
        Rectangle shipRect;
        Vector2 shipDirection; //new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

        KeyboardState keyboardState;

        Texture2D rectTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            shipAngle = 0;
            shipSpeed = 2.5f;
            shipPosition = new Vector2(100, 100);
            shipRect = new Rectangle(shipPosition.ToPoint(), new Point(50, 50));
            shipDirection = Vector2.Zero;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            rectTexture = Content.Load<Texture2D>("Images/rectangle");
            shipTexture = Content.Load<Texture2D>("Images/enterprise");
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (keyboardState.IsKeyDown(Keys.Left))
                shipAngle -= 0.08f;
            if (keyboardState.IsKeyDown(Keys.Right))
                shipAngle += 0.08f;

            shipDirection = new Vector2((float)Math.Cos(shipAngle), (float)Math.Sin(shipAngle));

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                shipPosition += shipDirection * shipSpeed;
                shipRect.Location = shipPosition.ToPoint();
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                shipPosition -= shipDirection * shipSpeed;
                shipRect.Location = shipPosition.ToPoint();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            // Draws hitbox
            _spriteBatch.Draw(rectTexture, shipRect, Color.Red);

            // Rotates texture and draws it over hitbox
            _spriteBatch.Draw(shipTexture, new Rectangle(shipRect.Center, shipRect.Size), null, Color.White, shipAngle, new Vector2(shipTexture.Width / 2, shipTexture.Height / 2), SpriteEffects.None, 1f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}