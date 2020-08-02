using Nsnbc.Core;

namespace Nsnbc.Events
{
    /// <summary>
    /// A qevent that does nothing.
    /// </summary>
    internal class QNop : QEvent
    {
        public override void Begin(AirSession airSession)
        {
        }
    }
}