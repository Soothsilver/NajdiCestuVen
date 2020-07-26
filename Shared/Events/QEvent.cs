namespace Nsnbc.Events
{
    /// <summary>
    /// Represents an atomic part of a script, such as a single dialogue line or the starting of a song.
    /// </summary>
    public abstract class QEvent
    {
        public abstract void Begin(Session session);
    }
}