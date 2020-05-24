using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;

namespace Nsnbc.Android
{
    public abstract class CommonGame : Game, IInputMatrices
    {
        protected CommonGame()
        {
            VirtualWidth = 1920;
            VirtualHeight = 1080;
        }
        
        protected GraphicsDeviceManager Graphics;
        protected int VirtualWidth;
        protected int VirtualHeight;
        protected bool UpdateMatrix = true;
        protected Matrix ScaleMatrix = Matrix.Identity;

        public Matrix Scale { 
            get {
                if(UpdateMatrix) {
                    CreateScaleMatrix();
                    UpdateMatrix =false;
                    Root.InputScaling = this;
                }
                return ScaleMatrix;
            }
        }
        protected void CreateScaleMatrix() {
            ScaleMatrix = Matrix.CreateScale(
                (float)GraphicsDevice.Viewport.Width/ VirtualWidth, 
                (float)GraphicsDevice.Viewport.Height/ VirtualHeight, 1f);
        }
 
        public Matrix InputScale => Matrix.Invert(Scale);
        public Vector2 InputTranslate => new Vector2(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y);

        protected float GetVirtualAspectRatio ()
        {
            return VirtualWidth / (float)VirtualHeight;   
        }
 

        public Viewport MyViewport { get; set; }
        protected void ResetViewport ()
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
            Viewport viewport = new Viewport ();   
            viewport.X = (Graphics.PreferredBackBufferWidth / 2) - (width / 2); 
            viewport.Y = (Graphics.PreferredBackBufferHeight / 2) - (height / 2); 
            viewport.Width = width; 
            viewport.Height = height; 
            viewport.MinDepth = 0; 
            viewport.MaxDepth = 1;     	
            if (changed) {
                UpdateMatrix = true;
            }   
            GraphicsDevice.Viewport = viewport;
            MyViewport = viewport;
        }
    }
}