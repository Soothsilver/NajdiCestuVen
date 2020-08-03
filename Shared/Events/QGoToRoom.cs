using JetBrains.Annotations;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes;

namespace Nsnbc.Events
{
    public class QGoToRoom : QEvent
    {
        public string RoomName { get; set; }

        public QGoToRoom(string roomName)
        {
            RoomName = roomName;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.Session.SceneStack.Pop();
            airSession.Session.PushScene(airSession.Session.FindExistingScene(RoomName)! );
        }
    }
}