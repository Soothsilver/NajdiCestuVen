using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Nsnbc;
using Nsnbc.Android;
using Nsnbc.Auxiliary;
using Origin.Display;

namespace Android
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class AndroidGame : CommonGame
    {
        private SpriteBatch spriteBatch;

        public AndroidGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Graphics.IsFullScreen = true;
            TouchPanel.EnabledGestures = GestureType.None;
            Graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Library.Init(Content);
            Primitives.Init(spriteBatch, GraphicsDevice);
            Writer.SpriteBatch = spriteBatch;
            Root.Init(spriteBatch, this, Graphics);
            Root.Graphics.GraphicsDevice.Clear(Color.Black);
            // foreach (ArtName artName in (ArtName[])Enum.GetValues(typeof(ArtName)))
            // {
            //     Library.FlippedArt(artName);
            // }
            
            ResetViewport();
            PhaseLoop.EnterFirstPhase();
        }
        
        protected override void Update(GameTime gameTime)
        {
            PhaseLoop.Update(gameTime);
            Root.UpdateTouch();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) Activity.MoveTaskToBack(true);
            
            base.Update(gameTime);
        }
        
        protected void FullViewport ()
        { 
            Viewport vp = new Viewport (); 
            vp.X = vp.Y = 0; 
            vp.Width = Graphics.PreferredBackBufferWidth;
            vp.Height =  Graphics.PreferredBackBufferHeight;
            GraphicsDevice.Viewport = vp;   
        }
 
        protected override void Draw(GameTime gameTime)
        {
            if (MyViewport.X != GraphicsDevice.Viewport.X || MyViewport.Y != GraphicsDevice.Viewport.Y)
            {
                ResetViewport();
            }
            GraphicsDevice.Clear(Color.Black);

            // BeginDraw();
            spriteBatch.Begin(SpriteSortMode.Immediate, transformMatrix: Scale);
            PhaseLoop.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
