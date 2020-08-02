using JetBrains.Annotations;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes;

namespace Nsnbc.Events
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
        
        [UsedImplicitly]
        public QGoToRoom()
        {
        }

        public override void Begin(AirSession airSession)
        {
            airSession.ActiveScene!.ActiveRoom =
                Room ?? ( airSession.ActiveScene!.FindExistingRoom(RoomName) );
        }
    }
}