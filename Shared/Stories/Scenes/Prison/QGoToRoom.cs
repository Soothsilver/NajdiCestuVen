using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Scenes.Prison
{
    public class QGoToRoom : QEvent
    {
        public Room Room { get; }

        public QGoToRoom(Room room)
        {
            Room = room;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.ActiveScene!.ActiveRoom = Room;
        }
    }
}