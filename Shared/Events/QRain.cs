using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Sounds;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QRain : QEvent
    {
        private readonly float volume;

        public QRain(float volume)
        {
            this.volume = volume;
        }
        public override void Begin(AirSession airSession)
        {
            Sfxs.BeginRain(volume);
        }
    }
}