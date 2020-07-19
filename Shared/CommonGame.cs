using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.PostSharp;

namespace Nsnbc
{
    [Trace]
    public abstract class CommonGame : Game, IInputMatrices
    {
        private SpriteBatch spriteBatch = null!;
        protected readonly GraphicsDeviceManager Graphics;
        private readonly int virtualWidth;
        private readonly int virtualHeight;
        private bool updateMatrix = true;
        private Matrix scaleMatrix = Matrix.Identity;
        
        protected CommonGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            virtualWidth = 1920;
            virtualHeight = 1080;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Primitives.Init(spriteBatch, GraphicsDevice);
            Writer.SpriteBatch = spriteBatch;
            Root.Init(spriteBatch, this, Graphics);
            Root.Graphics.GraphicsDevice.Clear(Color.Black);
            Library.Init(Content);
            ResetViewport();
            // For some reason, you can't initialize the scaling here otherwise it gets broken. We must wait for the first
            // Draw cycle and do it within the spritebatch loop.
            Eqatec.Send("VERSION " + typeof(CommonGame).Assembly.GetName().Version.ToString(3));
            #if DEBUG
            Eqatec.Send("BUILD DEBUG");
            #endif
            PhaseLoop.EnterFirstPhase();
        }
        
        [Trace(AttributeExclude = true)]
        protected override void Update(GameTime gameTime)
        {
            PhaseLoop.Update(gameTime);
            base.Update(gameTime);
        }

        [Trace(AttributeExclude = true)]
        protected override void Draw(GameTime gameTime)
        {    
            if (MyViewport.X != GraphicsDevice.Viewport.X || MyViewport.Y != GraphicsDevice.Viewport.Y)
            {
                ResetViewport();
            }
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, transformMatrix: Scale);
            PhaseLoop.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        private Matrix Scale { 
            get {
                if(updateMatrix) {
                    CreateScaleMatrix();
                    updateMatrix =false;
                    Root.InputScaling = this;
                }
                return scaleMatrix;
            }
        }

        private void CreateScaleMatrix() {
            scaleMatrix = Matrix.CreateScale(
                (float)GraphicsDevice.Viewport.Width/ virtualWidth, 
                (float)GraphicsDevice.Viewport.Height/ virtualHeight, 1f);
        }
 
        public Matrix InputScale => Matrix.Invert(Scale);
        public Vector2 InputTranslate => new Vector2(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y);

        private float GetVirtualAspectRatio ()
        {
            return virtualWidth / (float)virtualHeight;   
        }

        
        private Viewport MyViewport { get; set; }

        private void ResetViewport ()
        {
            float targetAspectRatio = GetVirtualAspectRatio ();   
            // figure out the largest area that fits in this resolution at the desired aspect ratio     
            int width = Graphics.PreferredBackBufferWidth;   
            int height = (int)(width / targetAspectRatio + .5f);   
            bool changed = false;     
            if (height > Graphics.PreferredBackBufferHeight) { 
                height = Graphics.PreferredBackBufferHeight;   
                // PillarBox 
                width = (int)(height * targetAspectRatio + .5f);
                changed = true;   
            }     
            // set up the new viewport centered in the backbuffer 
            Viewport viewport = new Viewport
            {
                X = (Graphics.PreferredBackBufferWidth / 2) - (width / 2),
                Y = (Graphics.PreferredBackBufferHeight / 2) - (height / 2),
                Width = width,
                Height = height,
                MinDepth = 0,
                MaxDepth = 1
            };

            if (changed) {
                updateMatrix = true;
            }   
            GraphicsDevice.Viewport = viewport;
            MyViewport = viewport;
        }

        public void ApplyFullScreenModeChanges()
        {
            PlatformServices.Services.ApplyFullscreenModeChanges();
        }
    }
}