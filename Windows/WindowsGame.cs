using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nsnbc;
using Nsnbc.Android;
using Nsnbc.Auxiliary;
using Origin.Display;

namespace Windows
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class WindowsGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public WindowsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            this.Window.Title = "Najdi cestu ven!";
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Library.Init(this.Content);
            Primitives.Init(spriteBatch, GraphicsDevice);
            Writer.SpriteBatch = spriteBatch;
            Root.Init(spriteBatch, this, graphics);
            PhaseLoop.EnterFirstPhase();
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            PhaseLoop.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            PhaseLoop.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
