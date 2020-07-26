using Nsnbc.Auxiliary;
using Nsnbc.Events;

namespace Nsnbc.Core
{
    internal class ClickToContinueActivity : IQActivity
    {
        public bool Blocking => true;
        public bool Dead { get; private set; }
        public void Update(AirSession airSession, float elapsedSeconds)
        {
            if (Root.WasMouseLeftClick || Root.WasTouchReleased)
            {
                Dead = true;
            }
        }
    }
}