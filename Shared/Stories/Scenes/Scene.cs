using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.PostSharp;

namespace Nsnbc.Stories.Scenes
{
    [Serializable]
    public abstract class Scene
    {      
        public Rectangle CurrentZoom { get; set; }

        
        public virtual void Begin(Session hardSession)
        {
            CurrentZoom = CommonGame.R1920x1080;
        }

        [Trace(AttributeExclude = true)]
        public abstract void Draw(AirSession airSession);

        /// <summary>
        /// Performs actions in response to a left-click somewhere on the screen while this is the active scene. Returns true if the click was handled by the scene.
        /// </summary>
        /// <param name="airSession">The current air-session.</param>
        /// <returns>True if this scene managed to use the click. False if other stuff should be given a chance to do something with the click.</returns>
        public virtual bool Click(AirSession airSession)
        {
            return false;
        }

    }
}