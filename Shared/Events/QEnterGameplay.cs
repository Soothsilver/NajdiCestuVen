using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Sounds;
using Nsnbc.Stories;
using Nsnbc.Texts;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QEnterGameplay : QEvent
    {
        public override void Begin(AirSession airSession)
        {
            airSession.QuickEnqueue(new Script(
                new QEvent[]
            {
                new QSilence(),
                new QSfx(SoundEffectName.Whoosh),
                new QFlyFromCenter(G.CzEn(ArtName.Najdi, ArtName.NajdiEn), 1),
                new QSfx(SoundEffectName.Whoosh),
                new QFlyFromCenter(G.CzEn(ArtName.Cestu, ArtName.CestuEn), 1),
                new QSfx(SoundEffectName.Whoosh),
                new QFlyFromCenter(G.CzEn(ArtName.Ven, ArtName.VenEn), 1),
                new QWait(1, false),
                new QEndFlyouts(),
                new QYouHaveControl(true),
                new QBeginSong(Songname.Gameplay)
            }));
        }
    }
}