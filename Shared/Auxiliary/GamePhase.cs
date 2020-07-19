using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc;
using Nsnbc.PostSharp;

namespace Auxiliary
{
    /// <summary>
    /// An abstract class that represents a layer of UI. These layers ("phases") are stacked on top of each other. All of them get drawn, but only the top one gets updated.
    /// </summary>
    [PublicAPI]  
    [Trace]
    public abstract class GamePhase
    {
    
        /// <summary>
        /// Gets or sets the flag that determines whether this phase will be deleted during the next Root.Update() cycle.
        /// </summary>
        public bool ScheduledForElimination { get; set; }


        /// <summary>
        /// Virtual method. Override this to perform drawing this phase. The base method will draw all UIElements of this phase. 
        /// This method will be called regardless of whether this phase is on top of the stack.
        /// </summary>
        /// <param name="sb">The spriteBatch to use. The method assumes the spriteBatch is already Begun.</param>
        /// <param name="game">The game.</param>
        /// <param name="elapsedSeconds">Seconds elapsed since last draw cycle.</param>
        protected internal abstract void Draw(SpriteBatch sb, Game game, float elapsedSeconds);

        /// <summary>
        /// Virtual method. Override this to perform updates in this phase. This method will only be called if this phase is on top of stack. The base method causes all UI Elements in the UIElement list to update.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="elapsedSeconds">Seconds elapsed since last update cycle.</param>
        protected internal virtual void Update(Game game, float elapsedSeconds)
        {
            if ((Root.WasMouseLeftClick || Root.WasTouchReleased))
            {
                Root.WasMouseLeftClick = false;
                Root.WasTouchReleased = false;
                if (Ux.MouseOverAction != null)
                {
                    Ux.MouseOverAction();
                }
            }
        }
        /// <summary>
        /// Performs any initialization code. Base method is empty.
        /// </summary>
        /// <param name="game">The game.</param>
        protected internal virtual void Initialize(Game game)
        {

        }
        /// <summary>
        /// Performs any destruction code, then causes the phase to be flagged for removal from stack.
        /// Base method causes this by setting the ScheduledForElimination flag.
        /// </summary>
        /// <param name="game">The game.</param>
        public virtual void Destruct(Game game)
        {
            ScheduledForElimination = true;
        }
    }
}
