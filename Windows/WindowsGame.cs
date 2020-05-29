using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows.Forms;
using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc;
using Nsnbc.Android;
using Nsnbc.Android.Auxiliary;
using Nsnbc.Auxiliary;
using Origin.Display;

namespace Windows
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class WindowsGame : CommonGame
    {
        private SpriteBatch spriteBatch;
        private readonly Resolution theResolution;

        public WindowsGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Window.Title = "Najdi cestu ven!";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            IntPtr hWnd = Window.Handle;
            var control = Control.FromHandle(hWnd);
            Form form1 = control.FindForm();
            form1.FormBorderStyle = FormBorderStyle.None;
            //  form.TopMost = true;
            theResolution = Utilities.GetSupportedResolutions().Last();
            form1.Width = theResolution.Width;
            form1.Height = theResolution.Height;
            form1.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Graphics.PreferredBackBufferWidth = theResolution.Width;
            Graphics.PreferredBackBufferHeight = theResolution.Height;
            Graphics.ApplyChanges();
            LocalDataStore.Init(IsolatedStorageFile.GetUserStoreForDomain());
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Library.Init(Content);
            Primitives.Init(spriteBatch, GraphicsDevice);
            Writer.SpriteBatch = spriteBatch;
            LoadTheContent();
            Root.Init(spriteBatch, this, Graphics);
            ResetViewport();
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
            if (MyViewport.X != GraphicsDevice.Viewport.X || MyViewport.Y != GraphicsDevice.Viewport.Y)
            {
                ResetViewport();
            }
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(transformMatrix: Scale);
            PhaseLoop.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
