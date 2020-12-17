using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    internal class QSetInteractibleArt : QEvent
    {
        private readonly string interactibleName;
        private readonly ArtName newArt;

        public QSetInteractibleArt(string interactibleName, ArtName newArt)
        {
            this.interactibleName = interactibleName;
            this.newArt = newArt;
        }

        public override void Begin(AirSession airSession)
        {
            var interactible = airSession.Session.FindInteractible(interactibleName)!;
            interactible.VisualArt = newArt;
        }
    }
}