using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Scenes.Xml
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