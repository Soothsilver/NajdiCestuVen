using Nsnbc.Core;

namespace Nsnbc.Events
{
    /// <summary>
    /// Represents an atomic part of a script, such as a single dialogue line or the starting of a song.
    /// </summary>
    public abstract class QEvent
    {
        /// <summary>
        /// Executes the event. This may cause an activity or a blocking activity to happen, sound to playback etc.
        /// </summary>
        /// <param name="airSession">The current air-session to be affected.</param>
        public abstract void Begin(AirSession airSession);

        /// <summary>
        /// Executes the event, suppressing anything that would be gone by the time of the next event, such as voice lines.
        /// </summary>
        /// <param name="airSession">The current air-session to be affected.</param>
        public virtual void FastForward(AirSession airSession)
        {
            this.Begin(airSession);
        }

        /// <summary>
        /// Returns true if the game cannot be saved while this is in the queue.
        /// </summary>
        public virtual bool PreventsSaving
        {
            get
            {
                return false;
            }
        }
    }
}