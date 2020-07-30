using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Stories;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QMakeInvisible : QEvent
    {
        private VisibleInteractible visibleInteractible;

        public QMakeInvisible(VisibleInteractible visibleInteractible)
        {
            this.visibleInteractible = visibleInteractible;
        }

        public override void Begin(AirSession airSession)
        {
            visibleInteractible.VisualArt = ArtName.Null;
            visibleInteractible.Rectangle = Rectangle.Empty;
        }
    }
}