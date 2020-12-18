using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Sounds;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QSilence : QEvent
    {
        public override void Begin(AirSession airSession)
        {
            airSession.Session.CurrentBackgroundSoundLoop = SoundEffectName.Null;
            Sfxs.Silence();
        }
    }
}