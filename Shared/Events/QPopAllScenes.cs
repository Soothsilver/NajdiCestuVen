using System.Linq;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)] 
    public class QPopAllScenes : QEvent
    {
        public override void Begin(AirSession airSession)
        {
            foreach (Scene scene in airSession.Session.SceneStack.Reverse<Scene>().ToList())
            {
                airSession.Session.SceneStack.Pop();
                scene.AfterPop(airSession);
            }
        }
    }
}