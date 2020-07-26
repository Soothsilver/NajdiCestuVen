using Nsnbc.Core;
using Nsnbc.Sounds;

namespace Nsnbc.Events
{
    public class QSilence : QEvent
    {
        public override void Begin(AirSession airSession)
        {
            Sfxs.Silence();
        }
    }
}