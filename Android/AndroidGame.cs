
using System;
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
    public class AndroidGame : Game, IInputMatrices
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        private int virtualWidth;
        private int virtualHeight;
        private bool updateMatrix = true;
        private Matrix scaleMatrix = Matrix.Identity;
        public Viewport Viewport => new Viewport(0, 0, virtualWidth, virtualHeight);

        public Matrix Scale { 
            get {
                if(updateMatrix) {
                    CreateScaleMatrix();
                    updateMatrix =false;
                    Root.InputScaling = this;
                }
                return scaleMatrix;
            }
        }
        protected void CreateScaleMatrix() {
            scaleMatrix = Matrix.CreateScale(
                (float)GraphicsDevice.Viewport.Width/ virtualWidth, 
                (float)GraphicsDevice.Viewport.Height/ virtualHeight, 1f);
        }
 
        public Matrix InputScale => Matrix.Invert(Scale);

        public Vector2 InputTranslate => new Vector2(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y);

        public AndroidGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            // graphics.PreferredBackBufferWidth = 1920;
            // graphics.PreferredBackBufferHeight = 1080;
            this.virtualWidth = 1920;
            this.virtualHeight = 1080;
            TouchPanel.EnabledGestures = GestureType.None;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Library.Init(this.Content);
            Primitives.Init(spriteBatch, GraphicsDevice);
            Writer.SpriteBatch = spriteBatch;
            Root.Init(spriteBatch, this, graphics);
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
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) Game.Activity.MoveTaskToBack(true);
            
            base.Update(gameTime);
        }
        
        protected void FullViewport ()
        { 
            Viewport vp = new Viewport (); 
            vp.X = vp.Y = 0; 
            vp.Width = this.graphics.PreferredBackBufferWidth;
            vp.Height =  this.graphics.PreferredBackBufferHeight;
            GraphicsDevice.Viewport = vp;   
        }
 
        protected float GetVirtualAspectRatio ()
        {
            return(float)virtualWidth / (float)virtualHeight;   
        }
 
        protected void ResetViewport ()
        {
            float targetAspectRatio = GetVirtualAspectRatio ();   
            // figure out the largest area that fits in this resolution at the desired aspect ratio     
            int width = this.graphics.PreferredBackBufferWidth;   
            int height = (int)(width / targetAspectRatio + .5f);   
            bool changed = false;     
            if (height > graphics.PreferredBackBufferHeight) { 
                height = graphics.PreferredBackBufferHeight;   
                // PillarBox 
                width = (int)(height * targetAspectRatio + .5f);
                changed = true;   
            }     
            // set up the new viewport centered in the backbuffer 
            Viewport viewport = new Viewport ();   
            viewport.X = (graphics.PreferredBackBufferWidth / 2) - (width / 2); 
            viewport.Y = (graphics.PreferredBackBufferHeight / 2) - (height / 2); 
            viewport.Width = width; 
            viewport.Height = height; 
            viewport.MinDepth = 0; 
            viewport.MaxDepth = 1;     	
            if (changed) {
                updateMatrix = true;
            }   
            GraphicsDevice.Viewport = viewport;
            MyViewport = viewport;
        }

        public Viewport MyViewport { get; set; }

        protected void BeginDraw ()
        {   
            // // Start by reseting viewport 
            // FullViewport ();   
            // // Clear to Black 
            // GraphicsDevice.Clear (Color.Lime);   
            // // Calculate Proper Viewport according to Aspect Ratio 
            // ResetViewport ();   
            // // and clear that    
            // // This way we are gonna have black bars if aspect ratio requires it and     
            // // the clear color on the rest 
            GraphicsDevice.Clear (Color.Purple);   
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
