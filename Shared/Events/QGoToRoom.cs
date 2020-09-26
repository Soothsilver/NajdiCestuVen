using JetBrains.Annotations;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)] 
    public class QGoToRoom : QEvent
    {
        public string RoomName { get; set; }

        public QGoToRoom(string roomName)
        {
            RoomName = roomName;
        }

        public override void Begin(AirSession airSession)
        {
            Scene currentScene = airSession.Session.SceneStack.Pop();
            Scene newScene = airSession.Session.FindExistingScene(RoomName)!;
            airSession.Session.PushScene(newScene);
            newScene.CurrentZoom = currentScene.CurrentZoom;
            airSession.QuickEnqueue(new QZoomInto(CommonGame.R1920x1080, 0.1f));

        }
    }
}