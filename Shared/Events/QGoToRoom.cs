using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Scenes.Prison
{
    public class QGoToRoom : QEvent
    {
        public Room Room { get; set; }
        public string RoomName { get; set; }

        public QGoToRoom(Room room)
        {
            Room = room;
        }
        public QGoToRoom(string roomName)
        {
            RoomName = roomName;
        }
        
        public QGoToRoom()
        {
        }

        public override void Begin(AirSession airSession)
        {
            airSession.ActiveScene!.ActiveRoom = Room;
        }
    }
}