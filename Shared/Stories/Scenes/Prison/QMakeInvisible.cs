using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Scenes.Prison
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
            visibleInteractible.Visible = false;
            visibleInteractible.Rectangle = Rectangle.Empty;
        }
    }
}